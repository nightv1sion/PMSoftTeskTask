import axios from "axios";
import { FormikErrors, useFormik } from "formik";
import { useEffect, useState } from "react";
import { Book, BookForCreateDto, BookForUpdateDto } from "./interfaces";
import ModalWindow from "./ModalWindow";

export default function CreateBookModal(props: createBookModalProps){
    const [errorMessage, setErrorMessage] = useState<string>("");

    const createBook = () => {
        let token = props.getToken();
        if(!token)
        {
            props.onHide();
            props.setToken();
        }
        const book:BookForUpdateDto = formik.values;
        const baseUrl = process.env.REACT_APP_API;
        const url = "book/";
        axios({method: "POST", baseURL: baseUrl, url: url, data: JSON.stringify(book), headers: {"Authorization": "Bearer " + token, "Content-Type": "application/json"}})
        .then(() => {props.getBooks(); clearFormikValues(); props.onHide()})
        .catch((error) => { setErrorMessage("Something went wrong when putting to the server"); setTimeout(() => {setErrorMessage("")}, 5000); props.setToken();});
    }

    const clearFormikValues = () => {
        formik.setValues({
            name: "",
            year: 0,
            genre: "",
            author: ""
        });
        formik.setTouched({
            name: false,
            year:false,
            genre: false,
            author: false
        });
    }

    const validate = (values: BookForCreateDto) => {
        const errors:FormikErrors<BookForCreateDto>= {};
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
            name: "",
            year: 0,
            genre: "",
            author: ""
        },
        onSubmit: createBook,
        validate
    });

    useEffect(() => {}, [props.showModal]);

    return <ModalWindow title={"Create book"} onHide={props.onHide} onSubmit={() => formik.handleSubmit()} 
    isSubmitDisabled={() => { return (formik.errors.author || formik.errors.name || formik.errors.genre || formik.errors.year || formik.values.year <= 0) ? true : false; }} showModal={props.showModal}>
        <form onSubmit={formik.handleSubmit}>
        {errorMessage ? <div className="text-danger text-center">{errorMessage}</div> : <></>}
        
        {formik.errors.name ? <label htmlFor="name" className="text-danger">Name is required</label> : <label htmlFor="name">Name</label>}

       <input className="form-control"
            id="name"
            name="name"
            onChange={formik.handleChange}
            value={formik.values.name}
        />
        {formik.errors.year ? <label htmlFor="year" className="text-danger">Year is required and must be more than zero</label> : <label htmlFor="year">Year</label>}
        
        <input className="form-control"
            id="year"
            name="year"
            onChange={formik.handleChange}
            type="number"
            value={formik.values.year != 0 ? formik.values.year : ""}
        />
        {formik.errors.genre ? <label htmlFor="genre" className="text-danger">Genre is required</label> : <label htmlFor="genre">Genre</label>}

        <input className="form-control"
            id="genre"
            name="genre"
            onChange={formik.handleChange}
            value={formik.values.genre}
        />

        {formik.errors.author ? <label htmlFor="author" className="text-danger">Author is required</label> : <label htmlFor="author">Author</label>}
        <input className="form-control"
            id="author"
            name="author"
            onChange={formik.handleChange}
            value={formik.values.author}
        />
        </form>
    </ModalWindow>
}

interface createBookModalProps {
    onHide: () => void;
    showModal: boolean;
    getBooks: () => void;
    getToken: () => string | null;
    setToken: () => void;
}