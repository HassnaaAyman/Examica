import * as questionsAPI from '../../API/questionAPI'
export const ADD_QUESTION = "ADD_QUESTION";
export const DELETE_QUESTION = "DELETE_QUESTION";
export const GET_ALL_QUESTIONS = "GET_ALL_QUESTIONS"




export const addQuesSuccess = (question) => {
    return { type: ADD_QUESTION, payload: question }
}


export const addNewQuestion = (question, token) => {
    return dispatch => {
        dispatch({type: "IsLoading"});
        questionsAPI.addNewQuestion(question, token)
            .then(res => {
                    if(res.status === 200) dispatch(addQuesSuccess(res.data));
                    dispatch({type: "Loaded"});
                }
            )
            .catch(console.error);
    }
}

export const addNewQuestionToExam = (examId, question, token) => {
    return dispatch => {
    dispatch({type: "IsLoading"});
    questionsAPI.addNewQuestion(question, token)
        .then(res => {
                if(res.status === 200) {
                    dispatch(addQuesSuccess(res.data));
                    // dispatch({type: "AddQuestionToExam", payload: res.data.id});
                    questionsAPI.assignQuestion(examId, res.data.id, token);
                }
                dispatch({type: "Loaded"});
            }
        )
        .catch(console.error);
    }
}


export const getAll = (orgId, token) => {
    return (dispatch) => {
        dispatch({type: "IsLoading"});
        questionsAPI.getAll(orgId, token)
            .then(res => {
                if (res.status === 200) {
                    dispatch(getAllSuccess(res.data));
                }
                dispatch({type: "Loaded"});
            })
            .catch(console.error);
    };
};
export const getAllSuccess = (value) => {
    return { type: GET_ALL_QUESTIONS, payload: value };
};



export const deleteQuestion = (id, token) => {
    return (dispatch) => {
            dispatch({type: "IsLoading"});
            questionsAPI.deleteQuestion(id, token)
            .then(res => {
                if (res.status === 200) {
                    dispatch(deleteSuccess(res.data));
                }
                dispatch({type: "Loaded"});
            })
            .catch(console.error);
    };
};
export const deleteSuccess = (value) => {
    return { type: DELETE_QUESTION, payload: value };
};

