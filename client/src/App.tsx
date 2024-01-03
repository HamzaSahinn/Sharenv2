import React from 'react';
import './App.css';
import SignInSignUp from './views/SignInSignUpForm/SignInSignUpForm.Public';
import { AuthProvider } from 'react-auth-kit';
import { BrowserRouter, Route, RouterProvider, Routes, createBrowserRouter } from "react-router-dom";
import { RequireAuth } from 'react-auth-kit'
import Dashboard from './views/Dashboard/Dashboard.Private';
import Home from './views/Home/Home.Public';
import DashboardLayout from './layouts/Dashboard.Layout';
import FilesPage from './views/Dashboard/Files.Private';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/login",
    element: <SignInSignUp />
  },
  {
    path: "/dashboard",
    element: <RequireAuth loginPath={'/login'}><DashboardLayout /></RequireAuth>,
    children: [
      {
        index: true,
        element: <Dashboard />
      },
      {
        path: 'files',
        element: <FilesPage />
      }
    ]
  }
]);

function App() {
  return (
    <AuthProvider authType={'cookie'}
      authName={'_auth'}
      cookieDomain={window.location.hostname}
      cookieSecure={window.location.protocol === "https:"}>
      <RouterProvider router={router} />
    </AuthProvider>

  );
}

export default App;
