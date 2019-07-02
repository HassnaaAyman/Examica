import * as userAPI from '../../API/userAPI';

export const GET_ALL = "GET_ALL";
export const GET_USER = "GET_USER";
export const GET_ACTIVE_USER = "GET_ACTIVE_USER";
export const ADD_ROLE = "ADDROLE";
export const GET_ALL_Of_Org = "GET_ALL_Of_Org";




export const getAll=(token) =>{
    return (dispatch)=>{
            dispatch({type: "IsLoading"});
            userAPI.getAllUsers(token)
            .then(res=>{
                if(res.status===200){
                    dispatch(getAllUsersSuccess(res.data));
                }
                dispatch({type: "Loaded"});
            })
            .catch(console.error);
    };

     
}
export const getAllUsersSuccess =(value)=>{
return {type:GET_ALL,payload:value};
};

export const getAllOfOrg=(orgId, token) =>{
    return (dispatch)=>{
            dispatch({type: "IsLoading"});
            userAPI.getAllUsersOfOrg(orgId,token)
            .then(res=>{
                if(res.status===200){
                    dispatch(getAllUsersOfOrgSuccess(res.data));
                }
                dispatch({type: "Loaded"});
            })
            .catch(console.error);
    };

     
}
export const getAllUsersOfOrgSuccess =(value)=>{
return {type:GET_ALL_Of_Org,payload:value};
};


export const getOneUser =(userId, token)=>{
    return (dispatch)=> {
        userAPI.getUserById(userId,token)
        .then(res=>{
            if(res.status===200){
                dispatch(getOneUserSuccess(res.data));
            }

        })
        .catch(console.error)
    };

};


export const getOneUserSuccess =(value)=>{
    return {
        type: GET_ACTIVE_USER,
        payload:value,
    }
 }

export const addRole = (roles, token) => {
    return dispatch => {
        userAPI.addRole(roles, token)
        .then(res =>{
            if (res.status === 200) {
                dispatch(addRoleSuccess(roles));
            }
        })
        .catch(console.error);
    }
  }


export const getUser =(userId, orgId, token)=>{
return (dispatch)=> {
    dispatch({type: "IsLoading"});
    userAPI.getUser(userId, orgId, token)
    .then(res=>{
        if(res.status===200){
            dispatch(getUserSuccess(res.data));
        }
        dispatch({type: "Loaded"});
    })
    .catch(console.error)
};

};


export const getUserSuccess =(value)=>{
   return {
       type:GET_USER,
       payload:value,
   }
}

export const addRoleSuccess = (value) => {
    return {
        type:ADD_ROLE, 
        payload: value }
}
