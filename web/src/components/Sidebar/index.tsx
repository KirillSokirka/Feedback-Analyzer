import React, { useCallback } from "react";

import {
  Container,
  SBottomLinks,
  SContent,
  SLinks,
  SAuthLinks,
} from "./styles";

import { Link } from "../Link";
import useAuthContext from "../../context/hooks";

const Sidebar = () => {
  const { user, logoutUser } = useAuthContext();

  console.log(user);

  return (
    <Container>
      <SContent>
        {user ? <h2>Hello {user.fullname}!</h2> : <h1>Hello ......</h1>}
        <SLinks>
          <Link label="Articles" route="/" />
          {user && (
            <>
              <Link label="Create article" route="/" />
              <Link label="My Statistic" route="/" />
            </>
          )}
        </SLinks>
        <SBottomLinks>
          <SLinks>
            {user ? (
              <Link label="Logout" route="/" onClick={logoutUser} />
            ) : (
              <>
                <Link label="Log in" route="/login" />
                <Link label="Sign up" route="/signup" />
              </>
            )}
          </SLinks>
        </SBottomLinks>
      </SContent>
    </Container>
  );
};

export default Sidebar;
