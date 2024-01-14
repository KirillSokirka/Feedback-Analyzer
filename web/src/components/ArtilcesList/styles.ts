import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 20px;
`;

const ArticleCard = styled.div`
  background: white;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: fit-content;
  transition: box-shadow 0.3s ease, transform 0.3s ease;

  &:hover {
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
    transform: translateY(-2px);
  }
`;

const ArticleTitle = styled.h1`
  font-size: 24px;
  color: #333;
  margin: 0;
`;

const AuthorName = styled.h2`
  font-size: 16px;
  font-weight: normal;
  color: #666;
  margin: 8px 0;
`;

const ArticleMetadata = styled.div`
  display: flex;
  align-items: center;
  justify-content: start;
  font-size: 14px;
  color: #999;
`;

export { Container, ArticleCard, ArticleTitle, AuthorName, ArticleMetadata };
