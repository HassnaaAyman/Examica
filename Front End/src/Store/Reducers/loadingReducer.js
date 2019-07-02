const initialState = false;

const loadingReducer = (state = initialState, action) => {
    let newState = state;
    switch (action.type) {
		case "IsLoading":
            newState= true;
            break;
        case "Loaded":
            newState= false;
            break;
		default:
			break;
	}
	return newState;
};
export default loadingReducer;
