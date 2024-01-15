import React, { useState, useEffect } from "react";
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

  useEffect(() => {
    getResponse();
  }, [articleId]);

  async function getResponse() {
    if (articleId) {
      const result = (await getArticleById(articleId)) as ArticleDetailDto;
      setTitle(result.title);
      setContent(result.content);
    }
  }

  const handleTitleChange = (event: any) => {
    setTitle(event.target.value);
  };

  const handleSubmit = async () => {
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
  };

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
