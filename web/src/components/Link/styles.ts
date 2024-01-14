import styled from "styled-components";
import { Link } from "react-router-dom";

const StyledLink = styled(Link)`
  text-decoration: none;
  display: inline-block;
  color: white;
  padding: 0 40px 0 0;
  font-size: 20px;

  &::after {
    content: "";
    display: block;
    width: 0;
    height: 2px;
    background: white;
    transition: width 0.3s;
  }

  &:hover {
    cursor: pointer;
  }

  &:hover::after {
    width: 100%;
    transition: width 0.3s;
  }

  &:last-child {
    padding: 0;
  }
`;

export { StyledLink };
