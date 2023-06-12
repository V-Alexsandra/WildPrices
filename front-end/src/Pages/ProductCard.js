import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import Header from '../Components/Header';
import change from '../Images/change2.png';
import '../Styles/ProductCard.css';
import { Modal, Button } from 'react-bootstrap';
import Form from 'react-bootstrap/Form';
import LineChart from '../Components/LineChart';

const ProductCard = () => {
  const [min, setMin] = useState([]);
  const [max, setMax] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [newDesiredPrice, setNewDesiredPrice] = useState('');
  const [desiredPrice, setDesiredPrice] = useState('');
  const [desiredPriceError, setDesiredPriceError] = useState(false);

  const [priceHistory, setPriceHistory] = useState([]);

  const [isDesiredPriceValue, setisDesiredPriceValue] = useState(false);

  const handleDesiredPriceChange = (event) => {
    const desiredPriceRegex = /^\d+(\.\d+)?$/;
    setNewDesiredPrice(event.target.value);
    setDesiredPriceError(!desiredPriceRegex.test(event.target.value));
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleShowModal = () => {
    setShowModal(true);
  };

  const handleLoad = (article) => {
    const url = `https://localhost:8443/min-and-max-price`;
    const params = {
      Article: article,
    };

    axios
      .get(url, { params })
      .then((result) => {
        if (result.status === 200) {
          setMin(result.data.minPrice);
          setMax(result.data.maxPrice);
        }
      })
      .catch((error) => {
        alert(error);
      });

    const url2 = `https://localhost:8443/api/PriceHistory`;
    axios
      .get(url2, { params })
      .then((result) => {
        if (result.status === 200) {
          setPriceHistory(result.data);
        }
      })
      .catch((error) => {
        alert(error);
      });

    localStorage.setItem('prices', JSON.stringify({ priceHistory }));

    const url1 = 'https://localhost:8443/api/Product/getdesiredprice';
    const params1 = {
      article: article,
    };

    axios
      .get(url1, { params: params1 })
      .then((result) => {
        if (result.status === 200) {
          setDesiredPrice(result.data);
        }
      })
      .catch((error) => {
        alert(error);
      });
  };

  useEffect(() => {
    handleLoad(article);

    if (desiredPrice >= currentPrice || (currentPrice > desiredPrice && currentPrice <= desiredPrice + 1)) {
      setisDesiredPriceValue(true);
    } else {
      setisDesiredPriceValue(false);
    }
  }, [desiredPrice]);

  const handleChangeClick = (article, newDesiredPrice) => {
    const url = `https://localhost:8443/updateDesiredPrice`;
    const params = {
      Article: article,
    };
    const data = {
      DesiredPrice: newDesiredPrice,
    };

    axios
      .put(url, data, { params })
      .then((result) => {
        if (result.status === 200) {
          document.location = '/productcard';
        }
      })
      .catch((error) => {
        alert(error);
      });
  };

  function handleMouseEnter(event) {
    event.target.classList.add('icon-hover');
  }

  function handleMouseLeave(event) {
    event.target.classList.remove('icon-hover');
  }

  const handleSubmit = () => {
    handleChangeClick(article, newDesiredPrice);
  };

  var { article, link, name, isDesiredPrice, currentPrice } = JSON.parse(localStorage.getItem('selectedProduct'));

  const formatPrice = (price) => {
    return parseFloat(price).toFixed(2);
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
              value={newDesiredPrice}
              onChange={handleDesiredPriceChange}
              className={`desired-price-input ${desiredPriceError ? 'input-error' : ''}`}
              style={{ backgroundColor: '#6233F8', color: 'white' }}
            />
            {desiredPriceError && (
              <p className="error-message">Пожалуйста, введите число (дробную часть через точку).</p>
            )}
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" style={{ backgroundColor: '#6233F8', color: 'white' }} onClick={handleSubmit}>
            Сохранить
          </Button>
        </Modal.Footer>
      </Modal>
      <Header />
      <div className="contmain"></div>
      <Container>
        <Row>
          <Col>
            <Container className="cont">
              <p className="title">Стоимость отслеживаемого товара:</p>
              <div class="card-body basketcardP">
                <Container>
                  <Row>
                    <Col>
                      <a href={link} target="_blank" rel="noopener noreferrer" className="nameP">
                        {name}
                      </a>
                    </Col>
                  </Row>
                  <br></br>
                </Container>
                <Container>
                  <Row>
                    <Col>
                      <p className="textP">Артикул: {article}</p>
                    </Col>
                    <Col>
                      <p className="textP">
                        Статус: {isDesiredPriceValue ? 'достиг желаемой стоимости' : 'не достиг желаемой стоимости'}
                      </p>
                    </Col>
                  </Row>
                </Container>
              </div>
            </Container>
          </Col>
          <Col>
            <div class="card-body productcard">
              <Row>
                <Col>
                  <p className="pricetext">Минимальная: </p>
                </Col>
                <Col>
                  <p className="price">{formatPrice(min)} руб.</p>
                </Col>
              </Row>
            </div>
            <div class="card-body productcard">
              <Row>
                <Col>
                  <p className="pricetext">Максимальная: </p>
                </Col>
                <Col>
                  <p className="price">{formatPrice(max)} руб.</p>
                </Col>
              </Row>
            </div>
            <div class="card-body pricecard">
              <Row>
                <Col>
                  <p className="pricetext">Текущая: </p>
                </Col>
                <Col>
                  <p className="price">{formatPrice(currentPrice)} руб.</p>
                </Col>
              </Row>
            </div>
            <div class="card-body pricecard">
              <Row>
                <Col>
                  <p className="pricetext">Желаемая: </p>
                </Col>
                <Col>
                  <p className="price" style={{ paddingLeft: 35 }}> {formatPrice(desiredPrice)} руб.</p>
                </Col>
                <Col xs={2}>
                  <img
                    src={change}
                    onMouseEnter={handleMouseEnter}
                    onMouseLeave={handleMouseLeave}
                    onClick={handleShowModal}
                    alt="Изменить"
                    width="18"
                    height="19"
                    className=""
                    style={{ paddingBottom: 3 }}
                  ></img>
                </Col>
              </Row>
            </div>
          </Col>
        </Row>
        <Container>
          <Row>
            <div class="card-body grafcard">
              <LineChart priceHistory={priceHistory} />
            </div>
          </Row>
        </Container>
      </Container>
    </>
  );
};

export default ProductCard;