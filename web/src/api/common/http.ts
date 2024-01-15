import axios, { AxiosRequestConfig } from "axios";
import { ApiResponse, ProblemDetails } from "./interfaces";

export const defaultFetch = async <T>(
  url: string,
  config: AxiosRequestConfig = {}
): Promise<ApiResponse<T>> => {
  const defaultConfig: AxiosRequestConfig = {
    headers: {
      "Content-Type": "application/json",
    },
    ...config,
  };

  try {
    const response = await axios(url, defaultConfig);
    return { data: response.data };
  } catch (error: any) {
    let problemDetails: ProblemDetails = {};

    if (axios.isAxiosError(error) && error.response) {
      problemDetails = error.response.data;
    } else {
      problemDetails = {
        title: "Unknown Error",
        status: 0,
        errors: [
          {
            code: "UnknownError",
            description: "An unknown error occurred",
          },
        ],
      };
    }

    return { problemDetails };
  }
};

export const protectedFetch = async <T>(
  url: string,
  token: string,
  config: AxiosRequestConfig = {}
): Promise<ApiResponse<T>> => {
  const authConfig: AxiosRequestConfig = {
    ...config,
    headers: {
      ...config.headers,
      Authorization: `Bearer ${token}`,
    },
  };
  return defaultFetch<T>(url, authConfig);
};
