import React, {Component} from 'react';
import { connect } from 'react-redux';
import {Button} from 'element-react/next';
import {logoutUser} from '../../Store/Actions/authActions';
import './LogoutButton.css';
import {withRouter} from 'react-router-dom';
import * as orgActions from '../../Store/Actions/organizationActions';

class LogoutButton extends Component {
  
  logout() {
      this.props.logout();
      this.props.history.push("/");
  }

  componentDidMount() {
    this.props.getOrgs(this.props.token);
  }

  render() {
    return <Button onClick={this.logout.bind(this)}>Logout</Button>;
  }
}

const mapStateToProps = state => {
  return {
    token: state.auth.token
  };
};

const mapDispatchToProps = dispatch => {
  return {
    logout: () => dispatch(logoutUser()),
    getOrgs: token => dispatch(orgActions.getAll(token)) 
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(LogoutButton));
