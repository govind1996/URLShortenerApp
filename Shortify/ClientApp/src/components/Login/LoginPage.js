import React from "react";
import LoginForm from "./LoginForm";
import { connect } from "react-redux";
import { clearAuthMessages, startLogin } from "../../actions/auth";
import "../../styles/style.scss";
import NavMenu from "../layout/NavMenu";
import Loader from "../Common/Loader";

class LoginPage extends React.Component {
	onSubmit = (username, password) => {
		this.props.onSubmitDispatch(username, password);
	};
	clearErrors = () => {
		this.props.clearAuthMessagesDispatch();
	};
	render() {
		//this.props.clearAuthMessagesDispatch();
		return (
			<div>
				<NavMenu />
				{this.props.isLoading && <Loader></Loader>}
				<LoginForm
					onSubmit={this.onSubmit}
					isLoading={this.props.isLoading}
					isAuthorized={this.props.isAuthorized}
					redirectPath={this.props.redirectPath}
					error={this.props.loginMessage}
					clearErrors={this.clearErrors}
				/>
			</div>
		);
	}
}

const mapStateToProps = (state) => ({
	isLoading: state.auth.isLoading,
	isAuthorized: state.auth.isAuthorized,
	redirectPath: state.auth.redirectPath,
	loginMessage: state.auth.loginMessage,
});
const mapDispatchToProps = (dispatch) => ({
	onSubmitDispatch: (username, password) =>
		dispatch(startLogin(username, password)),
	clearAuthMessagesDispatch: () => dispatch(clearAuthMessages()),
});
export default connect(mapStateToProps, mapDispatchToProps)(LoginPage);
