import styled from "styled-components";

const PageContainer = styled.div`
  margin: 20px;
`;

const Header = styled.header`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const HeaderTitle = styled.p``;

const HeaderActions = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;

const Edited = styled.div`
  font-size: 14px;
  color: grey;
`;

const ActionItem = styled.div``;

const PageContent = styled.div`
  display: grid;
  grid-template-columns: 1fr 3fr 3fr;
  margin-top: 100px;
`;

const Title = styled.h1`
  color: rgb(55, 53, 47);
  font-size: 48px;
  margin-bottom: 20px;
  grid-column: 2 / span 2;
`;

const Text = styled.div`
  padding: 10px;
  font-family: "Space Mono", monospace;
  font-weight: 400;
  font-style: normal;
  background-color: #f7f6f3;
  display: inline;
  text-align: left;
  flex-shrink: 1;
  margin-bottom: 20px;
  grid-column: 2 / 3;
`;

const CommentsSectionContainer = styled.div`
  grid-column: 2 / 3;
`;

export {
  Title,
  PageContainer,
  Header,
  HeaderTitle,
  HeaderActions,
  ActionItem,
  PageContent,
  Text,
  Edited,
  CommentsSectionContainer,
};
