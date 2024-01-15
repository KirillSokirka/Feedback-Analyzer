import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 5px;
`;

const CommentEditMode = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
`;

const CommentEditModeActions = styled.div`
  display: flex;
  align-items: row;
  gap: 15px;
`;

const CommentActions = styled.div`
  visibility: hidden;
  opacity: 0;
  gap: 15px;
  margin-top: 5px;
  transition: visibility 0s, opacity 0.5s ease;
  max-height: 0;
  overflow: hidden;
`;

const CommentContainer = styled.div`
  background: #f0f0f0;
  border-radius: 4px;
  padding: 10px;
  position: relative;

  &:hover ${CommentActions} {
    display: flex;
    visibility: visible;
    opacity: 1;
    max-height: 50px;
    transition-delay: 0s, 0s;
  }
`;

const CommentText = styled.p`
  color: #333;
`;

const CommentatorName = styled.span`
  font-weight: bold;
  color: rgb(55, 53, 47);
  margin-right: 8px;
  margin-bottom: 10px;
  display: block;
`;

export {
  Container,
  CommentContainer,
  CommentText,
  CommentatorName,
  CommentActions,
  CommentEditMode,
  CommentEditModeActions,
};
