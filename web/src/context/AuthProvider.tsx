import React, { ReactNode, useState, useEffect, useCallback } from "react";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

import AuthContext, { IAuthContext } from "./AuthContext";
import {
  login,
  register,
  refreshToken as refreshTokenAsync,
  JwtTokens,
  User,
} from "../api/auth/";

interface Props {
  children: ReactNode;
}

const AUTH_TOKENS = `JwtTokens`;

export const AuthProvider = ({ children }: Props) => {
  const [user, setUser] = useState<User | null>(null);
  const [jwtTokens, setJwtTokens] = useState<JwtTokens | null>(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const decodeAndSetUser = useCallback((accessToken: string) => {
    const decodedToken = jwtDecode(accessToken) as any;
    setUser({
      id: decodedToken.uid,
      fullname: decodedToken.sub,
      email: decodedToken.email,
    });
  }, []);

  useEffect(() => {
    const tokens = localStorage.getItem(AUTH_TOKENS);
    if (tokens) {
      const parsedTokens: JwtTokens = JSON.parse(tokens);
      setJwtTokens(parsedTokens);
      decodeAndSetUser(parsedTokens.accessToken);
    }
  }, [decodeAndSetUser]);

  const updateAuthStates = useCallback(
    (data: JwtTokens) => {
      setJwtTokens(data);
      decodeAndSetUser(data.accessToken);
      localStorage.setItem(AUTH_TOKENS, JSON.stringify(data));
    },
    [decodeAndSetUser]
  );

  const registerUser = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const email = (
      event.currentTarget.elements.namedItem("email") as HTMLInputElement
    ).value;
    const username = (
      event.currentTarget.elements.namedItem("username") as HTMLInputElement
    ).value;
    const password = (
      event.currentTarget.elements.namedItem("password") as HTMLInputElement
    ).value;

    const data = await register(email, username, password);

    if (data) {
      navigate("/login");
    }
  };

  const loginUser = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const email = (
      event.currentTarget.elements.namedItem("email") as HTMLInputElement
    ).value;
    const password = (
      event.currentTarget.elements.namedItem("password") as HTMLInputElement
    ).value;

    const data = await login(email, password);

    if (data) {
      updateAuthStates(data as JwtTokens);
      navigate("/");
    }
  };

  const logoutUser = () => {
    setJwtTokens(null);
    setUser(null);
    localStorage.removeItem(AUTH_TOKENS);
    navigate("/");
  };

  const refreshToken = async () => {
    if (jwtTokens && user) {
      const data = await refreshTokenAsync(user.email, jwtTokens);

      if (data) {
        updateAuthStates(data as JwtTokens);
      } else {
        logoutUser();
      }
    }

    if (loading) {
      setLoading(false);
    }
  };

  const contextData: IAuthContext = {
    user,
    jwtTokens,
    registerUser,
    loginUser,
    logoutUser,
    refreshToken,
  };

  useEffect(() => {
    if (loading) {
      refreshToken();
    }

    const fiveHoursFiftyFiveMinutes = 1000 * 60 * 355;

    const interval = setInterval(() => {
      if (jwtTokens) {
        refreshToken();
      }
    }, fiveHoursFiftyFiveMinutes);

    return () => {
      clearInterval(interval);
    };
  }, [jwtTokens, loading]);

  return (
    <AuthContext.Provider value={contextData}>
      {loading ? null : children}
    </AuthContext.Provider>
  );
};
