import Axios from 'axios';
import URL from './Config/url';

export const getAllUsers = (token)=>{
    return Axios.get(`${URL}/api/users`, {headers: {Authorization: token}});


}

export const getAllUsersOfOrg = (orgId,token)=>{
    return Axios.get(`${URL}/api/users/organization/${orgId}`, {headers: {Authorization: token}});


}

export const getUserById = (id,token)=>{
    return Axios.get(`${URL}/api/users/${id}`, {headers: {Authorization: token}});
}

export const getUser = (userId,orgId,token)=>{
    return Axios.get(`${URL}/api/users/${userId}/${orgId}`, {headers:{Authorization:token}})
}

export const addRole = (user, token) => {
    return Axios.post(`${URL}/api/users/assign`, user, {headers: {Authorization: token}});
}

export const login = (user) => {
    return Axios.post(`${URL}/api/account/login`, user);
}

export const register = (user) => {
    let arr = user.name.split('');
    user.name = arr.reduce((res, el) => {
        if(el !== " ") res = res + el;
        return res;
    } ,"");
    return  Axios.post(`${URL}/api/account/register`, user);
}