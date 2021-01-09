import React from "react";
import { connect } from "react-redux";
import {
	setRegisterStatus,
	startRegister,
	clearAuthMessages,
} from "../../actions/auth";
import RegisterForm from "./RegisterForm";
import NavMenu from "../layout/NavMenu";
import Loader from "../Common/Loader";

class RegisterPage extends React.Component {
	onSubmit = (username, email, password, confirmPassword) => {
		this.props.onSubmitDispatch(username, email, password, confirmPassword);
		// this.props.history.push('/links')
	};
	setRegisterStatus = (status) => {
		this.props.setRegisterStatusDispatch(status);
	};
	clearErrors = () => {
		this.props.clearAuthMessagesDispatch();
	};
	render() {
		return (
			<div>
				<NavMenu />
				{this.props.isLoading && <Loader></Loader>}
				<RegisterForm
					onSubmit={this.onSubmit}
					isLoading={this.props.isLoading}
					isAuthorized={this.props.isAuthorized}
					error={this.props.registerMessage}
					redirectPath={this.props.redirectPath}
					isRegisterSuccess={this.props.isRegisterSuccess}
					setRegisterStatus={this.setRegisterStatus}
					clearErrors={this.clearErrors}
				/>
			</div>
		);
	}
}

const mapStateToProps = (state) => ({
	isLoading: state.auth.isLoading,
	isAuthorized: state.auth.isAuthorized,
	registerMessage: state.auth.registerMessage,
	redirectPath: state.auth.redirectPath,
	isRegisterSuccess: state.auth.isRegisterSuccess,
});
const mapDispatchToProps = (dispatch) => ({
	onSubmitDispatch: (username, email, password, confirmPassword) =>
		dispatch(startRegister(username, email, password, confirmPassword)),
	setRegisterStatusDispatch: (status) => dispatch(setRegisterStatus(status)),
	clearAuthMessagesDispatch: () => dispatch(clearAuthMessages()),
});
export default connect(mapStateToProps, mapDispatchToProps)(RegisterPage);
