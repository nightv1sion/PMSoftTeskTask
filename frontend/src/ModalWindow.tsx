import { FormEvent, useEffect } from 'react';
import {Button, Modal} from 'react-bootstrap'
import { Book } from './interfaces';
export default function ModalWindow(props: modalWindowProps){

    useEffect(() => {}, [props.children]);
    
    return <>
        <Modal
      aria-labelledby="contained-modal-title-vcenter"
      centered
      show={props.showModal}>
      <Modal.Header>
        <Modal.Title id="contained-modal-title-vcenter">
          {props.title}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {props.children}
      </Modal.Body>
      <Modal.Footer>
        {props.isSubmitDisabled() ? <Button variant="outline-success" disabled>Submit</Button> : 
        <Button onClick={props.onSubmit} variant="outline-success">Submit</Button>}
        <Button onClick={props.onHide} variant="outline-secondary" >Close</Button>
      </Modal.Footer>
    </Modal>
    </>
}

interface modalWindowProps {
  title: string;
  onHide: () => void;
  onSubmit: () => void;
  children: JSX.Element;
  showModal: boolean;
  isSubmitDisabled: () => boolean;
}