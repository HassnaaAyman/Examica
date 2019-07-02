import * as authActions from '../Actions/authActions';

const initialState = {
	isError: false,
	isLoggedIn: false,
	token: '',
	userId: ''
};

const authReducer = (state = initialState, action) => {
	let newState = { ...state };
	switch (action.type) {
		case authActions.TOKEN:
			newState.isLoggedIn = true;
			newState.token = action.payload.token;
			newState.userId = action.payload.userId;
			break;
		case authActions.REGISTER:
		case authActions.LOGIN:
			let token = `bearer ${action.payload.token}`;
			localStorage.setItem("token", token);
			localStorage.setItem("userId", action.payload.userId);
			newState.isLoggedIn = true;
			newState.token = token;
			newState.userId = action.payload.userId;
			break;
		case authActions.LOGOUT:
			newState.isLoggedIn = false;
			newState.token = null;
			newState.userId = null;
			localStorage.removeItem("token");
			localStorage.removeItem("userId");
			break;
		case authActions.SUBMIT_ERROR:
			newState.isError = true;
			break;
		case authActions.REMOVE_ERROR:
			newState.isError = false;
			break;
		default:
			break;
	}
	return newState;
};
export default authReducer;
