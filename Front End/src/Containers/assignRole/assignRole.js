import React, { Component } from 'react';
import {Checkbox,Layout,Button} from 'element-react/next';
import * as getUserAPI from '../../API/userAPI';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import './assignRole.css';
import * as userActions from '../../Store/Actions/userActions';

class AssignRole extends Component {
  constructor(props) {
    super(props);
    this.state = {
      user: {
        userName: '',
        email:'',
        roles: {
          userId: "",
          organizationId: "",
          isAdmin:false,
          isExaminer:false,
          isExaminee:false,
          isObserver:false,
        }
      }
    };
  }
  

  onChange(key, value) {
    let newRoles = {...this.state.user.roles};
    newRoles[key]= value;
    this.setState({
      user:{
        ...this.state.user,
        roles: newRoles
      }
    });
  }

  onAddRole() {
    this.props.onAddRole(this.state.user.roles, this.props.token);
    this.props.history.push(`/`);
  }

  componentDidMount(){
    this.props.startLoading();
    getUserAPI.getUser(this.props.match.params.userId, this.props.match.params.id , this.props.token)
    .then(res => {
      if (res.status === 200) {
        this.setState({
          user : res.data
        })
      }
      setTimeout(this.props.endLoading,1000);
    })
    .catch(console.error)
  }

  render() {
    return (
      <>
        <Layout.Row className="ProfilePage">
          <Layout.Col sm={11} md={8} lg={5} offset={5}>
            <Layout.Row>
              <Layout.Col className="ProfilePage-left-section">
                <div className="ProfilePage-pic" />
              </Layout.Col>
            </Layout.Row>
            <Layout.Row>
              <Layout.Col>
                <div className="ProfilePage-info">
                  <h3 className="ProfilePage-info-name">
                    {this.state.user.userName}
                  </h3>
                  <p>
                    <i className="fas fa-envelope ProfilePage-info-icon" />
                    {this.state.user.email}
                  </p>
                </div>
              </Layout.Col>
            </Layout.Row>
          </Layout.Col>

          <Layout.Col
            sm={5}
            md={8}
            lg={11}
            className="ProfilePage-right-section"
          >
           <div className="assignRole container">
              <Layout.Row gutter="20">
                <Layout.Col span="6" offset="2">
                  <div className="grid-content bg-purple">
                    <Checkbox label="Make as an admin" checked={this.state.user.roles.isAdmin} onChange={this.onChange.bind(this, "isAdmin")}></Checkbox>
                    <Checkbox label="Make as an examiner" checked={this.state.user.roles.isExaminer} onChange={this.onChange.bind(this, "isExaminer")}></Checkbox>
                    <Checkbox label="Make as an examinee" checked={this.state.user.roles.isExaminee} onChange={this.onChange.bind(this, "isExaminee")}></Checkbox>
                    <Button onClick={this.onAddRole.bind(this)} className="Buttonprimary">Submit</Button>
                  </div>
                </Layout.Col>
              </Layout.Row>
            </div>
          </Layout.Col>
        </Layout.Row>
      </>
    )
  }
}

const mapStateToProps = state => {
  return {
    user : state.users.oneUser,
    token: state.auth.token
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    onAddRole : (role, token) => dispatch(userActions.addRole(role, token)),
    startLoading: () => dispatch({type: "IsLoading"}),
    endLoading: () => dispatch({type: "Loaded"})
  }
}

export default connect(mapStateToProps,mapDispatchToProps)(withRouter(AssignRole));
