import React, { useEffect, useState } from 'react';
import './App.css';
import { Header } from './Header';
import { BrowserRouter, Route, Router, Routes } from 'react-router-dom';
import Home from './Home';
import BookList from './BookList';
import { Book } from './interfaces';
import axios from 'axios';
import Authentication from './Authentication';
import Login from './Login';
import { useJwt } from 'react-jwt';
import jwtDecode from 'jwt-decode';

function App() {
  
  const [books, setBooks] = useState<Book[]>([]);

  const [token, setToken] = useState<string | null>(localStorage.getItem("jwt"));

  const getBooks = () => {
    const baseUrl= process.env.REACT_APP_API;
    const url = "book/all";
    axios({method: "GET", baseURL: baseUrl!, url: url}).then(
      response => {setBooks(response.data);})
    .catch(error => {});
  }

  const setUpToken = () => {
    const token = localStorage.getItem("jwt");
    setToken(token);
  }

  const getToken = () => {
    const token = localStorage.getItem("jwt");
    if(!token)
      return null;
    return token;
  }

  const getUser = () => {
    const jwtToken = getToken();

    if(!jwtToken)
      return null;
    
    const decodedToken:any = jwtDecode(jwtToken); 
    const user = {
        userName: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"], 
        role: decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]};

    return user;
  }

  useEffect(() => {
    const token = localStorage.getItem("jwt");
    if(!token)
      return;
    const decoded:any = jwtDecode(token);
    if(decoded.exp * 1000 < new Date().getTime())
    {
      localStorage.removeItem("jwt");
      setToken("");
    }
  }, []);

  return (
    <div className="App">
        <BrowserRouter>
          <Header getUser={getUser} setToken={setUpToken} token={token}></Header>
          <Routes>
            <Route path="/" element={<Home></Home>}></Route>
            <Route path="/books" element={<BookList books={books} getBooks={getBooks} getUser={getUser} getToken={getToken} setToken={setUpToken}></BookList>}></Route>
            <Route path="/auth" element={<Authentication setToken={setUpToken} getUser={getUser}></Authentication>}></Route>
          </Routes>
        </BrowserRouter>
    </div>
  );
}

export default App;
