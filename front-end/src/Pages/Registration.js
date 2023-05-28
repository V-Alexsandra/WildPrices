import React, { useState } from 'react';
import axios from 'axios';
import WildPricesNav from '../Components/WildPricesNav';
import '../Styles/Register.css';
import Card from 'react-bootstrap/Card';
import { Container, Row, Col } from 'react-bootstrap';
import Form from 'react-bootstrap/Form';

const Registration = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [emailError, setEmailError] = useState(false);
    const [passwordError, setPasswordError] = useState(false);
    const [passwordRepeatError, setPasswordRepeatError] = useState(false);

    function handleMouseEnter(event) {
        event.target.classList.add('but-hover');
    }

    function handleMouseLeave(event) {
        event.target.classList.remove('but-hover');
    }

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
        setPasswordError(!isValidPassword(value));
    };

    const isValidPassword = (password) => {
        const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&{}\[\]()\-])[A-Za-z\d@$!%*#?&{}\[\]()\-]{8,}$/;
        return passwordRegex.test(password);
    };

    const handleRepeatPasswordChange = (value) => {
        setRepeatPassword(value);
        setPasswordRepeatError(value !== password);
    }

    const handleRegistration = () => {
        const data = {
            Email: email,
            Password: password,
            RepeatPassword: repeatPassword
        };

        const url = 'https://localhost:8443/api/Auth/register';
        axios.post(url, data)
            .then((result) => {
                if (result.status === 200) {
                    document.location = "/";
                }
            })
            .catch((error) => {
                alert("Аккаунт не создан. Проверьте введенные данные.");
            })
    }

    return (
        <>
            <WildPricesNav /><br></br>
            <Container>
                <Row className="my-auto">
                    <Col className="text-center">
                        <p className="caption">Регистрация</p>
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
                                <Form.Control
                                    className={`input ${passwordError ? 'input-error' : ''}`}
                                    type="password"
                                    onChange={(e) => handlePasswordChange(e.target.value)}
                                />
                                {passwordError && (
                                    <p className="error-message">
                                        Введенный пароль должен содержать не менее 8 символов, включая минимум 1 букву, 1 цифру и 1 специальный символ.
                                    </p>
                                )}
                            </div>
                            <div className="input-gr">
                                <label className='text'>Повторите пароль</label><br></br>
                                <Form.Control
                                    className={`input ${passwordRepeatError ? 'input-error' : ''}`}
                                    type="password"
                                    onChange={(e) => handleRepeatPasswordChange(e.target.value)}
                                />
                                {passwordRepeatError && (
                                    <p className="error-message">
                                        Пароли не совпадают.
                                    </p>
                                )}
                            </div>
                        </Card>
                    </Col>
                </Row>
            </Container>
            <Container>
                <Row>
                    <Col sm={5} md={{ span: 5, offset: 2 }} lg={{ span: 3, offset: 2 }} ><p className='buttomactive' onClick={handleRegistration} onMouseEnter={handleMouseEnter}
                        onMouseLeave={handleMouseLeave} type="submit">Создать аккаунт</p></Col>
                    <Col sm={{ span: 3, offset: 4 }} md={{ span: 3, offset: 2 }} lg={{ span: 4, offset: 3 }}><p className='buttom' onMouseEnter={handleMouseEnter}
                        onMouseLeave={handleMouseLeave} onClick={() => { document.location = "http://localhost:3000" }}>Авторизация</p></Col>
                </Row>
            </Container>
        </>
    );
}

export default Registration;