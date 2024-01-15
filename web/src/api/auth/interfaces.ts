export interface User {
  id: string;
  fullname: string;
  email: string;
}

export interface JwtTokens {
  accessToken: string;
  refreshToken: string;
}
