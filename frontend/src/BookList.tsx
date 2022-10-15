import { useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import { Book } from "./interfaces";
import ModalWindow from "./ModalWindow";
import "./styles/BookList.css";
import UpdateBookModal from "./UpdateBookModal";

export default function BookList(props: bookListProps){

    const [updateBookModalForms, setUpdateBookModalForms] = useState<boolean[]>(new Array(props.books.length).fill(false));

    useEffect(() => {
        props.getBooks();
    }, [])

    return <div className="booklist">
        
        <h1>Books Table</h1>
        <Button>Create New</Button>
        <Table striped bordered hover variant="dark" className="w-75">
      <thead>
        <tr>
          <th>Name</th>
          <th>Year</th>
          <th>Genre</th>
          <th>Author</th>
          <th>Operations</th>
        </tr>
      </thead>
      <tbody>
        {props.books.map((book, index) =>
            <tr key={index}>
                <td>{book.name}</td>
                <td>{book.year}</td>
                <td>{book.genre}</td>
                <td>{book.author}</td>
                <td>
                     <div className="operations">
                        <div>
                            <Button variant="outline-primary" className="w-75" onClick={() => {
                                let arr = updateBookModalForms.slice();
                                arr[index] = true; setUpdateBookModalForms(arr);}}>Edit</Button>
                            <UpdateBookModal book={book} onHide={() => {
                                let arr = updateBookModalForms.slice(); 
                                arr[index] = false;
                                setUpdateBookModalForms(arr);}} showModal={updateBookModalForms[index]} getBooks={props.getBooks}></UpdateBookModal>
                        </div>
                        <div>
                            <Button variant="outline-danger" className="w-75">Delete</Button>  
                        </div>
                    </div> 
                </td>
            </tr>)}
      </tbody>
    </Table>
    </div>
}

interface bookListProps {
    books: Book[],
    getBooks: () => void;
}