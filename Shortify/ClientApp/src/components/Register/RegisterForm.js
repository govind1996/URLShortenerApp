import React from "react";
import { Redirect } from "react-router-dom";
import { Button, Container, Form, FormGroup, Label, Input } from "reactstrap";
import "../../styles/style.scss";
import {
	validateEmail,
	validatePassword,
	validateConfirmPassword,
	validateUsername,
} from "../Common/validations";
import MessageAlert from "../Common/MessageAlert";

class RegisterForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			username: {
				value: "",
				isValid: false,
				touched: false,
			},
			email: {
				value: "",
				isValid: false,
				touched: false,
			},
			password: {
				value: "",
				isValid: false,
				touched: false,
			},
			confirmPassword: {
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
	emailOnChange = (e) => {
		const email = e.target.value;
		const isValid = validateEmail(email);
		this.setState({
			email: {
				value: email,
				isValid: isValid,
				touched: true,
			},
		});
	};
	passwordOnChange = (e) => {
		const password = e.target.value;
		const isValid = validatePassword(password);
		const confirmPassword = this.state.confirmPassword.value;
		const isValid1 = validateConfirmPassword(
			password,
			confirmPassword,
			isValid
		);
		this.setState({
			password: {
				value: password,
				isValid: isValid,
				touched: true,
			},
			confirmPassword: {
				value: confirmPassword,
				isValid: isValid1,
				touched: this.state.confirmPassword.touched,
			},
		});
	};
	confirmPasswordOnChange = (e) => {
		const confirmPassword = e.target.value;
		const password = this.state.password.value;
		const isValid = validateConfirmPassword(
			password,
			confirmPassword,
			this.state.password.isValid
		);
		this.setState({
			confirmPassword: {
				value: confirmPassword,
				isValid: isValid,
				touched: true,
			},
		});
	};
	handleOnSubmit = (e) => {
		e.preventDefault();

		const username = this.state.username;
		const email = this.state.email;
		const password = this.state.password;
		const confirmPassword = this.state.confirmPassword;
		const isFormValid =
			username.isValid &&
			email.isValid &&
			password.isValid &&
			confirmPassword.isValid;
		if (isFormValid) {
			this.setState({
				error: "",
			});
			this.props.onSubmit(
				username.value,
				email.value,
				password.value,
				confirmPassword.value
			);
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
					<h3 className="header">Register</h3>

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
							<Label for="Email" size="sm">
								Email
							</Label>
							<Input
								onChange={this.emailOnChange}
								value={this.state.email.value}
								type="email"
								name="Email"
								id="Email"
								placeholder="Email"
								bsSize="lg"
								invalid={
									this.state.email.touched &&
									!this.state.email.isValid
								}
							/>
						</FormGroup>
						<FormGroup row>
							<Label for="Username" size="sm">
								Username
							</Label>
							<Input
								onChange={this.usernameOnChange}
								value={this.state.username.value}
								type="text"
								name="Username"
								id="Username"
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
							<Label for="confirmPassword" size="sm">
								Confirm Password
							</Label>
							<Input
								onChange={this.confirmPasswordOnChange}
								value={this.state.confirmPassword.value}
								type="password"
								name="confirmPassword"
								id="confirmPassword"
								placeholder="Confirm Password"
								bsSize="lg"
								invalid={
									this.state.confirmPassword.touched &&
									!this.state.confirmPassword.isValid
								}
							/>
						</FormGroup>
						<FormGroup row>
							<Button outline color="success" size="lg" block>
								Register
							</Button>
						</FormGroup>
					</Form>
				</div>
			</Container>
		);

		if (this.props.isAuthorized) {
			return <Redirect to={this.props.redirectPath} />;
		}
		if (this.props.isRegisterSuccess) {
			this.props.setRegisterStatus(false);
			return <Redirect to={this.props.redirectPath} />;
		}
		return form;
	}
}

export default RegisterForm;
