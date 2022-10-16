import axios from "axios";
import { useFormik } from "formik";
import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { UserForRegisterDto } from "./interfaces";

export default function Register(props: registerProps){

    const [error, setError] = useState<string>();

    const registerUser = () => {
        const user = formik.values;
        const baseUrl = process.env.REACT_APP_API;
        const url = "authentication/register";
        axios({
            method: "POST",
            baseURL: baseUrl,
            data: JSON.stringify(user),
            url: url,
            headers: {
                "Content-Type": "application/json"
            },
        }).then(response => {props.toggleToLogin();})
        .catch(error => {const err = error.toJSON();  
            if(err.status==409) setError("Username is already in use");
            else setError("Something went wrong when posting to the server");});
    }

    const validate = (values: UserForRegisterDto) => {
        const errors: any = {};
        if(!values.userName)
            errors.userName = "Username is required";
        if(!values.password)
            errors.password = "Password is required";
        if(values.password != values.confirmPassword)
            errors.confirmPassword = "Password Confirmation must be the same as password";
        return errors; 
    }
    
    const isValidForm = () => {
         if(formik.errors.userName || formik.errors.password)
            return false;
        if(formik.values.password != formik.values.confirmPassword)
            return false;
        return true;
    }

    const formik = useFormik({
        initialValues: {
            userName: "",
            password: "",
            confirmPassword: "",
            isAdmin: false
        },
        onSubmit: registerUser,
        validate
    })
    
    return <>
        <form onSubmit={formik.handleSubmit} className="form-control">
        <h2>Register</h2>
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

            {formik.errors.confirmPassword ? <label htmlFor="confirmPassword" className="text-danger">Password Confirmation must be the same as the password</label> : 
            <label htmlFor="password">Password Confirmation</label>}
            <input className="form-control"
                id="confirmPassword"
                name="confirmPassword"
                type="password"
                onChange={formik.handleChange}
                value={formik.values.confirmPassword}/>
            
            <Form.Check type="switch"
                id="isAdmin" 
                name="isAdmin" onChange={formik.handleChange} label="Admin Mode (Can create, update, delete)"></Form.Check>

        <div className="login-button">
            <Button type="submit" onClick={() => formik.handleSubmit()} disabled={!isValidForm()}>Register</Button>
        </div>
        </form>
    </>
}

interface registerProps {
    toggleToLogin: () => void;
}