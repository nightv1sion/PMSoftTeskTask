import axios from "axios";
import { FormikErrors, useFormik } from "formik";
import { FormEvent, useEffect, useState } from "react";
import { Book, BookForUpdateDto } from "./interfaces";
import ModalWindow from "./ModalWindow";

export default function UpdateBookModal(props: updateBookModalProps){

    const [errorMessage, setErrorMessage] = useState<string>("");

    const updateBook = () => {
        let token = props.getToken();
        if(!token)
        {
            props.onHide();
            props.setToken();
        }
        const book:BookForUpdateDto = formik.values;
        const baseUrl = process.env.REACT_APP_API;
        const url = "book/" + formik.values.id;

        axios({method: "PUT", baseURL: baseUrl, url: url, data: JSON.stringify(book), headers: {"Authorization": "Bearer " + token, "Content-Type": "application/json"}})
        .then(() => {props.getBooks(); props.onHide()})
        .catch((error) => { setErrorMessage("Something went wrong when putting to the server"); setTimeout(() => {setErrorMessage("")}, 5000); props.setToken();});
    }

    const validate = (values: Book) => {
        const errors:FormikErrors<Book>= {};
        if(!values.name)
            errors.name = "Required";
        if(values.year <= 0)
            errors.year = "Required and must be above than zero";
        if(!values.genre)
            errors.genre = "Required";
        if(!values.author)
            errors.author = "Required";
        return errors;
    }

    const formik = useFormik({
        initialValues: {
            id: props.book.id,
            name: props.book.name,
            year: props.book.year,
            genre: props.book.genre,
            author: props.book.author
        },
        onSubmit: updateBook,
        validate
    });

    useEffect(() => {
        formik.setValues({
            id: props.book.id,
            name: props.book.name,
            year: props.book.year,
            genre: props.book.genre,
            author: props.book.author
        });
    }, [props.book, props.showModal]);

    return <ModalWindow title={"Edit '" + props.book.name + "' book"} 
                onHide={props.onHide} 
                onSubmit={() => formik.handleSubmit()} 
                isSubmitDisabled={() => { 
                    return (formik.errors.author || formik.errors.name || formik.errors.genre || formik.errors.year || formik.values.year <= 0) ? true : false; }} 
                    showModal={props.showModal}>
        <form onSubmit={formik.handleSubmit}>
        {errorMessage ? <div className="text-danger text-center">{errorMessage}</div> : <></>}
        
        {formik.errors.name ? <label htmlFor="name" className="text-danger">Name is required</label> : <label htmlFor="name">Name</label>}

       <input className="form-control"
            id="name"
            name="name"
            onChange={formik.handleChange}
            value={formik.values.name}
        />
        {formik.errors.year ? <
            label htmlFor="year" className="text-danger">Year is required and must be more than zero</label> 
            : <label htmlFor="year">Year</label>}
        
        <input className="form-control"
            id="year"
            name="year"
            type="number"
            onChange={formik.handleChange}
            value={formik.values.year}
        />
        {formik.errors.genre ? 
            <label htmlFor="genre" className="text-danger">Genre is required</label> 
            : <label htmlFor="genre">Genre</label>}

        <input className="form-control"
            id="genre"
            name="genre"
            onChange={formik.handleChange}
            value={formik.values.genre}
        />

        {formik.errors.author ? 
            <label htmlFor="author" className="text-danger">Author is required</label> 
            : <label htmlFor="author">Author</label>}
        <input className="form-control"
            id="author"
            name="author"
            onChange={formik.handleChange}
            value={formik.values.author}
        />
        </form>
    </ModalWindow>
}

interface updateBookModalProps {
    book: Book;
    onHide: () => void;
    showModal: boolean;
    getBooks: () => void;
    getToken: () => string | null;
    setToken: () => void;
    books: Book[];
}