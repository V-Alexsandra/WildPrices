import React, { useState } from 'react';
import axios from 'axios'
import WildPricesNav from '../Components/WildPricesNav';
import '../Styles/Login.css';
import Card from 'react-bootstrap/Card';
import { Container, Row, Col } from 'react-bootstrap';
import Form from 'react-bootstrap/Form';

const Login = () => {
    function handleMouseEnter(event) {
        event.target.classList.add('but-hover');
    }

    function handleMouseLeave(event) {
        event.target.classList.remove('but-hover');
    }

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [emailError, setEmailError] = useState(false);

    const handleEmailChange = (value) => {
        setEmail(value);
        setEmailError(!isValidEmail(value));
    };

    const isValidEmail = (email) => {
        const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
        return emailRegex.test(email);
    };

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handleLogin = () => {
        const data = {
            Email: email,
            Password: password,
        };

        alert("Пожалуйста, подождите. Собираем Вашу корзину");

        const url = 'https://localhost:8443/api/Auth/login';
        axios.post(url, data).then((result) => {
            if (result.status === 200) {
                document.location = "/basket";
            }
        }).catch((error) => {
            alert(error);
        })
    }

    return (
        <>
            <WildPricesNav /><br></br><br></br>
            <Container>
                <Row className="my-auto">
                    <Col className="text-center">
                        <p className="caption">Авторизация</p>
                    </Col>
                </Row>
            </Container>
            <Container>
                <Row className="justify-content-center">
                    <Col md={8}>
                        <Card body className='form'>
                            <div className="input-gr">
                                <label className='text'>Электронная почта</label><br></br>
                                <Form.Control
                                    className={`input ${emailError ? 'input-error' : ''}`}
                                    type="email"
                                    placeholder="name@example.com"
                                    id="txtName"
                                    onChange={(e) => handleEmailChange(e.target.value)}
                                />
                                {emailError && <p className="error-message">Пожалуйста, введите корректный адрес электронной почты.</p>}
                            </div>
                            <div className="input-gr">
                                <label className='text'>Пароль</label><br></br>
                                <Form.Control className='input' type="password" onChange={(e) => handlePasswordChange(e.target.value)} />
                            </div>
                        </Card>
                    </Col>
                </Row>
            </Container>
            <Container>
                <Row>
                    <Col sm={3} md={{ span: 2, offset: 2 }}><p className='buttomactive' onClick={() => handleLogin()} onMouseEnter={handleMouseEnter}
                        onMouseLeave={handleMouseLeave}>Войти</p></Col>
                    <Col sm={{ span: 3, offset: 4 }} md={{ span: 3, offset: 4 }} lg={{ span: 3, offset: 4 }}><p className='buttom' onMouseEnter={handleMouseEnter}
                        onMouseLeave={handleMouseLeave} onClick={() => document.location = "http://localhost:3000/registration"}>Регистрация</p></Col>
                </Row>
            </Container>
        </>
    );
}

export default Login;