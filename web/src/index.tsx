import React from "react";
import ReactDOM from "react-dom/client";
import axios from "axios";
import { Route, BrowserRouter, Routes } from "react-router-dom";

import App from "./App";

import NotFound from "./pages/NotFound";
import Articles from "./pages/Articles";
import LoginPage from "./pages/Login";
import RegisterPage from "./pages/Register";
import ArticleDetail from "./pages/ArticleDetail";
import ArticleCreate from "./pages/ArticleCreate";
import ArticleEdit from "./pages/ArticleEdit";

axios.defaults.xsrfCookieName = "csrftoken";
axios.defaults.xsrfHeaderName = "X-CSRFToken";

const rootDiv = document.getElementById("root") as HTMLElement;

const reactRoot = ReactDOM.createRoot(rootDiv);

reactRoot.render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />}>
        <Route index element={<Articles />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/signup" element={<RegisterPage />} />
        <Route path="/articles/:articleId" element={<ArticleDetail />} />
        <Route path="/articles/create" element={<ArticleCreate />} />
        <Route path="/articles/edit/:articleId" element={<ArticleEdit />} />
        <Route path="*" element={<NotFound />} />
      </Route>
    </Routes>
  </BrowserRouter>
);
