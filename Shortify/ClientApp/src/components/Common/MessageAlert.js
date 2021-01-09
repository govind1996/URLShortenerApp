import React, { useState } from "react";
import { Alert } from "reactstrap";

const MessageAlert = (props) => {
	const [visible, setVisible] = useState(props.visible);

	const onDismiss = () => setVisible(false);

	return (
		<Alert color="danger" isOpen={visible} toggle={onDismiss}>
			{props.message}
		</Alert>
	);
};

export default MessageAlert;
