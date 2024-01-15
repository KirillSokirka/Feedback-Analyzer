import React, { useState, useEffect, useCallback } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";

import { StyledButton } from "../../components/GlobalStyles";
import useAuthContext from "../../context/hooks";

import {
  updateArticle,
  getArticleById,
  ArticleDetailDto,
  UpdateArticleCommand,
} from "../../api/articles";

import {
  PageContainer,
  PageContent,
  TitleInput,
  Header,
  HeaderActions,
} from "./styles";

import ContentTextArea from "../../components/ContentTextarea";

const ArticleEdit = () => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  const { user, jwtTokens } = useAuthContext();
  const navigate = useNavigate();
  const { articleId } = useParams();

  const getResponse = useCallback(async () => {
    if (articleId) {
      const result = await getArticleById(articleId);
      if (result) {
        setTitle((result as ArticleDetailDto).title);
        setContent((result as ArticleDetailDto).content);
      }
    }
  }, [articleId]);

  useEffect(() => {
    getResponse();
  }, [getResponse]);

  const handleTitleChange = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      setTitle(event.target.value);
    },
    []
  );

  const handleSubmit = useCallback(async () => {
    if (user && jwtTokens && articleId) {
      const article: UpdateArticleCommand = {
        id: articleId,
        title: title,
        content: content,
      };

      const response = await updateArticle(article, jwtTokens.accessToken);

      if (!response) {
        toast.success("Article updated successfully!");
        navigate("/");
      }
    }
  }, [user, jwtTokens, articleId, title, content, navigate]);

  return (
    <PageContainer>
      <Header>
        <HeaderActions>
          <StyledButton width="100px" padding="8px 12px" onClick={handleSubmit}>
            Submit
          </StyledButton>
        </HeaderActions>
      </Header>
      <PageContent>
        <TitleInput
          placeholder="Untitled"
          value={title}
          onChange={handleTitleChange}
        />
        <ContentTextArea
          value={content}
          handleChange={setContent}
          placeHolder="content goes here..."
        />
      </PageContent>
    </PageContainer>
  );
};

export default ArticleEdit;
