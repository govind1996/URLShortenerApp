import axios from "../axios";
import moment from "moment";

//clear messsages
export const clearAuthMessages = () => ({
	type: "CLEAR_AUTH_MESSAGES",
});

//login actions
export const setLoader = (isLoading) => ({
	type: "SET_LOADER",
	isLoading,
});
export const setRedirecet = (path) => ({
	type: "SET_REDIRECT_PATH",
	redirectPath: path,
});
export const loginSuccess = (authToken, expiresAt) => ({
	type: "LOGIN_SUCCESS",
	authToken,
	expiresAt,
});
export const loginFailed = (message) => ({
	type: "LOGIN_FAILED",
	message,
});
export const logout = () => ({
	type: "LOGOUT",
});
export const startLogout = (token) => {
	return (dispatch) => {
		axios
			.post("/account/signout")
			.then((response) => {
				console.log("logoutSuccess");
				localStorage.removeItem("authToken");
				localStorage.removeItem("expiresAt");
				dispatch(logout());
			})
			.catch((error) => {
				console.log("logouterror");
			});
	};
};
export const autoLogout = (expiresAt) => {
	return (dispatch) => {
		setTimeout(() => {
			dispatch(startLogout());
		}, expiresAt);
	};
};
export const startLogin = (username, password) => {
	return (dispatch) => {
		dispatch(setLoader(true));
		const payload = {
			username: username,
			password: password,
		};
		console.log(payload);
		axios
			.post("/account/signin", payload)
			.then((response) => {
				console.log(response.data);
				if (response.data.exception === null) {
					var user = response.data.user;
					const authToken = user.jwtToken;
					const expiresInSeconds = user.expiresAt;
					const expiresAt = moment().add(expiresInSeconds, "s");
					console.log(authToken);

					//saving in local storage
					localStorage.setItem("authToken", authToken);
					localStorage.setItem("expiresAt", expiresAt);
					console.log(expiresInSeconds);
					dispatch(loginSuccess(authToken, expiresAt));
					dispatch(autoLogout(expiresInSeconds * 1000));
				} else {
					dispatch(loginFailed(response.data.exception.message));
				}
			})
			.catch((error) => {
				console.log("login failed");
				dispatch(loginFailed());
			});
	};
};

//register actions

export const setRegisterStatus = (status) => ({
	type: "SET_REGISTER_STATUS",
	status,
});

export const registerSuccess = () => ({
	type: "REGISTER_SUCCESS",
});

export const registerFailed = (message) => ({
	type: "REGISTER_FAILED",
	message,
});

export const startRegister = (username, email, password, confirmPassword) => {
	return (dispatch) => {
		dispatch(setLoader(true));
		const payload = {
			username: username,
			email: email,
			password: password,
			confirmPassword: confirmPassword,
		};
		console.log(payload);
		axios
			.post("/account/register", payload)
			.then((response) => {
				console.log(response);
				if (response.data.exception === null) {
					dispatch(registerSuccess());
				} else {
					dispatch(registerFailed(response.data.exception.errors));
				}
				// this.props.history.push('/links')
			})
			.catch((error) => {
				console.log("registerfailed");
				dispatch(registerFailed());
			});
	};
};

export const checkAuthState = () => {
	return (dispatch) => {
		const token = localStorage.getItem("authToken");
		const expiresAt = moment(localStorage.getItem("expiresAt"));
		if (!token) {
			dispatch(startLogout());
		} else {
			const now = moment();
			const expiresInSeconds = expiresAt.diff(now, "s");
			console.log(expiresInSeconds);
			dispatch(loginSuccess(token, expiresAt));
			dispatch(autoLogout(expiresInSeconds * 1000));
		}
	};
};
