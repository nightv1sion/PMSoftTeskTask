import { useEffect, useState} from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useJwt } from "react-jwt";
import { Link } from "react-router-dom";
import { User } from "./interfaces";
import "./styles/Header.css";

export function Header(props: headerProps){

    const [user, setUser] = useState<User | null>(props.getUser());

    useEffect(() => {setUser(props.getUser())}, [props.token]);

    return <>
        <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand as={Link} to="/">PMSOFT</Navbar.Brand>
          <Nav className="me-auto nav">
            <Nav.Link className="books" as={Link} to="/books">Books</Nav.Link>
            <Nav.Link className="login" as={Link} to="/auth">{user==null ? "Login" : user.userName}</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </>
}

interface headerProps {
    getUser:() => User | null;
    setToken: () => void;
    token: string | null;
}