import React, { useState, useEffect } from 'react';
import axios from 'axios'
import Header from '../Components/Header';
import { Container, Row, Col, Modal, Button, Form } from 'react-bootstrap';
import '../Styles/Card.css';
import change from '../Images/change.png';
import look from '../Images/look.png';
import Card from 'react-bootstrap/Card';

const Profile = () => {
    const [lookIcon, setLookIcon] = useState(look);

    const [products, setProducts] = useState("");
    const [profile, setProfile] = useState("");
    const [name, setName] = useState("");
    const [showModal, setShowModal] = useState(false);
    const [nameError, setnameError] = useState(false);

    const handleCloseModal = () => {
        setShowModal(false);
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleNameChange = (event) => {
        const nicknameRegex = /^[a-zA-Z0-9]+$/;
        setName(event.target.value);
        setnameError(!nicknameRegex.test(event.target.value));
    };

    const handleSubmit = () => {
            handleChangeName(name);
    };

    const handleChangeName = (name) => {
        const data = JSON.stringify(name);
        const url = 'https://localhost:8443/api/Auth';
        axios.put(url, data, {
            headers: {
                'Content-Type': 'application/json'
            }
        }).then((result) => {
            if (result.status === 200) {
                setProfile(result.data);
                document.location = "/profile";
            }
        }).catch((error) => {
            alert("Такой никнейм уже существует. Пожалуйста, напишите другой никнейм.");
        })
    }

    const handleProducts = () => {

        const url = 'https://localhost:8443/api/Product/countProducts';
        axios.get(url).then((result) => {
            if (result.status === 200) {
                setProducts(result.data);
            }
        }).catch((error) => {
            alert(error);
        })
    }

    const handleProfile = () => {

        const url = 'https://localhost:8443/api/Auth';
        axios.get(url).then((result) => {
            if (result.status === 200) {
                setProfile(result.data);
            }
        }).catch((error) => {
            alert(error);
        })
    }

    useEffect(() => {
        handleProfile();
        handleProducts();
    }, []);

    function handleMouseEnter(event) {
        event.target.classList.add('icon-hover');
    }

    function handleMouseLeave(event) {
        event.target.classList.remove('icon-hover');
    }

    return (
        <>
            <Modal
                show={showModal}
                onHide={handleCloseModal}
                style={{ fontFamily: 'Franklin Gothic Book, sans-serif', fontSize: '16px', color: '#150056' }}
            >
                <Modal.Header closeButton>
                    <Modal.Title>Введите новый никнейм</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group controlId="name">
                        <Form.Label style={{ color: '#150056' }}>Никнейм</Form.Label>
                        <Form.Control
                            type="text"
                            value={name}
                            onChange={handleNameChange}
                            className={`desired-price-input ${nameError ? 'input-error' : ''}`}
                            style={{ backgroundColor: '#6233F8', color: 'white' }}
                        />
                        {nameError && (
                            <p className="error-message">Никнейм может содержать только латинские буквы и цифры.</p>
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
            <br></br><br></br>
            <Container>
                <Row className="justify-content-center">
                    <Col md={12}>
                        <Card body className='card'>
                            <Container>
                                <Row>
                                    <Col md={4}>
                                        <p>Никнейм:</p>
                                    </Col>
                                    <Col md={3}>
                                        <p className='data'>{profile.userName}</p>
                                    </Col>
                                    <Col>
                                        <img src={change}
                                            onMouseEnter={handleMouseEnter}
                                            onMouseLeave={handleMouseLeave}
                                            alt="Изменение" width="20" height="20" className="" onClick={handleShowModal}></img>
                                    </Col>
                                </Row>
                            </Container>
                            <Container>
                                <Row>
                                    <Col md={4}>
                                        <p>Электронная почта:</p>
                                    </Col>
                                    <Col md={3}>
                                        <p className='data'>{profile.userEmail}</p>
                                    </Col>
                                </Row>
                            </Container>
                        </Card>
                    </Col>
                </Row>
            </Container>
            <br></br>
            <Container>
                <Row>
                    <Col md={12}>
                        <Card body className='card'>
                            <Container>
                                <Row>
                                    <Col md={5}>
                                        <p>Количество отслеживаемых товаров: </p>
                                    </Col>
                                    <Col md={3}>
                                        <p className='data'>{products.totalProductsCount}</p>
                                    </Col>
                                    <Col>
                                        <img
                                            src={lookIcon}
                                            onMouseEnter={handleMouseEnter}
                                            onMouseLeave={handleMouseLeave}
                                            alt="Просмотр"
                                            width="24" height="24" onClick={() => { document.location = "http://localhost:3000/basket" }}></img>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col md={5}>
                                        <p>Достигли желаемой стоимости: </p>
                                    </Col>
                                    <Col md={3}>
                                        <p className='data'>{products.isDesiredProductsCount}</p>
                                    </Col>
                                    {/* <Col>
                            <img src={look} alt="Просмотр" width="20" height="20" className=""
                            onClick={() => {(document.location = "http://localhost:3000/basket#dots-costs");}}></img>
                        </Col>*/}
                                </Row>
                            </Container>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    );
}

export default Profile;