import * as userActions from '../Actions/userActions';

const initial ={
    allUsers: [],
    allUsersOfOrg: [],
    activeUser: null,
    exams: []
};

const userReducer =(state=initial,action)=>{
    let newArrOfUsers=[...state.allUsers];
    let newArrOfOrgUsers=[...state.allUsersOfOrg];
    let newActiveUser= {...state.activeUser}
    let newArrOfExams = [...state.exams]
    switch(action.type){
        case userActions.GET_ALL:
            newArrOfUsers=action.payload
            break;
        case userActions.GET_ALL_Of_Org:
                newArrOfOrgUsers=action.payload
                break;
        case userActions.GET_ACTIVE_USER:
            newActiveUser= action.payload;
            break;
        case userActions.ADD_ROLE:
            const newUser= action.payload;
            const index = newArrOfOrgUsers.findIndex(u => u.id === newUser.userId);
            if((newUser.isAdmin || newUser.isExaminer || newUser.isExaminee) && index < 0)
            {
                newArrOfOrgUsers.push(newArrOfUsers.find(u => u.id === newUser.userId));
            }
            else if(!(newUser.isAdmin || newUser.isExaminer || newUser.isExaminee) && index > -1)
            {
                newArrOfOrgUsers.splice(index, 1);
            }
            break;
        case "GET_USER_EXAMS":
            newArrOfExams= action.payload;
            break;
      default:
            break;

    }
    return {
            allUsers:newArrOfUsers,
            allUsersOfOrg: newArrOfOrgUsers,
            activeUser: newActiveUser,
            exams: newArrOfExams
    }
}
export default userReducer;