import { BrowserRouter, Routes, Route } from "react-router-dom"
import React, {useState} from 'react';
import  { useEffect } from 'react';
import './App.css';
import Registration from './Pages/Registration';
import Login from './Pages/Login';
import Profile from "./Pages/Profile";
import Basket from "./Pages/Basket";
import ProductCard from "./Pages/ProductCard";

function App() {
  useEffect(() => {
    document.title = 'WILDPRICES';
  }, []);

  return (
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<Login/>}/>
      <Route path="/registration" element={<Registration />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="/basket" element={<Basket />} />
      <Route path="/productcard" element={<ProductCard />} />
    </Routes>
  </BrowserRouter>
  );
}

export default App;
