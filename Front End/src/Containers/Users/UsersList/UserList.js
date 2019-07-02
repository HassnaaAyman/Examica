import React, { Component } from 'react';
import UserCard from '../UserCard';
import './UserList.css';
import '../../../Components/SearchBox/SearchBox.css';
import {withRouter} from 'react-router-dom';

class UserList extends Component {
    state= {
      search: "",
      path: ""
    }

    onChange(event) {
      this.setState({
        search: event.target.value
      })
    }

    componentDidMount() {
      this.setState({
        path: this.props.history.location.pathname
      });
    }

    componentDidUpdate() {
      if(this.props.history.location.pathname !== this.state.path) {
        this.setState({
          path: this.props.history.location.pathname,
          search: ""
        });
      }
    }

    render() {
    
    let usersArr = this.props.users;
    const search = this.state.search.trim().toLowerCase();
    if(search !== "") {
      usersArr = usersArr.filter(u => u.userName.toLowerCase().includes(search) || u.email.toLowerCase().includes(search));
    }

    let users = (<h3 className="NotFound">No users Found!</h3>);
    if(usersArr.length > 0) users = usersArr.map(user=>{
      return(
          <UserCard 
          key={user.id}
          id={user.id}
          UserName={user.userName}
          Email={user.email}
          />
    )});
    return(
    <div className="UserList">
      <div className="SearchBox">
        <input placeholder="Search" value={this.state.search} onChange={this.onChange.bind(this)} className="SearchBox-input"></input><i className="fas fa-search SearchBox-icon"></i>
      </div>
      <div className="UserList-Cards">
        {users}
      </div>
    </div>
    );
  }
  
}

export default withRouter(UserList);
