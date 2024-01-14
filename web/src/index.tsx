import React from "react";
import ReactDOM from "react-dom/client";
import axios from "axios";
import { Route, BrowserRouter, Routes } from "react-router-dom";

import App from "./App";

import NotFound from "./pages/NotFound";
import ArticleBrowse from "./pages/ArticlesBrowse";
import LoginPage from "./pages/Login";
import RegisterPage from "./pages/Register";

axios.defaults.xsrfCookieName = "csrftoken";
axios.defaults.xsrfHeaderName = "X-CSRFToken";

const rootDiv = document.getElementById("root") as HTMLElement;

const reactRoot = ReactDOM.createRoot(rootDiv);

reactRoot.render(
  <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route path="/" element={<ArticleBrowse />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/signup" element={<RegisterPage />} />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </>
);
