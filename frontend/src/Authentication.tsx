import { useState } from "react";
import { Button } from "react-bootstrap";
import { Link, Route, Router, Routes } from "react-router-dom";
import Login from "./Login";
import "./styles/Authentication.css";

export default function Authentication(){
    
    const [toggleForm, setToggleForm] = useState<boolean>(false);
    
    return <>

        <div className="switcher">
            <div className="login-link">
                <Button variant="outline-secondary" onClick={() => setToggleForm(!toggleForm)}>{toggleForm == false ? "Go to Register Form" : "Go to Login Form"}</Button>
            </div>
        </div>
        <div className="auth">
            {toggleForm == false ? <Login></Login> : <div>Register</div>}
        </div>
    </>
} 