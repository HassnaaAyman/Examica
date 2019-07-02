import * as questionActions from '../Actions/questionActions';

const initialState = {
	questions: []
};

const questionReducer = (state = initialState, action) => {
	let newArr = [...state.questions];
	switch (action.type) {
		case questionActions.ADD_QUESTION:
			newArr.push(action.payload);
			break;
		case questionActions.GET_ALL_QUESTIONS:
			newArr = action.payload;
			break;
		case questionActions.DELETE_QUESTION:
			let questionIndex = newArr.findIndex(q => q.id === action.payload.questionId);
			if(questionIndex > -1) newArr.splice(questionIndex, 1);
			break;
		default:
			break;
	}
	return {
		...state,
		questions: newArr
	};
};

export default questionReducer;
