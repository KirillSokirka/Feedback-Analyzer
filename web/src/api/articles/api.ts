import { defaultFetch, protectedFetch } from "../common/http";
import { processResponse } from "../common/utils";

import {
  ArticleDto,
  ArticleDetailDto,
  CreateArticleCommand,
  UpdateArticleCommand,
} from "./interfaces";

import { ARTICLES, ARTICLE } from "./urls";

export const getArticles = async (): Promise<ArticleDto[] | null> => {
  const response = await defaultFetch<ArticleDto[]>(ARTICLES);
  return processResponse(response);
};

export const getArticleById = async (
  id: string
): Promise<ArticleDetailDto | null> => {
  const response = await defaultFetch<ArticleDetailDto>(`${ARTICLE(id)}`);
  return processResponse(response);
};

export const postArticle = async (
  article: CreateArticleCommand,
  token: string
): Promise<string | null> => {
  const response = await protectedFetch<string>(ARTICLES, token, {
    method: "post",
    data: article,
  });
  return processResponse(response);
};

export const updateArticle = async (
  article: UpdateArticleCommand,
  token: string
): Promise<string | null> => {
  const response = await protectedFetch<string>(ARTICLES, token, {
    method: "put",
    data: article,
  });
  return processResponse(response);
};

export const deleteArticle = async (
  id: string,
  token: string
): Promise<null | null> => {
  const response = await protectedFetch<null>(`${ARTICLE(id)}`, token, {
    method: "delete",
  });
  return processResponse(response);
};
