import { createStore, combineReducers, applyMiddleware, compose } from "redux";
import linksReducer from "../reducers/links";
import authReducer from "../reducers/auth";
import notificationReducer from "../reducers/notification";
import thunk from "redux-thunk";

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
//create store by combining multiple reducers
export default () => {
	const store = createStore(
		combineReducers({
			links: linksReducer,
			auth: authReducer,
			notification: notificationReducer,
		}),
		composeEnhancers(applyMiddleware(thunk))
		//window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
	);
	return store;
};
