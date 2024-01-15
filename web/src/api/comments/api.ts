import { COMMENT, ARTICLE_COMMENTS } from "./urls";

import { protectedFetch, processResponse } from "../common";

export const postComment = async (
  articleId: string,
  text: string,
  commentatorId: string,
  token: string
): Promise<string | null | boolean> => {
  const response = await protectedFetch<string>(
    ARTICLE_COMMENTS(articleId),
    token,
    {
      method: "post",
      data: {
        text,
        commentatorId,
      },
    }
  );
  return processResponse(response);
};

export const updateComment = async (
  articleId: string,
  text: string,
  commentId: string,
  token: string
): Promise<string | null | boolean> => {
  const response = await protectedFetch<string>(
    COMMENT(articleId, commentId),
    token,
    {
      method: "PUT",
      data: { text },
    }
  );
  return processResponse(response);
};

export const deleteComment = async (
  articleId: string,
  commentId: string,
  token: string
): Promise<null | null | boolean> => {
  const response = await protectedFetch<null>(
    `${COMMENT(articleId, commentId)}`,
    token,
    {
      method: "delete",
    }
  );
  return processResponse(response);
};
