import React from "react";

import {
  Container,
  SBottomLinks,
  SContent,
  SLinks,
  StatisticContainer,
} from "./styles";

import { Link } from "../Link";
import useAuthContext from "../../context/hooks";
import UserStatistic from "../UserStatistic";

const Sidebar = () => {
  const { user, logoutUser } = useAuthContext();

  return (
    <Container>
      <SContent>
        {user && <h2>Hello {user.fullname}!</h2>}
        <StatisticContainer>
          <UserStatistic />
        </StatisticContainer>
        <SLinks>
          <Link label="Articles" route="/" />
          {user && (
            <>
              <Link label="Create article" route="/articles/create" />
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
