import styled, { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
  * {
    font-family: 'Montserrat', sans-serif;
  }

  html, body {
    margin: 0;
    height: 100%;
  }

  p, h1, h2, h3, h4 {
    padding: 0;
    margin: 0;
  }
`;

export const StyledInput = styled.input`
  width: 225px;
  font-size: 22px;
  border-top: 0;
  border-left: 0;
  border-right: 0;
  border-color: #ccc;

  &:focus {
    outline: none;
  }
`;

export const StyledButton = styled.button`
  width: 225px;
  padding: 12px 20px;
  font-size: 14px;
  font-weight: normal;
  line-height: 16px;
  color: black;
  background-color: white;
  border: 1px solid black;
  transition: background-color 0.3s, color 0.3s;

  &:hover {
    outline: none;
    cursor: pointer;
    background-color: black;
    color: white;
  }

  &:active {
    outline: none;
    background-color: #fff;
    color: #000;
  }
`;

export const StyledForm = styled.form`
  margin-top: 30px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 30px;

  & > div {
    font-size: 18px;
    width: 200px;
  }
`;

export const FullPageContainer = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export default GlobalStyles;
