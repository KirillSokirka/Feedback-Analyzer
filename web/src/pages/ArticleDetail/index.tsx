import React from "react";

import {
  Title,
  PageContainer,
  Header,
  HeaderTitle,
  HeaderActions,
  ActionItem,
  PageContent,
  Text,
  Edited,
} from "./styles";

const ArticleDetail = () => {
  return (
    <PageContainer>
      <Header>
        <HeaderTitle>Tasker Settings</HeaderTitle>
        <HeaderActions>
          <Edited>edited - </Edited>
          <ActionItem>Edit</ActionItem>
          <ActionItem>Delete</ActionItem>
        </HeaderActions>
      </Header>
      <PageContent>
        <Title>Tasker Settings</Title>
        <Text>
          Lorem Ipsum is simply dummy text of the printing and typesetting
          industry. Lorem Ipsum has been the industry's standard dummy text ever
          since the 1500s, when an unknown printer took a galley of type and
          scrambled it to make a type specimen book. It has survived not only
          five centuries, but also the leap into electronic typesetting,
          remaining essentially unchanged. It was popularised in the 1960s with
          the release of Letraset sheets containing Lorem Ipsum passages, and
          more recently with desktop publishing software like Aldus PageMaker
          including versions of Lorem Ipsum.
        </Text>
      </PageContent>
    </PageContainer>
  );
};

export default ArticleDetail;
