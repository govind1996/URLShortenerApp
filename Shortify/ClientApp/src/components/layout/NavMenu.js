import React, { Component } from "react";
import { connect } from "react-redux";
import {
	Collapse,
	Container,
	Navbar,
	NavbarBrand,
	NavbarToggler,
	NavItem,
	NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "../../styles/style.scss";

class NavMenu extends Component {
	static displayName = NavMenu.name;

	constructor(props) {
		super(props);

		this.toggleNavbar = this.toggleNavbar.bind(this);
		this.state = {
			collapsed: true,
		};
	}

	toggleNavbar() {
		this.setState({
			collapsed: !this.state.collapsed,
		});
	}

	render() {
		return (
			<header>
				<Navbar
					className="navbar-expand-sm navbar-toggleable-sm ng-white box-shadow mb-3"
					light
				>
					<Container>
						<NavbarBrand tag={Link} to="/">
							Shortify
						</NavbarBrand>
						<NavbarToggler
							onClick={this.toggleNavbar}
							className="mr-2"
						/>
						<Collapse
							className="d-sm-inline-flex flex-sm-row-reverse"
							isOpen={!this.state.collapsed}
							navbar
						>
							<ul className="navbar-nav flex-grow">
								<NavItem>
									<NavLink
										tag={Link}
										className="text-dark"
										to="/app"
									>
										Home
									</NavLink>
								</NavItem>
								{this.props.isAuthorized ? (
									<NavItem>
										<NavLink
											tag={Link}
											className="text-dark"
											to="/app/links"
										>
											Dashboard
										</NavLink>
									</NavItem>
								) : null}
								{this.props.isAuthorized ? (
									<NavItem>
										<NavLink
											tag={Link}
											className="text-dark"
											to="/app/shorten"
										>
											Shorten Link
										</NavLink>
									</NavItem>
								) : null}
								{this.props.isAuthorized ? (
									<NavItem>
										<NavLink
											tag={Link}
											className="text-dark"
											to="/app/logout"
										>
											Logout
										</NavLink>
									</NavItem>
								) : null}
								{!this.props.isAuthorized ? (
									<NavItem>
										<NavLink
											tag={Link}
											className="text-dark"
											to="/app/Login"
										>
											Login
										</NavLink>
									</NavItem>
								) : null}
								{!this.props.isAuthorized ? (
									<NavItem>
										<NavLink
											tag={Link}
											className="text-dark"
											to="/app/Register"
										>
											Register
										</NavLink>
									</NavItem>
								) : null}
							</ul>
						</Collapse>
					</Container>
				</Navbar>
			</header>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		isAuthorized: state.auth.isAuthorized,
	};
};
export default connect(mapStateToProps)(NavMenu);
