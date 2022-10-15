import { Container, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";
import "./styles/Header.css";

export function Header(){
    return <>
        <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand as={Link} to="/">PMSOFT</Navbar.Brand>
          <Nav className="me-auto nav">
            <Nav.Link className="books" as={Link} to="/books">Books</Nav.Link>
            <Nav.Link className="login" as={Link} to="/auth">Login</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </>
}