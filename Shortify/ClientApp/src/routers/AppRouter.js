import React from "react";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import HomePage from "../components/Home/HomePage";
import DashboardPage from "../components/Dashboard/DashboardPage";
import ShortenLinkPage from "../components/ShortenLink/ShortenLinkPage";
import NotFoundPage from "../components/NotFoundPage";
import LoginPage from "../components/Login/LoginPage";
import RegisterPage from "../components/Register/RegisterPage";
import Logout from "../components/Logout";
import Home from "../components/Home/Home";

const AppRoute = () => (
	<BrowserRouter>
		<div>
			<Switch>
				<Route path="/app" component={HomePage} exact={true} />
				<Route
					path="/"
					render={() => <Redirect to="/app" />}
					exact={true}
				/>
				<Route path="/app/links" component={DashboardPage} />
				<Route path="/app/login" component={LoginPage} />
				<Route path="/app/register" component={RegisterPage} />
				<Route path="/app/shorten" component={ShortenLinkPage} />
				<Route path="/app/logout" component={Logout} />
				<Route path="/app/notfound" component={NotFoundPage} />
				<Route path="/:url" component={Home} />
				<Route component={NotFoundPage} />
			</Switch>
		</div>
	</BrowserRouter>
);

export default AppRoute;
