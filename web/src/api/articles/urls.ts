const BASE_URL = process.env.REACT_APP_BASE_URL || "";

// Articles
const ARTICLES = `${BASE_URL}/articles`;
const ARTICLE = (id: string) => `${BASE_URL}/articles/${id}`;
const ARTICLE_COMMENTS = (articleId: string) =>
  `${BASE_URL}/articles/${articleId}/comments`;
const COMMENT = (articleId: string, commentId: string) =>
  `${BASE_URL}/articles/${articleId}/comments/${commentId}`;
const ARTICLE_FEEDBACK = (articleId: string) =>
  `${BASE_URL}/articles/${articleId}/feedback`;

export { ARTICLES, ARTICLE, ARTICLE_COMMENTS, COMMENT, ARTICLE_FEEDBACK };
