const BASE_URL = process.env.REACT_APP_BASE_URL || "";

const ARTICLE_COMMENTS = (articleId: string) =>
  `${BASE_URL}/articles/${articleId}/comments`;
const COMMENT = (articleId: string, commentId: string) =>
  `${BASE_URL}/articles/${articleId}/comments/${commentId}`;

export { ARTICLE_COMMENTS, COMMENT };
