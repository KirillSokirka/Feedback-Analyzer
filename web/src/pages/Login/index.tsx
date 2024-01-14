import {
  StyledInput,
  StyledButton,
  FullPageContainer,
  StyledForm,
} from "../../components/GlobalStyles";

import useAuthContext from "../../context/hooks";

const LoginPage = () => {
  const { loginUser } = useAuthContext();

  return (
    <FullPageContainer>
      <StyledForm onSubmit={loginUser}>
        <StyledInput type="text" name="email" placeholder="Email" />
        <StyledInput type="password" name="password" placeholder="Password" />
        <StyledButton type="submit">Login</StyledButton>
      </StyledForm>
    </FullPageContainer>
  );
};

export default LoginPage;
