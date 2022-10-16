import { Button } from "react-bootstrap";
import { User } from "./interfaces";

export default function UserProfile(props: userProfileProps){

    const logOut = () => {
        localStorage.removeItem("jwt");
        props.setToken();
    }

    return  <>
        <h2>Profile</h2>
        <div className="form-control">
            <div>Username: {props.user.userName}</div>
            <div>Role: {props.user.role ? props.user.role : "simple user"}</div>
            <Button variant="outline-danger" onClick={logOut}>Log Out</Button>
        </div>
    </>
    
}

interface userProfileProps {
    user: User;
    setToken: () => void;
}