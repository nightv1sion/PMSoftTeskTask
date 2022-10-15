import React, { useState } from 'react';
import './App.css';
import { Header } from './Header';
import { BrowserRouter, Route, Router, Routes } from 'react-router-dom';
import Home from './Home';
import BookList from './BookList';
import { Book } from './interfaces';
import axios from 'axios';
import Authentication from './Authentication';
import Login from './Login';

function App() {
  
  const [books, setBooks] = useState<Book[]>([]);
  

  const getBooks = () => {
    const baseUrl= process.env.REACT_APP_API;
    const url = "book/all";
    axios({method: "GET", baseURL: baseUrl!, url: url}).then(
      response => {console.log(response); setBooks(response.data);})
    .catch(error => console.log(error.toJSON()));
  }

  return (
    <div className="App">
        <BrowserRouter>
          <Header></Header>
          <Routes>
            <Route path="/" element={<Home></Home>}></Route>
            <Route path="/books" element={<BookList books={books} getBooks={getBooks}></BookList>}></Route>
            <Route path="/auth" element={<Authentication></Authentication>}></Route>
          </Routes>
        </BrowserRouter>
    </div>
  );
}

export default App;
