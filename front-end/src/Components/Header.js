import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import basket from '../Images/basket.png';
import profile from '../Images/profile.png';
import search from '../Images/search.png';
import basket2 from '../Images/basket2.png';
import profile2 from '../Images/profile2.png';
import search2 from '../Images/search2.png';
import InputGroup from 'react-bootstrap/InputGroup';
import { Modal, Button } from 'react-bootstrap';

function Header() {
  const [searchIcon, setSearchIcon] = useState(search);
  const [basketIcon, setBasketIcon] = useState(basket);
  const [profileIcon, setProfileIcon] = useState(profile);
  const [showModal, setShowModal] = useState(false);
  const [desiredprice, setDesiredPrice] = useState('');
  const [articleSend, setArticle] = useState('');
  const [articleError, setArticleError] = useState(false);
  const [desiredPriceError, setDesiredPriceError] = useState(false);

  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      setIsMobile(window.innerWidth <= 768);
    };

    window.addEventListener('resize', handleResize);
    handleResize();

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  const handleDesiredPriceChange = (event) => {
    const desiredPriceRegex = /^\d+(\.\d+)?$/;
    setDesiredPrice(event.target.value);
    setDesiredPriceError(!desiredPriceRegex.test(event.target.value));
  };

  const handleSubmit = () => {
    const data = {
      DesiredPrice: desiredprice,
      Article: articleSend,
    };
    handleProduct(desiredprice);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleShowModal = () => {
    if (articleError || articleSend === '') {
      alert('Пожалуйста, проверьте введенный артикул.');
      setShowModal(false);
    } else {
      setShowModal(true);
    }
  };

  const handleSearchMouseOver = () => {
    if (articleSend === '') {
      setSearchIcon(search);
    } else {
      setSearchIcon(search2);
    }
  };

  const handleSearchMouseOut = () => {
    setSearchIcon(search);
  };

  const handleBasketMouseOver = () => {
    setBasketIcon(basket2);
  };

  const handleBasketMouseOut = () => {
    setBasketIcon(basket);
  };

  const handleProfileMouseOver = () => {
    setProfileIcon(profile2);
  };

  const handleProfileMouseOut = () => {
    setProfileIcon(profile);
  };

  const handleArticleChange = (value) => {
    const articleRegex = /^\d{8,}$/;
    setArticle(value);
    setArticleError(value !== '' && !articleRegex.test(value));
  };

  const handleProduct = (desiredprice) => {
    const data = {
      DesiredPrice: parseFloat(desiredprice),
      Article: articleSend,
    };

    const data1 = {
      Article: articleSend,
    };

    const url = 'https://localhost:8443/api/Product/createProduct';
    const url1 = `https://localhost:8443/api/PriceHistory/${articleSend}`;
    axios
      .post(url, data)
      .then((result) => {
        if (result.status === 200) {
          axios
            .post(url1, data1)
            .then((result) => {
              if (result.status === 200) {
                document.location = '/basket';
              }
            })
            .catch((error) => {
              alert("Товар не найден.");
            });
        }
      })
      .catch((error) => {
        alert("Товар не найден.");
      });
  };

  return (
    <>
      <Modal
        show={showModal}
        onHide={handleCloseModal}
        style={{ fontFamily: 'Franklin Gothic Book, sans-serif', fontSize: '16px', color: '#150056' }}
      >
        <Modal.Header closeButton>
          <Modal.Title>Введите желаемую цену</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Group controlId="desiredPrice">
            <Form.Label style={{ color: '#150056' }}>Желаемая цена</Form.Label>
            <Form.Control
              type="text"
              value={desiredprice}
              onChange={handleDesiredPriceChange}
              className={`desired-price-input ${desiredPriceError ? 'input-error' : ''}`}
              style={{ backgroundColor: '#6233F8', color: 'white' }}
            />
            {desiredPriceError && (
              <p className="error-message">Пожалуйста, введите число (дробную часть через точку)</p>
            )}
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" style={{ backgroundColor: '#6233F8', color: 'white' }} onClick={handleSubmit}>
            Сохранить
          </Button>
        </Modal.Footer>
      </Modal>

      <Navbar className="head">
        <Container>
          <Navbar.Brand className={`logo ${isMobile ? 'mobile' : ''}`}>
            {isMobile ? 'WP' : 'WILDPRICES'}
          </Navbar.Brand>
          <Nav>
            <InputGroup className="search">
              <Form.Control
                type="text"
                placeholder="Артикул товара:"
                className={`searchinput ${articleError ? 'input-error' : ''}`}
                onChange={(e) => handleArticleChange(e.target.value)}
              />
            </InputGroup>
            <Nav.Link href="#action1">
              <img
                src={searchIcon}
                alt="Поиск"
                onMouseOver={handleSearchMouseOver}
                onMouseOut={handleSearchMouseOut}
                width="29"
                height="29"
                className="icon"
                onClick={handleShowModal}
              />
            </Nav.Link>
            <Nav.Link href="#action1">
              <img
                src={basketIcon}
                alt="Корзина"
                onMouseOver={handleBasketMouseOver}
                onMouseOut={handleBasketMouseOut}
                width="36"
                height="29"
                className="icon"
                onClick={() => {
                  document.location = 'http://localhost:3000/basket';
                }}
              />
            </Nav.Link>
            <Nav.Link href="#action1">
              <img
                src={profileIcon}
                alt="Профиль"
                onMouseOver={handleProfileMouseOver}
                onMouseOut={handleProfileMouseOut}
                width="27"
                height="32"
                className="icon"
                onClick={() => {
                  document.location = 'http://localhost:3000/profile';
                }}
              />
            </Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
}

export default Header;