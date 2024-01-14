export interface User {
  id: number;
  fullname: string;
  email: string;
}

export interface JwtTokens {
  accessToken: string;
  refreshToken: string;
}
