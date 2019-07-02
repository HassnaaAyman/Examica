import * as examsAPI from '../../API/examsAPI';
// import * as questionAPI from '../../API/questionAPI';

export const ADD = 'ADD';
export const EDIT = 'EDIT';
export const DELETE = 'DELETE';
export const GET_ORG_EXAMS = 'GET_ORG_EXAMS';
export const GET_USER_EXAMS = 'GET_USER_EXAMS';

export const add = (exam, orgId, token) => {
	return (dispatch) => {
		examsAPI
			.add(exam, orgId, token)
			.then((res) => {
				if (res.status === 200) {
					dispatch(addSuccess(res.data));
				}
			})
			.catch(console.error);
	};
};
export const addSuccess = (value) => {
	return { type: ADD, payload: value };
};


export const getByOrgId = (orgId, token) =>{
 return (dispatch) => {
	  dispatch({type: "IsLoading"});
	  examsAPI.getAll(orgId, token)
	  .then(res => {
		  dispatch({type: GET_ORG_EXAMS , payload : res.data})
		  dispatch({type: "Loaded"});
	  })
	  .catch(err => {
		dispatch({type: "Loaded"});
		console.error(err);
	  });
 }
}

export const getByUser = (token) =>{
	return (dispatch) => {
		 dispatch({type: "IsLoading"});
		 examsAPI.getByUser(token)
		 .then(res => {
			 dispatch({type: GET_USER_EXAMS , payload : res.data})
			 dispatch({type: "Loaded"});
		 })
		 .catch(err => {
		   dispatch({type: "Loaded"});
		   console.error(err);
		 });
	}
}

export const getById = (examId, token) => {
	return (dispatch) => {
		 dispatch({type: "IsLoading"});
		 examsAPI.getById(examId, token)
		 .then(res => {
			 dispatch({type: "GetExamToBeAdded" , payload : res.data})
			 dispatch({type: "Loaded"});
		 })
		 .catch(err => {
		   dispatch({type: "Loaded"});
		   console.error(err);
		 });
	}
}