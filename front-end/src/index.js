import React from 'react';
import ReactDOM from 'react-dom/client';
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

const head = document.getElementsByTagName('head')[0];
const link1 = document.createElement('link');
link1.href = 'https://fonts.googleapis.com/css2?family=Franklin+Gothic+Heavy&display=swap';
link1.rel = 'stylesheet';
head.appendChild(link1);

const link2 = document.createElement('link');
link2.href = 'https://fonts.googleapis.com/css2?family=Franklin+Gothic+Book&display=swap';
link2.rel = 'stylesheet';
head.appendChild(link2);

const link3 = document.createElement('link');
link3.href = 'https://fonts.googleapis.com/css2?family=Franklin+Gothic+Demi&display=swap';
link3.rel = 'stylesheet';
head.appendChild(link3);

const link = document.createElement('link');
link.rel = 'stylesheet';
link.href = 'https://fonts.googleapis.com/css?family=Bungee&display=swap';
document.head.appendChild(link);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
