//auth reducer
const authDefaultState = {
	isLoading: false,
	authToken: "",
	isAuthorized: false,
	redirectPath: "/app/links",
	registerMessage: [],
	expiresAt: "",
	loginMessage: "",
	isRegisterSuccess: false,
};

const authReducer = (state = authDefaultState, action) => {
	switch (action.type) {
		case "LOGIN_SUCCESS": {
			return {
				...state,
				authToken: action.authToken,
				isLoading: false,
				isAuthorized: true,
				redirectPath: "/app/links",
				expiresAt: action.expiresAt,
				loginMessage: "",
				registerMessage: [],
			};
		}
		case "LOGIN_FAILED": {
			return {
				...state,
				isLoading: false,
				loginMessage: action.message,
			};
		}
		case "REGISTER_SUCCESS": {
			return {
				...state,
				isLoading: false,
				redirectPath: "/app/login",
				registerMessage: [],
				loginMessage: "",
				isRegisterSuccess: true,
			};
		}
		case "REGISTER_FAILED": {
			return {
				...state,
				isLoading: false,
				registerMessage: action.message,
				isRegisterSuccess: false,
			};
		}
		case "SET_REGISTER_STATUS": {
			return {
				...state,
				isRegisterSuccess: action.status,
			};
		}
		case "SET_LOADER": {
			return {
				...state,
				isLoading: action.isLoading,
			};
		}
		case "LOGOUT": {
			return {
				...state,
				isLoading: false,
				authToken: "",
				isAuthorized: false,
				redirectPath: "/app/links",
				registerMessage: "",
				expiresAt: 0,
			};
		}
		case "CLEAR_AUTH_MESSAGES": {
			return {
				...state,
				registerMessage: "",
				loginMessage: "",
			};
		}
		default: {
			return state;
		}
	}
};

export default authReducer;
