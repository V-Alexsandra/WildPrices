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

    function handleMouseEnter(event) {
        event.target.classList.add('but-hover');
    }

    function handleMouseLeave(event) {
        event.target.classList.remove('but-hover');
    }

    const handleEmailChange = (value) => {
        setEmail(value);
    }

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handleRepeatPasswordChange = (value) => {
        setRepeatPassword(value);
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
                alert(error);
            })
    }

    return (
        <>
            <WildPricesNav /><br></br><br></br>
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
                                <Form.Control className='input' type="email" placeholder="name@example.com" id="txtName" 
                                required 
                                onChange={(e) => handleEmailChange(e.target.value)} />
                            </div>
                            <div className="input-gr">
                                <label className='text'>Пароль</label><br></br>
                                <Form.Control className='input' type="password" 
                                onChange={(e) => handlePasswordChange(e.target.value)} />
                            </div>
                            <div className="input-gr">
                                <label className='text'>Повторите пароль</label><br></br>
                                <Form.Control className='input' type="password" onChange={(e) => handleRepeatPasswordChange(e.target.value)} />
                            </div>
                        </Card>
                    </Col>
                </Row>
            </Container>
            <Container>
                <Row>
                    <Col sm={5} md={{ span: 5, offset: 2 }} lg={{ span: 3, offset: 2 }} ><p className='buttomactive' onClick={handleRegistration}  onMouseEnter={handleMouseEnter}
                                            onMouseLeave={handleMouseLeave} type="submit">Создать аккаунт</p></Col>
                    <Col sm={{ span: 3, offset: 4 }} md={{ span: 3, offset: 2 }} lg={{ span: 4, offset: 3 }}><p className='buttom'  onMouseEnter={handleMouseEnter}
                                            onMouseLeave={handleMouseLeave} onClick={() => { document.location = "http://localhost:3000" }}>Авторизация</p></Col>
                </Row>
            </Container>
        </>
    );
}

export default Registration;