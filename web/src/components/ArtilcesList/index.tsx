import React from "react";
import { useNavigate } from "react-router";

import {
  ArticleCard,
  Container,
  AuthorName,
  ArticleTitle,
  ArticleMetadata,
} from "./styles";

const ArticlesList = () => {
  const navigate = useNavigate();

  return (
    <Container>
      <ArticleCard onClick={() => navigate("/articles/" + "1")}>
        <AuthorName>Tom Zimberoff</AuthorName>
        <ArticleTitle>At Skywalker Ranch We Were</ArticleTitle>
        <ArticleMetadata>Jan 8</ArticleMetadata>
      </ArticleCard>
      <ArticleCard>
        <AuthorName>Tom Zimberoff</AuthorName>
        <ArticleTitle>At Skywalker Ranch We Were</ArticleTitle>
        <ArticleMetadata>Jan 8</ArticleMetadata>
      </ArticleCard>
    </Container>
  );
};

export default ArticlesList;
