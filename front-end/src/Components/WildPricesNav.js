import { Container, Navbar } from 'react-bootstrap';
import '../Styles/Header.css';

function WildPricesNav() {
  return (
    <>
      <Navbar className='head'>
        <Container>
          <Navbar.Brand className='logo'>WILDPRICES</Navbar.Brand>
        </Container>
      </Navbar>
    </>
  );
}

export default WildPricesNav;