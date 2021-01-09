import React, { useState } from "react";
import { Button, Modal, ModalBody, ModalFooter } from "reactstrap";
import "../../styles/style.scss";

const AlertModal = (props) => {
	console.log(props.id);
	const [open, setOpen] = useState(props.isOpen);

	const toggle = () => {
		setOpen(!open);
		props.setDeleteState();
	};

	const deleteLink = () => {
		setOpen(!open);
		//deleting url
		props.deleteLink();
		props.setDeleteState();
	};

	return (
		<div>
			<Modal isOpen={open}>
				<ModalBody className="responseBody">
					{`Are you sure you want to delete this url?`}
				</ModalBody>
				<ModalFooter>
					<Button color="success" onClick={deleteLink}>
						Delete
					</Button>
					<Button color="primary" onClick={toggle}>
						Cancel
					</Button>
				</ModalFooter>
			</Modal>
		</div>
	);
};

export default AlertModal;
