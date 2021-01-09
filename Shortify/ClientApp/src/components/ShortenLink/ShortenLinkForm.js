import React from "react";
import { Button, Row, Col, Form, FormGroup, Label, Input } from "reactstrap";
import "../../styles/style.scss";
import ResponseModal from "./ResponseModal";
import { validateUrl } from "../Common/validations";

export default class ShortenLinkForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			url: "",
			isValid: false,
		};
	}

	onLinkChange = (e) => {
		const url = e.target.value;
		let isValid = validateUrl(url);
		this.setState(() => ({ url, isValid }));
	};
	handleOnSubmit = (e) => {
		e.preventDefault();
		const url = this.state.url;

		if (this.state.isValid) {
			console.log("submitted");
			this.setState(() => ({ url: "", isValid: false }));
			this.props.onSubmit(url, this.props.token);
		}
	};
	render() {
		let form = (
			// <div className="inputUrl">
			<Row xs="1" md="1" lg="1" className="inputUrl">
				<Col>
					<Form onSubmit={this.handleOnSubmit}>
						<FormGroup>
							<Label for="url" size="md">
								Enter url to shrink
							</Label>
							<Input
								onChange={this.onLinkChange}
								type="url"
								name="url"
								id="url"
								value={this.state.url}
								placeholder="https://shortify.com"
								bsSize="lg"
							/>
						</FormGroup>
						<FormGroup className="text-center">
							<Button
								outline
								color="success"
								size="lg"
								disabled={!this.state.isValid}
							>
								Shorten
							</Button>
						</FormGroup>
					</Form>
				</Col>
				<Col className="response">
					{this.props.response && (
						<ResponseModal
							isOpen={true}
							shortUrl={this.props.response.shortUrl}
						/>
					)}
				</Col>
			</Row>
			// </div>
		);
		return form;
	}
}
