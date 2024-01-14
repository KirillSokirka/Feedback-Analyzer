import { useContext } from "react";

import {
  StyledInput,
  StyledButton,
  StyledForm,
  FullPageContainer,
} from "../../components/GlobalStyles";
import AuthContext from "../../context/AuthContext";

const RegisterPage = () => {
  const { registerUser } = useContext(AuthContext)!;

  return (
    <FullPageContainer>
      <StyledForm onSubmit={registerUser}>
        <StyledInput
          type="text"
          name="email"
          placeholder="Enter email"
          required
        />
        <StyledInput
          type="text"
          name="username"
          placeholder="Enter username"
          required
        />
        <StyledInput
          type="password"
          name="password"
          placeholder="Enter password"
          required
        />
        <StyledInput
          type="password"
          name="confirmPassword"
          placeholder="Confirm password"
          required
        />
        <StyledButton type="submit">Register</StyledButton>
      </StyledForm>
    </FullPageContainer>
  );
};

export default RegisterPage;
