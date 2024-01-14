import React from "react";
import { StyledLink } from "./styles";

interface LinkProps {
  label: string;
  route: string;
  onClick?: () => void;
}

export const Link = ({ label, route, onClick }: LinkProps) => {
  return (
    <StyledLink to={route} onClick={onClick}>
      {label}
    </StyledLink>
  );
};
