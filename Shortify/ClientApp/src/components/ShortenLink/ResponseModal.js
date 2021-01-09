import React, { useState } from "react";
import { connect } from "react-redux";
import { Button, Modal, ModalBody, ModalFooter } from "reactstrap";
import { setResponse } from "../../actions/notification";
import "../../styles/style.scss";
import IconButton from "@material-ui/core/IconButton";
import FileCopyIcon from "@material-ui/icons/FileCopy";
import { CopyToClipboard } from "react-copy-to-clipboard";
import Tooltip from "@material-ui/core/Tooltip";

const ResponseModal = (props) => {
	const [open, setOpen] = useState(props.isOpen);

	const toggle = () => {
		setOpen(!open);
		//removing response
		props.dispatchSetResponse(undefined);
	};

	return (
		<div>
			<Modal isOpen={open}>
				<ModalBody className="responseBody">
					{`https://localhost:44322/${props.shortUrl}`}
					<CopyToClipboard
						text={`https://localhost:44322/${props.shortUrl}`}
					>
						<Tooltip title="Copy">
							<IconButton style={{}} aria-label="copy">
								<FileCopyIcon />
							</IconButton>
						</Tooltip>
					</CopyToClipboard>
				</ModalBody>
				<ModalFooter>
					<Button color="primary" onClick={toggle}>
						Close
					</Button>
				</ModalFooter>
			</Modal>
		</div>
	);
};

const mapDispatchToProps = (dispatch) => ({
	dispatchSetResponse: (response) => dispatch(setResponse(response)),
});
export default connect(undefined, mapDispatchToProps)(ResponseModal);
