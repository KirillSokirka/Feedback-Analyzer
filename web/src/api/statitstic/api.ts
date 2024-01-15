import { defaultFetch, processResponse, protectedFetch } from "../common";

import { SentimentDto } from "./interfaces";
import {
  ARTICLE_FEEDBACK,
  ARTICLE_COMMENTS_FEEDBACK,
  USER_ARTICLES_FEEDBACK,
} from "./urls";

export const getArticleFeedback = async (
  articleId: string
): Promise<SentimentDto | null | boolean> => {
  const response = await defaultFetch<SentimentDto>(
    ARTICLE_FEEDBACK(articleId)
  );
  return processResponse(response);
};

export const getArticleCommentsFeedback = async (
  articleId: string
): Promise<SentimentDto | null | boolean> => {
  const response = await defaultFetch<SentimentDto>(
    ARTICLE_COMMENTS_FEEDBACK(articleId)
  );
  return processResponse(response);
};

export const getUserArticlesFeedback = async (
  userId: string,
  accessToken: string
): Promise<SentimentDto | null | boolean> => {
  const response = await protectedFetch<SentimentDto>(
    USER_ARTICLES_FEEDBACK(userId),
    accessToken
  );
  return processResponse(response);
};
