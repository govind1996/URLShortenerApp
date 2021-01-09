import React from "react";
import { connect } from "react-redux";
import ShortenLinkForm from "./ShortenLinkForm";
import { startAddLink, setLoader } from "../../actions/links";
import { Redirect } from "react-router-dom";
import NavMenu from "../layout/NavMenu";
import Container from "reactstrap/lib/Container";
import Loader from "../Common/Loader";

class ShortenLinkPage extends React.Component {
	onSubmit = (url, token) => {
		this.props.loaderDispatch(true);
		console.log(token);
		this.props.onSubmitDispatch(url, token);
	};
	render() {
		if (!this.props.isAuthorized) {
			return <Redirect to="/login" />;
		}
		return (
			<div>
				<NavMenu />
				{this.props.isLoading && <Loader></Loader>}
				<Container>
					<div className="shortenForm">
						<ShortenLinkForm
							onSubmit={this.onSubmit}
							token={this.props.token}
							isLoading={this.props.isLoading}
							response={this.props.response}
						/>
					</div>
				</Container>
			</div>
		);
	}
}
const mapDispatchToProps = (dispatch) => ({
	onSubmitDispatch: (link, token) => dispatch(startAddLink(link, token)),
	loaderDispatch: (isLoading) => dispatch(setLoader(isLoading)),
});
const mapStateToProps = (state) => ({
	token: state.auth.authToken,
	isAuthorized: state.auth.isAuthorized,
	isLoading: state.links.isLoading,
	response: state.notification.response,
});
export default connect(mapStateToProps, mapDispatchToProps)(ShortenLinkPage);
