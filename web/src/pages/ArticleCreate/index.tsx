import React, { useState, useCallback } from "react";

import {
  PageContainer,
  PageContent,
  TitleInput,
  Header,
  HeaderActions,
} from "./styles";

import { StyledButton } from "../../components/GlobalStyles";
import useAuthContext from "../../context/hooks";

import ContentTextArea from "../../components/ContentTextarea";
import { postArticle } from "../../api/articles";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const ArticleCreate = () => {
  const { user, jwtTokens } = useAuthContext();
  const navigate = useNavigate();
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  const handleTitleChange = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      setTitle(event.target.value);
    },
    []
  );

  const handleSubmit = useCallback(async () => {
    if (user && jwtTokens) {
      const article = {
        title: title,
        content: content,
        creatorId: user.id,
      };

      const response = await postArticle(article, jwtTokens.accessToken);

      if (response) {
        toast.success("Article posted successfully!");
        navigate("/");
      }
    }
  }, [user, jwtTokens, title, content, navigate]);

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
          handleChange={setContent}
          value={content}
          placeHolder="content goes here.."
        />
      </PageContent>
    </PageContainer>
  );
};

export default ArticleCreate;
