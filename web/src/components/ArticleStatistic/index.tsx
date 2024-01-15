import React, { useState, useEffect } from "react";

import { GradientText } from "../GlobalStyles";
import { Container, SentimentParagraph } from "./styles";

import {
  getArticleFeedback,
  getArticleCommentsFeedback,
  SentimentDto,
  SentimentType,
} from "../../api/statitstic";

import { getEmoji, getText } from "../../utils/sentiment";

interface ArticleStatisticProps {
  articleId: string;
}

const ArticleStatistic = ({ articleId }: ArticleStatisticProps) => {
  const [articleFeedback, setArticleFeedback] = useState<SentimentDto | null>(
    null
  );
  const [commentsFeedback, setCommentsFeedback] = useState<SentimentDto | null>(
    null
  );

  useEffect(() => {
    const fetchFeedback = async () => {
      const articleFeedback = await getArticleFeedback(articleId);
      const commentsFeedback = await getArticleCommentsFeedback(articleId);

      if (articleFeedback) setArticleFeedback(articleFeedback as SentimentDto);
      if (commentsFeedback)
        setCommentsFeedback(commentsFeedback as SentimentDto);
    };

    fetchFeedback();
  }, [articleId]);

  return (
    <Container>
      <div>
        {articleFeedback &&
          articleFeedback.sentiment !== SentimentType.Empty && (
            <div>
              <GradientText>Article Feedback:</GradientText>
              <SentimentParagraph>
                {getEmoji(articleFeedback.sentiment)}{" "}
                {getText(articleFeedback.sentiment)}
              </SentimentParagraph>
            </div>
          )}
      </div>
      <div>
        {commentsFeedback &&
          commentsFeedback.sentiment !== SentimentType.Empty && (
            <div>
              <GradientText>Comments Feedback</GradientText>
              <SentimentParagraph>
                {getEmoji(commentsFeedback.sentiment)}{" "}
                {getText(commentsFeedback.sentiment)}
              </SentimentParagraph>
            </div>
          )}
      </div>
    </Container>
  );
};

export default ArticleStatistic;
