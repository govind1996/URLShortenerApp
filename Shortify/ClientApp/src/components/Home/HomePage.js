import React from "react";
import NavMenu from "../layout/NavMenu";
import { Container } from "reactstrap";

const HomePage = () => {
	return (
		<div>
			<NavMenu />
			<Container style={{ textAlign: "center" }}>
				<h1>Welcome to Shortify</h1>
				<h4>
					An URL shortener application created using React and ASP.NET
					Core
				</h4>
			</Container>
		</div>
	);
};

export default HomePage;
