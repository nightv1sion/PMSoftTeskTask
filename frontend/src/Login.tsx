import axios from "axios";
import { useFormik } from "formik"
import { useState } from "react";
import { Button } from "react-bootstrap";
import { UserForLoginDto } from "./interfaces";
import "./styles/Login.css";

export default function Login(props: loginProps){
    
    const [error, setError] = useState<string>("");

    const saveJWT = (token: string) => {
        localStorage.setItem("jwt", token);
        props.setToken();
    }

    const loginUser = () => {
        const user = formik.values;
        const baseUrl = process.env.REACT_APP_API;
        const url = "authentication/login";
        axios({
            method: "POST",
            baseURL: baseUrl,
            data: JSON.stringify(user),
            url: url,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(response => {saveJWT(response.data)})
        .catch(error => {const err = error.toJSON(); 
            if(err.status==400) setError("Wrong username or password");
            else setError("Something went wrong when posting to the server");});
    }
    
    const validate = (values: UserForLoginDto) => {
        const errors: any = {};
        if(!values.userName)
            errors.userName = "Username is required";
        if(!values.password)
            errors.password = "Password is required";
        return errors;
    }
    
    const isValidForm = () => {
         if(formik.errors.userName || formik.errors.password)
            return false;
        return true;
    }

    const formik = useFormik({
        initialValues: {
            userName: "",
            password: ""
        },
        onSubmit: loginUser,
        validate
    })
    
    return <>
        <form onSubmit={formik.handleSubmit} className="form-control">
        <h2>Login</h2>
        <div className="text-center text-danger">{error}</div>
        {formik.errors.userName ? <label htmlFor="year" className="text-danger">Username is required</label> : <label htmlFor="year">Username</label>}
            <input className="form-control"
                id="userName"
                name="userName"
                onChange={formik.handleChange}
                value={formik.values.userName}/>

            {formik.errors.password ? <label htmlFor="password" className="text-danger">Password is required</label> : <label htmlFor="password">Password</label>}
            <input className="form-control"
                id="password"
                name="password"
                type="password"
                onChange={formik.handleChange}
                value={formik.values.password}/>

        <div className="login-button">
            <Button type="submit" onClick={() => formik.handleSubmit()} className="form-control" disabled={!isValidForm()}>Login</Button>
        </div>
        </form>
    </>
}

interface loginProps {
    setToken: () => void; 
}