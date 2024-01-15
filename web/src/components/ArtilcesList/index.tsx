import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router";

import {
  ArticleCard,
  Container,
  AuthorName,
  ArticleTitle,
  ArticleMetadata,
} from "./styles";

import { ArticleDto, getArticles } from "../../api/articles";
import { getFormatedDate } from "../../utils/date";

const ArticlesList = () => {
  const navigate = useNavigate();
  const [articles, setArticles] = useState<ArticleDto[] | null>(null);

  useEffect(() => {
    async function getResponse() {
      const result = await getArticles();

      if (result) {
        setArticles(result as ArticleDto[]);
      }
    }

    getResponse();
  }, []);

  return (
    <Container>
      {articles &&
        articles.map(art => {
          return (
            <ArticleCard
              onClick={() => navigate(`/articles/${art.id}`)}
              key={`article-${art.id}`}
            >
              <AuthorName>{art.creator}</AuthorName>
              <ArticleTitle>{art.title}</ArticleTitle>
              <ArticleMetadata>{getFormatedDate(art.created)}</ArticleMetadata>
            </ArticleCard>
          );
        })}
    </Container>
  );
};

export default ArticlesList;
