const BASE_URL = process.env.REACT_APP_BASE_URL || "";

// Articles
const ARTICLES = `${BASE_URL}/articles`;
const ARTICLE = (id: string) => `${BASE_URL}/articles/${id}`;

export { ARTICLES, ARTICLE };
