import Axios from 'axios';
import URL from './Config/url';


export const addNewQuestion = (question, token) => {
    return Axios.post(`${URL}/api/questions`, question, {headers: {Authorization: token}});
}

export const getAll = (orgId, token) => {
    return Axios.get(`${URL}/api/Questions/organization/${orgId}`, {headers: {Authorization: token}});
}

export const deleteQuestion = (questionId, token) => {
    return Axios.delete(`${URL}/api/Questions/${questionId}`, {headers: {Authorization: token}});
}

export const assignQuestion = (examId, questionId, token) => {
    return Axios.get(`${URL}/api/Exams/assign/${examId}/${questionId}`, {headers: {Authorization: token}});
}