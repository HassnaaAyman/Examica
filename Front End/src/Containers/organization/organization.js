import React, { Component } from 'react';
import {Switch, Route} from 'react-router-dom';
import OrganizationProfile from '../../Pages/organizationPage';
import {connect} from 'react-redux';
import {getCurrent, getOrgById} from '../../Store/Actions/organizationActions';
import {getAll, getAllOfOrg} from '../../Store/Actions/userActions';
import UsersList from '../../Containers/Users/UsersList';
import ExamsList from '../exams/exam-list';
import QuestionsList from '../PoolOfQuestions';
import AddExam from '../exams/exam-add';
import AssignRoles from '../assignRole';
import * as examActions from '../../Store/Actions/examActions';
import * as questionActions from '../../Store/Actions/questionActions';
import AddQuestion from '../QuestionTypes';

class Organization extends Component {
  constructor(props) {
    super(props);
    this.state = {
      orgId: null
    }
    this.getCurrentData = this.getCurrentData.bind(this);
  }


  getCurrentData() {
    this.setState({
      orgId: this.props.match.params.id
    });
    this.props.onGetRoles(this.props.userId,this.props.match.params.id, this.props.token);
    this.props.onGetOrg(this.props.match.params.id, this.props.token);
    this.props.onGetAllUsers(this.props.token);
    this.props.onGetAllUsersOfOrg(this.props.match.params.id, this.props.token);
    this.props.onGetAllExams(this.props.match.params.id, this.props.token);
    this.props.onGetQuestions(this.props.match.params.id, this.props.token);
  }

  componentDidMount() {
    this.getCurrentData();
  }

  componentDidUpdate() {
    if(this.props.match.params.id !== this.state.orgId)
    {
      this.getCurrentData();
    }
  }

  render() {
    let routes= null;
    if(this.props.roles && (this.props.roles.isOwner || this.props.roles.isAdmin) && this.props.roles.isExaminer) {
      routes= (
        <Switch>
          <Route path="/organization/:id/myusers" render={()=> <UsersList users={this.props.orgUsers} />} />
          <Route path="/organization/:id/users/assign/:userId" component={AssignRoles}/>
          <Route path="/organization/:id/users" render={()=> <UsersList users={this.props.allUsers} />} />
          <Route path="/organization/:id/exams/add" component={AddExam} />
          <Route path="/organization/:id/exams" component={ExamsList} />
          <Route path="/organization/:id/questions/add/exam/:examId" render={() => <AddQuestion isExam={true} /> }  />
          <Route path="/organization/:id/questions/add" component={AddQuestion} />
          <Route path="/organization/:id/questions" component={QuestionsList} />
          <Route path="/organization/:id" component={OrganizationProfile}/>
        </Switch>        
      );
    }
    else if(this.props.roles && (this.props.roles.isOwner || this.props.roles.isAdmin)) {
      routes= (
        <Switch>
          <Route path="/organization/:id/myusers" render={()=> <UsersList users={this.props.orgUsers} />} />
          <Route path="/organization/:id/users/assign/:userId" component={AssignRoles}/>
          <Route path="/organization/:id/users" render={()=> <UsersList users={this.props.allUsers} />} />
          <Route path="/organization/:id" component={OrganizationProfile}/>
        </Switch>        
      );
    }
    else if(this.props.roles && this.props.roles.isExaminer) {
      console.log(this.props.roles.isExaminer);
      routes= (
        <Switch>
          <Route path="/organization/:id/exams/add" component={AddExam} />
          <Route path="/organization/:id/exams" component={ExamsList} />
          <Route path="/organization/:id/questions/add/exam/:examId" render={() => <AddQuestion isExam={true} /> }  />
          <Route path="/organization/:id/questions/add" component={AddQuestion} />
          <Route path="/organization/:id/questions" component={QuestionsList} />
          <Route path="/organization/:id" component={OrganizationProfile}/>
        </Switch>
      );
    }
    return routes;
  }
}

const mapStateToProps = state => {
  return {
    token: state.auth.token,
    userId: state.auth.userId,
    organization: state.organizations.currentOrgnaziation,
    allUsers: state.users.allUsers,
    orgUsers: state.users.allUsersOfOrg,
    roles: state.users.activeUser.roles
  }
}

const mapDispatchToProps = dispatch => {
  return {
    onGetRoles: (userId,orgId,token) => {dispatch(getCurrent(userId,orgId,token))},
    onGetOrg: (orgId,token) => {dispatch(getOrgById(orgId,token))},
    onGetAllUsers: (token) => {dispatch(getAll(token))},
    onGetAllUsersOfOrg: (orgId, token) => {dispatch(getAllOfOrg(orgId, token))},
    onGetAllExams : (orgId, token) => dispatch(examActions.getByOrgId(orgId, token)),
    onGetQuestions: (orgId, token) => dispatch(questionActions.getAll(orgId, token))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Organization);
