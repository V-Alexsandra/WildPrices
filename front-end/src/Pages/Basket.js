import React, { useState, useEffect } from 'react';
import axios from 'axios'
import Header from '../Components/Header';
import { Container, Row, Col } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import look from '../Images/look.png';
import deleteicon from '../Images/deleteicon.png';
import '../Styles/Basket.css';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Modal, Button } from 'react-bootstrap';

const Basket = () => {
    const [showModal, setShowModal] = useState(false);
    const [selectedArticle, setSelectedArticle] = useState(null);

    const handleProductClick = (article, link, name, isDesiredPrice, currentPrice) => {
        localStorage.setItem('selectedProduct', JSON.stringify({ article, link, name, isDesiredPrice, currentPrice }));
        document.location = "http://localhost:3000/productcard"
    };

    function handleMouseEnter(event) {
        event.target.classList.add('icon-hover');
    }

    function handleMouseLeave(event) {
        event.target.classList.remove('icon-hover');
    }

    const [products, setProducts] = useState([]);
    const [url, setUrl] = useState('https://localhost:8443/api/Product/getAllProducts');
    const [selectedFilter, setSelectedFilter] = useState('Все товары');

    const handleDropdownSelect = (eventKey) => {
        switch (eventKey) {
            case '4.1':
                setUrl('https://localhost:8443/api/Product/getAllProducts');
                setSelectedFilter('Все товары');
                break;
            case '4.2':
                setUrl('https://localhost:8443/api/Product/getAllIsDesiredProducts');
                setSelectedFilter('Достигшие желаемой стоимости');
                break;
            case '4.3':
                setUrl('https://localhost:8443/api/Product/getAllIsNotDesiredProducts');
                setSelectedFilter('Не достигшие желаемой стоимости');
                break;
            default:
                break;
        }
    };

    const handleClickDostigle = () => {
        setUrl('https://localhost:8443/api/Product/getAllIsDesiredProducts');
        setSelectedFilter('Достигшие желаемой стоимости');
    };

    const handleDelete = (article) => {
        const urld = `https://localhost:8443/api/Product/${article}`;

        axios
            .delete(urld)
            .then(() => {
                setProducts(products.filter((product) => product.article !== article));
                window.location.href = '/basket';
            })
            .catch((error) => {
                alert(error);
            });
    };

    const handleCloseModal = () => {
        setSelectedArticle(null);
        setShowModal(false);
    };

    const handleShowModal = (article) => {
        setSelectedArticle(article);
        setShowModal(true);
    };

    const handleUndo = () => {
        handleCloseModal();
    }

    useEffect(() => {
        axios.get(url)
            .then((result) => {
                if (result.status === 200) {
                    setProducts(result.data);
                }
            })
            .catch((error) => {
                alert(error);
            });
    }, [url]);

    const handleSubmit = (article) => {
        handleDelete(article);
    };

    const renderBasket = () => {
        return products.map((product) => {
            let isDesiredPrice = false;

            if (product.desiredPrice >= product.currentPrice || (product.currentPrice > product.desiredPrice && product.currentPrice <= product.desiredPrice + 1)) {
                isDesiredPrice = true;
            } else {
                isDesiredPrice = false;
            }

            return (
                <>
                    <Container key={product.id}>
                        <Row>
                            <Card body className='basketcard'>
                                <Container>
                                    <Row>
                                        <Col sm={4} md={6} lg={8}>
                                            <a href={product.link} target="_blank" rel="noopener noreferrer" className='name'>{product.name}</a>
                                        </Col>
                                        <Col>
                                            <img src={look}
                                                onMouseEnter={handleMouseEnter}
                                                onMouseLeave={handleMouseLeave} alt="Просмотр" width="24" height="24" className=""
                                                onClick={() => handleProductClick(product.article, product.link, product.name, isDesiredPrice, product.currentPrice)}></img>
                                            <img src={deleteicon}
                                                onMouseEnter={handleMouseEnter}
                                                onMouseLeave={handleMouseLeave}
                                                alt="Удалить" width="21" height="21" className=""
                                                onClick={() => handleShowModal(product.article)}></img>
                                        </Col>
                                    </Row>
                                </Container>
                                <br></br>
                                <Container>
                                    <Row>
                                        <Col>
                                        </Col>
                                        <Col>
                                        </Col>
                                        <Col>
                                            <p className='text'>Текущая стоимость: <b>{product.currentPrice}</b> руб.</p>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col>
                                            <p className='text'>Артикул: {product.article}</p>
                                        </Col>
                                        <Col lg={4}>
                                            <p className='text'>Статус: {isDesiredPrice ? 'достиг желаемой стоимости' : 'не достиг желаемой стоимости'}</p>
                                        </Col>
                                        <Col>
                                            <p className='text'>Желаемая стоимость: <b>{product.desiredPrice}</b> руб.</p>
                                        </Col>
                                    </Row>
                                </Container>
                            </Card>
                        </Row>
                        <br></br>
                    </Container>
                </>
            )
        });
    }

    return (
        <>
            <Modal
                show={showModal}
                onHide={handleCloseModal}
                style={{ fontFamily: 'Franklin Gothic Book, sans-serif', fontSize: '12px', color: '#150056' }}
            >
                <Modal.Header closeButton>
                    <Modal.Title style={{fontFamily: 'Franklin Gothic Book, sans-serif', fontSize: '16px', color: '#150056'}}>Вы действительно хотите удалить отслеживаемый товар?</Modal.Title>
                </Modal.Header>
                <Modal.Footer>
                    <Button
                        variant="primary"
                        style={{ borderStyle: 'none', backgroundColor: '#6233F8', color: 'white' }}
                        onClick={() => handleSubmit(selectedArticle)}
                    >
                        Удалить
                    </Button>
                    <Button variant="primary" style={{borderStyle: 'none', backgroundColor: '#CBBEF5', color: '#6233F8' }} onClick={handleUndo}>
                        Отменить
                    </Button>
                </Modal.Footer>
            </Modal>
            <Header />
            <br></br>
            <Container>
                <Row>
                    <Col>
                        <NavDropdown title="Фильтр" id="nav-dropdown" className='navdropdown' onSelect={handleDropdownSelect}>
                            <NavDropdown.Item className='navdropdown' eventKey="4.1">Все товары</NavDropdown.Item>
                            <NavDropdown.Item id="dots-costs" onClick={handleClickDostigle} className='navdropdown' eventKey="4.2">Достигшие желаемой стоимости</NavDropdown.Item>
                            <NavDropdown.Item className='navdropdown' eventKey="4.3">Не достигшие желаемой стоимости</NavDropdown.Item>
                        </NavDropdown>
                        <p className='filtertext'>{selectedFilter}</p>
                    </Col>
                </Row>
            </Container>
            {renderBasket()}
        </>
    );
}

export default Basket;