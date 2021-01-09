//links reducer
const linksDefaultState = {
	isLoading: false,
	links: [],
	shortenLink: "",
};

const linksReducer = (state = linksDefaultState, action) => {
	switch (action.type) {
		case "ADD_LINK": {
			return {
				...state,
				isLoading: false,
				links: [...state.links, action.link],
			};
		}
		case "SET_LOADER": {
			return {
				...state,
				isLoading: action.isLoading,
			};
		}
		case "SET_LINKS": {
			return {
				...state,
				isLoading: false,
				links: action.links,
			};
		}
		case "SET_FAILED": {
			return {
				...state,
				isLoading: false,
				links: [],
			};
		}
		case "DELETE_LINK": {
			return {
				...state,
				isLoading: false,
				links: state.links.filter((link, index) => {
					return link.id !== action.index;
				}),
			};
		}
		default: {
			return state;
		}
	}
};

export default linksReducer;
