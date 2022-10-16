import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useJwt } from "react-jwt";
import { Link, Route, Router, Routes } from "react-router-dom";
import { User } from "./interfaces";
import Login from "./Login";
import Register from "./Register";
import "./styles/Authentication.css";
import UserProfile from "./UserProfile";

export default function Authentication(props: authenticationProps){

    const user = props.getUser();

    const setUpToken = () => {
        props.setToken();
    }

    const [toggleForm, setToggleForm] = useState<boolean>(false);

    return <>
        {user != null ? <div className="user-profile"><UserProfile user={user} setToken={props.setToken}></UserProfile></div> : <>
        <div className="switcher">
            <div className="login-link">
                <Button variant="outline-secondary" onClick={() => setToggleForm(!toggleForm)}>
                    {toggleForm == false ? "Go to Register Form" : "Go to Login Form"}
                </Button>
            </div>
        </div>
        <div className="auth">
            {toggleForm == false ? <Login setToken={() => setUpToken()}></Login> : <Register toggleToLogin={() => setToggleForm(!toggleForm)}></Register>}
        </div></>}
    </>
} 

interface authenticationProps {
    setToken: () => void;
    getUser: () => User | null;
}