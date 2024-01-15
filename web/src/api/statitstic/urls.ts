const BASE_URL = process.env.REACT_APP_BASE_URL || "";

const ARTICLE_FEEDBACK = (articleId: string) =>
  `${BASE_URL}/articles/${articleId}/feedback`;
const ARTICLE_COMMENTS_FEEDBACK = (articleId: string) =>
  `${BASE_URL}/articles/${articleId}/comments/feedback`;
const USER_ARTICLES_FEEDBACK = (userId: string) =>
  `${BASE_URL}/users/${userId}/articles/feedback`;

export { ARTICLE_FEEDBACK, ARTICLE_COMMENTS_FEEDBACK, USER_ARTICLES_FEEDBACK };
