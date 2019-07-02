import React, { Component } from 'react';
import {Card} from 'element-react/next';
import './UserCard.css' ;
import {withRouter} from 'react-router-dom';


class UserCard extends Component {
    onClick() {
      this.props.history.push(`/organization/${this.props.match.params.id}/users/assign/${this.props.id}`);
    }

    render() {
      return (
      <Card className="box-card">
        <div onClick={this.onClick.bind(this)}>
          <img alt="Profile" className="userCard-img" src="http://www.mattmovingsystems.com/root/images/profile_user.gif"></img>
          <div className="userName">{this.props.UserName}</div>
          <div className="email">{this.props.Email}</div>
        </div>
      </Card>
      )
    } 
  }

export default withRouter(UserCard);