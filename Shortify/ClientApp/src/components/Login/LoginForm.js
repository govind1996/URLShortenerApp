import React from "react";
import { Redirect, Link } from "react-router-dom";
import { Button, Container, Form, FormGroup, Label, Input } from "reactstrap";
import "../../styles/style.scss";
import { validateUsername } from "../Common/validations";
import MessageAlert from "../Common/MessageAlert";

class LoginForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			username: {
				value: "",
				isValid: false,
				touched: false,
			},
			password: {
				value: "",
				isValid: false,
				touched: false,
			},
			error: "",
		};
	}
	usernameOnChange = (e) => {
		const username = e.target.value;
		const isValid = validateUsername(username);
		this.setState({
			username: {
				value: username,
				isValid: isValid,
				touched: true,
			},
		});
	};
	passwordOnChange = (e) => {
		let password = e.target.value;
		let isValid = password === "" ? false : true;
		this.setState({
			password: {
				value: password,
				isValid: isValid,
				touched: true,
			},
		});
	};
	handleOnSubmit = (e) => {
		e.preventDefault();

		const username = this.state.username;
		const password = this.state.password;
		const isFormValid = username.isValid && password.isValid;
		if (isFormValid) {
			this.setState({
				error: "",
			});
			this.props.onSubmit(username.value, password.value);
		} else {
			this.setState({
				error: "Please enter all the required fields",
			});
		}
	};
	render() {
		let form = (
			<Container className="registerContainer">
				<div>
					<h3 className="header">Login</h3>
					{this.props.error.length > 0 ? (
						<MessageAlert
							visible={true}
							message={this.props.error}
						/>
					) : (
						false
					)}

					{this.state.error && (
						<MessageAlert
							visible={true}
							message={this.state.error}
						/>
					)}
					<Form onSubmit={this.handleOnSubmit}>
						<FormGroup row>
							<Label for="Username" size="sm">
								Username
							</Label>
							<Input
								onChange={this.usernameOnChange}
								value={this.state.username.value}
								type="text"
								name="Username"
								id="Email"
								placeholder="Username"
								bsSize="lg"
								invalid={
									this.state.username.touched &&
									!this.state.username.isValid
								}
							/>
						</FormGroup>
						<FormGroup row>
							<Label for="Password" size="sm">
								Password
							</Label>
							<Input
								onChange={this.passwordOnChange}
								value={this.state.password.value}
								type="password"
								name="Password"
								id="Password"
								placeholder="Password"
								bsSize="lg"
								invalid={
									this.state.password.touched &&
									!this.state.password.isValid
								}
							/>
						</FormGroup>
						<FormGroup row>
							<Button outline color="success" size="lg" block>
								Login
							</Button>
						</FormGroup>
					</Form>
					<p>
						<Link to="/app/register">
							{"Don't have an account? Sign Up"}
						</Link>
					</p>
				</div>
			</Container>
		);

		if (this.props.isAuthorized) {
			return <Redirect to={this.props.redirectPath} />;
		}
		// if (this.props.error != "") {
		// 	return <Redirect to={this.props.redirectPath} />;
		// }
		return form;
	}
}

export default LoginForm;
