import { LOGIN_URL, REFRESH_URL, REGISTER_URL } from "./urls";

import { JwtTokens } from "./interfaces";

import {
  defaultFetch,
  protectedFetch,
  processResponse,
  ProblemDetails,
} from "../common";

export const login = async (
  email: string,
  password: string
): Promise<JwtTokens | null | boolean> => {
  const response = await defaultFetch<JwtTokens>(LOGIN_URL, {
    method: "post",
    data: { email, password },
  });

  return processResponse(response);
};

export const register = async (
  email: string,
  username: string,
  password: string
): Promise<void | null | boolean> => {
  const response = await defaultFetch<void>(REGISTER_URL, {
    method: "post",
    data: { email, username, password },
  });

  return processResponse(response);
};

export const refreshToken = async (
  email: string,
  tokens: JwtTokens
): Promise<JwtTokens | null | boolean> => {
  const response = await protectedFetch<JwtTokens>(
    REFRESH_URL,
    tokens.accessToken,
    {
      method: "post",
      data: {
        email,
        ...tokens,
      },
    }
  );

  return processResponse(response);
};
