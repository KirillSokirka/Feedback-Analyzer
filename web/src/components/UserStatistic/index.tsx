import React, { useState, useEffect } from "react";
import {
  SentimentDto,
  getUserArticlesFeedback,
  SentimentType,
} from "../../api/statitstic";
import { getEmoji, getText } from "../../utils/sentiment";

import useAuthContext from "../../context/hooks";

import { Container, SentimentParagraph } from "./styles";

const UserStatistic = () => {
  const [userArticlesFeedback, setUserArticlesFeedback] =
    useState<SentimentDto | null>(null);

  const { user, jwtTokens } = useAuthContext();

  useEffect(() => {
    const fetchFeedback = async () => {
      if (user && jwtTokens) {
        const userArticleFeedback = await getUserArticlesFeedback(
          user.id,
          jwtTokens.accessToken
        );

        if (userArticleFeedback)
          setUserArticlesFeedback(userArticleFeedback as SentimentDto);
      }
    };

    fetchFeedback();
  }, [user, jwtTokens]);

  return (
    <Container>
      <div>
        {userArticlesFeedback &&
          userArticlesFeedback.sentiment !== SentimentType.Empty && (
            <div>
              <>Articles Feedback:</>
              <SentimentParagraph>
                {getEmoji(userArticlesFeedback.sentiment)}{" "}
                {getText(userArticlesFeedback.sentiment)}
              </SentimentParagraph>
            </div>
          )}
      </div>
    </Container>
  );
};

export default UserStatistic;
