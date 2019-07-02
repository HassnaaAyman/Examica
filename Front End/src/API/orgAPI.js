import axios from 'axios';
import URL from './Config/url';

export const addOrg = (organization, token) => {
	return axios.post(`${URL}/api/Organizations`, organization, { headers: { Authorization: token } });
};

export const getOrgs = (token) => {
	return axios.get(`${URL}/api/Organizations/user`, { headers: { Authorization: token } });
};

export const getCurrentOrg = (userId, orgId, token) => {
	return axios.get(`${URL}/api/users/${userId}/${orgId}`, { headers: { Authorization: token } });
};

export const getOrgById = (orgId, token) => {
	return axios.get(`${URL}/api/organizations/${orgId}`, { headers: { Authorization: token } });
};