const initialState = {
    ExamId: null,
    QuestionsIds:[],
    ComplexQuestionsIds:[]
};

const addExamReducer = (state = initialState, action) => {
    let newExamId= state.ExamId;
	let newQuestionsIds = [ ...state.QuestionsIds ];
	// let newOrgExams = [ ...state.orgExams ];
	switch (action.type) {
		case "GetExamToBeAdded":
            newExamId= action.payload.id;
            for (const question of action.payload.questions) {
                newQuestionsIds.push(question.id);
            }
            break;
        case "AddQuestionToExam":
            newQuestionsIds.push(action.payload);
			break;
		default:
			break;
    }
    console.log(newExamId, newQuestionsIds);
	return {
        ...state,
		ExamId: newExamId,
		QuestionsIds: newQuestionsIds
	};
}
export default addExamReducer;
