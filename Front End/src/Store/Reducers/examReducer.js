import * as examActions from '../Actions/examActions';

const initialState = {
	userExams: [],
	orgExams: []
};

const examReducer = (state = initialState, action) => {
	let newUserExams = [ ...state.userExams ];
	let newOrgExams = [ ...state.orgExams ];
	switch (action.type) {
		case examActions.ADD:
			newOrgExams.push(action.payload);
			break;
		case examActions.GET_ORG_EXAMS:
				newOrgExams = action.payload ; 
				break;
		case examActions.GET_USER_EXAMS:
			newUserExams = action.payload ; 
				break;
		default:
			break;
	}
	return {
		userExams: newUserExams,
		orgExams: newOrgExams
	};
}
export default examReducer;
