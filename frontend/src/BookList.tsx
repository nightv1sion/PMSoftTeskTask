import axios from "axios";
import { useEffect, useState } from "react";
import { Button, Form, Table } from "react-bootstrap";
import CreateBookModal from "./CreateBookModal";
import { Book, User } from "./interfaces";
import ModalWindow from "./ModalWindow";
import "./styles/BookList.css";
import UpdateBookModal from "./UpdateBookModal";

export default function BookList(props: bookListProps){

    const [updateBookModalForms, setUpdateBookModalForms] = useState<boolean[]>(new Array(props.books.length).fill(false));

    const [createBookModalForm, setCreateBookModalForm] = useState<boolean>(false);

    const user: User | null = props.getUser();

    const [choosedBooks, setChoosedBooks] = useState<boolean[]>(new Array(props.books.length).fill(false));

    const deleteBook = (book: Book) => {
        const token = props.getToken();
        const baseURL = process.env.REACT_APP_API;
        const url = "book/" + book.id;
        axios({method: "DELETE", baseURL, url, headers: {"Authorization": "Bearer " + token} })
        .then((response) => {props.getBooks();})
        .catch((error) => {
            if(error.toJSON().status == null)
                props.setToken();
        }); 
    }

    const deleteChoosedBooks = async () => {
        const token = props.getToken();
        const baseURL = process.env.REACT_APP_API;
        for(let i=0;i<choosedBooks.length; i++){
            if(choosedBooks[i] == true)
            {
                const url = "book/" + props.books[i].id;
                axios({method: "DELETE", baseURL, url, headers: {"Authorization": "Bearer " + token} })
                .then((response) => {props.getBooks(); 
                    setChoosedBooks((new Array(props.books.length).fill(false)));})
                .catch((error) => {
                    if(error.toJSON().status == null)
                        props.setToken();
                }); 
            }
        }
    }

    useEffect(() => {
        if(props.books.length == 0)
            props.getBooks();
        setChoosedBooks(new Array(props.books.length).fill(false));
    }, [])

    return <div className="booklist">
        <h1>Books Table</h1>
        <div className="buttons-containter">
            <div className="delete-choosed-books">
                {user && user.role=="admin" ? 
                <Button variant="danger" hidden={choosedBooks.filter(b => b==true).length < 1}
                 onClick={() => deleteChoosedBooks()}>Delete Choosed</Button> 
                 : null}
            </div>
            <div className="create-new-book">
                {user && user.role=="admin" ? 
                <Button onClick={() => setCreateBookModalForm(true)}>Create New</Button> 
                : null}
            </div>
        </div>
        <CreateBookModal onHide={() => setCreateBookModalForm(false)} showModal={createBookModalForm}
         getBooks={props.getBooks} getToken={props.getToken} setToken={props.setToken}></CreateBookModal>
        <Table striped bordered hover variant="dark" className="w-75">
        <thead>
            <tr>
                {user && user.role == "admin"? <th>#</th> : null}
            <th>Name</th>
            <th>Year</th>
            <th>Genre</th>
            <th>Author</th>
            {user && user.role=="admin" ? <th>Operations</th> : <></>}
            </tr>
        </thead>
        <tbody>
            {props.books.map((book, index) =>
                <tr key={index}>
                    {user && user.role == "admin" ? <td>
                        <Form.Check type="switch"
                        id="custom-switch" onChange={() => {
                            const temp = choosedBooks.slice();
                            temp[index] = !temp[index];
                            setChoosedBooks(temp);}}
                            defaultChecked={choosedBooks[index]}
                            checked={choosedBooks[index]}
                        ></Form.Check>
                    </td> : null}
                    <td>{book.name}</td>
                    <td>{book.year}</td>
                    <td>{book.genre}</td>
                    <td>{book.author}</td>
                    {user && user.role=="admin" ? <td>
                        <div className="operations">
                            <div>
                                <Button variant="outline-primary" className="w-75" onClick={() => {
                                    let arr = updateBookModalForms.slice();
                                    arr[index] = true; setUpdateBookModalForms(arr);}}>Edit</Button>
                                <UpdateBookModal book={book} 
                                    onHide={() => {
                                        let arr = updateBookModalForms.slice(); 
                                        arr[index] = false;
                                        setUpdateBookModalForms(arr);}} 
                                    showModal={updateBookModalForms[index]} 
                                    getBooks={props.getBooks} 
                                    getToken={props.getToken} 
                                    setToken={props.setToken}
                                    books={props.books}></UpdateBookModal>
                            </div>
                            <div>
                                <Button variant="outline-danger" className="w-75" onClick={() => deleteBook(book)}>Delete</Button>  
                            </div>
                        </div> 
                    </td> : <></>}
                </tr>)}
        </tbody>
    </Table>
    </div>
}

interface bookListProps {
    books: Book[];
    getBooks: () => void;
    getUser: () => User | null;
    getToken: () => string | null;
    setToken: () => void;
}