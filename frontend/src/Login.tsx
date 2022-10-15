import axios from "axios";
import { useFormik } from "formik"
import { useState } from "react";
import { UserForLoginDto } from "./interfaces";
import "./styles/Login.css";

export default function Login(){
    
    const [error, setError] = useState<string>("");

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
        }).then(response => {console.log(response)})
        .catch(error => {const err = error.toJSON(); console.log(err); 
            if(err.status==400) setError("Wrong username or password");
            else setError("Something went wrong when posting to the server");});
    }
    
    const formik = useFormik({
        initialValues: {
            userName: "",
            password: ""
        },
        onSubmit: loginUser
    })
    
    return <>
        <form onSubmit={formik.handleSubmit} className="form-control">
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
            <button type="submit" onClick={() => formik.handleSubmit()} className="form-control">Login</button>
        </div>
        </form>
    </>
}