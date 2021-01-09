const initialState = {
	error: undefined,
	loginError: undefined,
	registerError: undefined,
	response: undefined,
};

const notificationReducer = (state = initialState, action) => {
	switch (action.type) {
		case "SET_ERROR":
			return {
				...state,
				error: action.error,
			};
		case "SET_RESPONSE":
			return {
				...state,
				response: action.response,
			};
		default:
			return state;
	}
};
export default notificationReducer;
