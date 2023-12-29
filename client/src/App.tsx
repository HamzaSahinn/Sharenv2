import React from 'react';
import './App.css';
import SignInSignUp from './views/SignInSignUpForm/SignInSignUpForm.Public';
import { AuthProvider } from 'react-auth-kit';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { RequireAuth } from 'react-auth-kit'
import Dashboard from './views/Dashboard/Dashboard.Private';
import Home from './views/Home/Home.Public';

function App() {
  return (
    <AuthProvider authType={'cookie'}
      authName={'_auth'}
      cookieDomain={window.location.hostname}
      cookieSecure={window.location.protocol === "https:"}>
      <div className="flex w-full justify-center items-center h-screen">
        <BrowserRouter>
          <Routes>
            <Route path='/' element={
              <Home />
            } />
            <Route path="/login" element={<SignInSignUp />} />
            <Route path='/dashboard' element={<RequireAuth loginPath={'/login'}>
              <Dashboard />
            </RequireAuth>} />
          </Routes>
        </BrowserRouter>
      </div>
    </AuthProvider>

  );
}

export default App;
