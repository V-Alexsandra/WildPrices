import React, { useState } from 'react';
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

  const handleDesiredPriceChange = (event) => {
    setDesiredPrice(event.target.value);
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
    setShowModal(true);
  };

  const handleSearchMouseOver = () => {
    setSearchIcon(search2);
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
    setArticle(value);
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
              alert(error);
            });
        }
      })
      .catch((error) => {
        alert(error);
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
              style={{ backgroundColor: '#6233F8', color: 'white' }}
            />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
            <Button variant="primary" style={{ backgroundColor: '#6233F8', color: 'white'}} onClick={handleSubmit}>
              Сохранить
            </Button>
        </Modal.Footer>
      </Modal>

      <Navbar className="head">
        <Container>
          <Navbar.Brand className="logo">WILDPRICES</Navbar.Brand>
          <Nav>
            <InputGroup className="search">
              <Form.Control
                type="text"
                placeholder="Артикул товара:"
                className="searchinput"
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
                onClick={handleShowModal} // Изменено на handleShowModal для открытия модального окна
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