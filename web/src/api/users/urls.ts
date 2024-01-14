const BASE_URL = process.env.REACT_APP_BASE_URL || "";

const USER = (id: string) => `${BASE_URL}/users/${id}`;
const USER_ARTICLES_FEEDBACK = (userId: string) =>
  `${BASE_URL}/users/${userId}/articles/feedback`;

export { USER, USER_ARTICLES_FEEDBACK };
