import React from "react";
import styled from "styled-components";
import { Outlet } from "react-router-dom";

import GlobalStyles from "./components/GlobalStyles";
import { AuthProvider } from "./context/AuthProvider";

import Sidebar from "./components/Sidebar";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const App = () => {
  return (
    <>
      <GlobalStyles></GlobalStyles>
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
      <PageContainer>
        <AuthProvider>
          <Sidebar />
          <PageContent>
            <Outlet />
          </PageContent>
        </AuthProvider>
      </PageContainer>
    </>
  );
};

const PageContainer = styled.div`
  display: flex;
  flex-direction: row;
  min-height: 100vh;
`;

const PageContent = styled.div`
  flex: 1;
`;

export default App;
