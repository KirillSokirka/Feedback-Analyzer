import React, { useState, useCallback, useEffect } from "react";

import { CommentDto } from "../../api/articles";
import { StyledButton } from "../GlobalStyles";

import {
  Container,
  CommentContainer,
  CommentText,
  CommentatorName,
  CommentActions,
  CommentEditMode,
  CommentEditModeActions,
} from "./styles";
import { StyledInput } from "../GlobalStyles";
import useAuthContext from "../../context/hooks";

interface CommentsSectionProps {
  comments: CommentDto[];
  comment: string;
  setComment: React.Dispatch<React.SetStateAction<string>>;
  postComment: (comment: string) => void;
  updateComment: (text: string, commentId: string) => void;
  deleteComment: (commentId: string) => void;
}

const CommentsSection = ({
  comments,
  comment,
  setComment,
  postComment,
  updateComment,
  deleteComment,
}: CommentsSectionProps) => {
  const { user, jwtTokens } = useAuthContext();
  const [editingId, setEditingId] = useState<string | null>(null);
  const [editText, setEditText] = useState("");

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setComment(event.target.value);
  };

  const handleEditChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEditText(event.target.value);
  };

  const handlePostCommentClick = () => {
    if (comment.trim()) {
      postComment(comment);
      setComment("");
    }
  };

  const handleUpdateClick = (commentId: string) => {
    if (editText.trim()) {
      updateComment(editText, commentId);
      setEditingId(null);
      setEditText("");
    }
  };

  const handleDeleteClick = (commentId: string) => {
    deleteComment(commentId);
  };

  const handleEditClick = (comment: CommentDto) => {
    setEditingId(comment.id);
    setEditText(comment.text);
  };

  const handleCancelClick = () => {
    setEditingId(null);
    setEditText("");
  };

  return (
    <Container>
      {user && jwtTokens && (
        <>
          <StyledInput
            type="text"
            name="comment"
            placeholder="Leave your opinion"
            width="100%"
            fontSize="18px"
            onChange={handleInputChange}
            value={comment}
          />
          {comment && (
            <StyledButton
              onClick={handlePostCommentClick}
              disabled={!comment.trim()}
            >
              Send Comment
            </StyledButton>
          )}
        </>
      )}
      {comments.length !== 0 &&
        comments.map(comment => (
          <CommentContainer key={comment.id}>
            {editingId === comment.id ? (
              <CommentEditMode>
                <StyledInput
                  width="100%"
                  fontSize="18px"
                  value={editText}
                  onChange={handleEditChange}
                />
                <CommentEditModeActions>
                  <StyledButton
                    width="100px"
                    padding="8px 12px"
                    onClick={() => handleUpdateClick(comment.id)}
                  >
                    Update
                  </StyledButton>
                  <StyledButton
                    width="100px"
                    padding="8px 12px"
                    onClick={handleCancelClick}
                  >
                    Cancel
                  </StyledButton>
                </CommentEditModeActions>
              </CommentEditMode>
            ) : (
              <>
                <CommentatorName>{comment.commenator}</CommentatorName>
                <CommentText>{comment.text}</CommentText>
                {user && (
                  <CommentActions>
                    <StyledButton
                      width="100px"
                      padding="8px 12px"
                      onClick={() => handleEditClick(comment)}
                    >
                      Edit
                    </StyledButton>
                    <StyledButton
                      width="100px"
                      padding="8px 12px"
                      onClick={() => handleDeleteClick(comment.id)}
                    >
                      Delete
                    </StyledButton>
                  </CommentActions>
                )}
              </>
            )}
          </CommentContainer>
        ))}
    </Container>
  );
};

export default CommentsSection;
