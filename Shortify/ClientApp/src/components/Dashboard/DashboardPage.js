import React from "react";
import LinksList from "./LinksList";
import { connect } from "react-redux";
import { startDeleteLink, startSetLinks } from "../../actions/links";
import { Redirect } from "react-router-dom";
import "../../styles/style.scss";
import NavMenu from "../layout/NavMenu";
import { Container } from "reactstrap";
import Alert from "./AlertModal";

class DashboardPage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			clicked: false,
			id: -1,
		};
	}
	componentDidMount() {
		this.props.setLinksDispatch(this.props.token);
	}
	handleDelete = (event, id, index) => {
		this.setState({
			clicked: true,
			id: id,
		});
		// this.props.startDeleteLinkDispatch(this.props.token, id, index);
	};
	deleteLink = () => {
		this.props.startDeleteLinkDispatch(
			this.props.token,
			this.state.id,
			this.state.index
		);
	};
	setDeleteState = () => {
		this.setState({
			clicked: false,
			id: -1,
		});
	};
	render() {
		let dashboard = (
			<div>
				{this.state.clicked && (
					<Alert
						isOpen={this.state.clicked}
						id={this.state.id}
						deleteLink={this.deleteLink}
						setDeleteState={this.setDeleteState}
					></Alert>
				)}
				<NavMenu />
				<Container>
					<div className="header">
						<h3>Links</h3>
					</div>
					<div className="tableOuterbox">
						<div className="tableContainer">
							<LinksList handleDelete={this.handleDelete} />
						</div>
					</div>
				</Container>
			</div>
		);
		if (!this.props.isAuthorized) {
			return <Redirect to="/app/login" />;
		}
		return dashboard;
	}
}
const mapStateToProps = (state) => ({
	isAuthorized: state.auth.isAuthorized,
	token: state.auth.authToken,
});
const mapDispatchToProps = (dispatch) => ({
	setLinksDispatch: (token) => dispatch(startSetLinks(token)),
	startDeleteLinkDispatch: (token, id, index) =>
		dispatch(startDeleteLink(token, id, index)),
});
export default connect(mapStateToProps, mapDispatchToProps)(DashboardPage);
