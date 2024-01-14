const BASE_URL = process.env.REACT_APP_BASE_URL || "";

const REGISTER_URL = `${BASE_URL}/register`;
const LOGIN_URL = `${BASE_URL}/login`;
const REFRESH_URL = `${BASE_URL}/refresh-token`;

export { LOGIN_URL, REFRESH_URL, REGISTER_URL };
