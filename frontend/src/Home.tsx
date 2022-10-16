import { ListGroup } from "react-bootstrap";
import "./styles/Home.css";
export default function Home(){
    return <>
        <div className="home">
            <h3>PMSOFT`s Test Task Completed By Danila Uprivanov</h3>
            <div className="requirements">
                <h4 className="requirements-header">Requirements:</h4>
                <ListGroup variant="flush">
                    <ListGroup.Item>Display a list of books with the following list of fields: ID, Title, Year, Genre, Author.</ListGroup.Item>
                    <ListGroup.Item>Implement the possibility to add a new book.</ListGroup.Item>
                    <ListGroup.Item>Implement the possibility to change the existing book.</ListGroup.Item>
                    <ListGroup.Item>Implement the possibility to delete the selected book/books.</ListGroup.Item>
                    <ListGroup.Item>Implement authorization</ListGroup.Item>
                </ListGroup>
            </div>
        </div>
    </>
}