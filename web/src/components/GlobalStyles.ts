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

interface StyledInputProps {
  width?: string;
  fontSize?: string;
}

export const StyledInput = styled.input<StyledInputProps>`
  width: ${props => props.width || "225px"};
  font-size: ${props => props.fontSize || "22px"};
  border-top: 0;
  border-left: 0;
  border-right: 0;
  border-bottom: 1px solid #ccc;

  &:focus {
    outline: none;
    border-color: #aaa;
  }
`;

interface StyledButtonProps {
  width?: string;
  padding?: string;
}

export const StyledButton = styled.button<StyledButtonProps>`
  width: ${props => props.width || "225px"};
  padding: ${props => props.padding || "12px 20px"};
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

export const GradientText = styled.h4`
  background: linear-gradient(to right, red, blue);
  background-clip: text;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  display: inline;
`;

export default GlobalStyles;
