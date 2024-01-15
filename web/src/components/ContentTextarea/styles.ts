import styled from "styled-components";

const ContentInput = styled.textarea`
  min-height: 50px;
  height: auto;
  overflow: hidden;
  grid-column: 2 / 3;
  font-family: "Space Mono", monospace;
  font-weight: 400;
  font-style: normal;
  resize: none;
  overflow: hidden;
  display: block;
  width: 100%;
  text-align: left;
  border: none;
  outline: none;
  margin-bottom: 20px;

  &:placeholder {
    color: rgba(55, 53, 47, 0.4);
  }
`;

export { ContentInput };
