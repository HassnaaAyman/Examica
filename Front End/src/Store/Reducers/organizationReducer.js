import * as orgActions from '../Actions/organizationActions';

const initialState = {
    all: [],
    currentOrgnaziation: null
};

const organizationReducer = (state = initialState, action) => {
    let newOrg = { ...state.currentOrgnaziation };
    let newArr = [...state.all];
	switch (action.type) {
		case orgActions.ALL:
            newArr = action.payload;
            break;
        case orgActions.CURRENT:
            newOrg= action.payload;
            break;
        case orgActions.ADDORG:
            newArr.push(action.payload);
            break;
		default:
			break;
    }
    return {
        all: newArr,
        currentOrgnaziation: newOrg
    };
};

export default organizationReducer;
