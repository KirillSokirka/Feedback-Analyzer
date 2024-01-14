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
        <StyledInput type="text" name="email" placeholder="Email" required />
        <StyledInput
          type="text"
          name="username"
          placeholder="Full name"
          required
        />
        <StyledInput
          type="password"
          name="password"
          placeholder="Password"
          required
        />
        <StyledButton type="submit">Register</StyledButton>
      </StyledForm>
    </FullPageContainer>
  );
};

export default RegisterPage;
