import { createContext } from "react";
import { JwtTokens } from "../api/auth/interfaces";
import { User } from "../api/auth/interfaces";

export interface IAuthContext {
  user: User | null;
  jwtTokens: JwtTokens | null;
  registerUser: (event: React.FormEvent<HTMLFormElement>) => Promise<void>;
  loginUser: (event: React.FormEvent<HTMLFormElement>) => Promise<void>;
  logoutUser: () => void;
  refreshToken: (data: JwtTokens) => Promise<void>;
}

const AuthContext = createContext<IAuthContext | null>(null);

export default AuthContext;
