import React, { Component } from "react";
import { Menu, Layout, Button } from "element-react/next";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import LogOutButton from "../../LogoutButton";
import "./upperNavbar.css";

class UpperNavbar extends Component {

  onSelect = (index, indexPath, item) => {
    if (item.props.to) this.props.history.push(item.props.to);
  };
  render() {
    let orgs = null;
    if(this.props.orgs.length > 0) {
      orgs= (
        <Menu.ItemGroup title="My Organization">
          {this.props.orgs.map(org => <Menu.Item key={org.id} index={`2-3-${org.id}`}  to={`/organization/${org.id}`}>{org.name}</Menu.Item>)}
        </Menu.ItemGroup>
      );
    }

    let btns = null;
    if (this.props.isLoggedIn) {
      btns = (
        <>
          <Menu.Item
            index="6"
            onClick={() => {
              console.log("sign out");
            }}
          >
            <LogOutButton />
          </Menu.Item>

          <Menu.SubMenu index="1" title="">
            <Menu.Item index="2-1" to="/profile">Profile</Menu.Item>
            <Menu.Item index="2-2" to="/buyOrg">Register new Organization</Menu.Item>
            {orgs}
          </Menu.SubMenu>
          <Menu.Item index="0" className="userIcon" to="/profile">
            <i className="fas fa-user-circle"></i>
            <span className="UserEmail"> {(this.props.user.userName) ? this.props.user.userName : ""}</span>
          </Menu.Item>

        </>
      );
    } else {
      btns = (
        <>
          <Menu.Item index="4" to="/Login">
            Sign in
          </Menu.Item>
          <Menu.Item index="5" to="/Register">
            <Button className="Buttonprimary">Sign up</Button>
          </Menu.Item>
        </>
      );
    }
    return (
      <div className="upperNavbar">
        <Menu
          defaultActive="1"
          className="el-menu-demo"
          mode="horizontal"
          onSelect={this.onSelect.bind(this)}
        >
          <Layout.Row>
            <Layout.Col span="17">
              <Menu.Item index="1">
                <a href="https://facebook.com">
                  <i className="fab fa-facebook-f" />
                </a>
              </Menu.Item>
              <Menu.Item index="2">
                <a href="https://twitter.com/">
                  <i className="fab fa-twitter" />
                </a>
              </Menu.Item>
              <Menu.Item index="3">
                <a href="https://www.linkedin.com">
                  <i className="fab fa-linkedin-in" />
                </a>
              </Menu.Item>
            </Layout.Col>
            {btns}
            <Layout.Col span="5" className="listOfContent" />
          </Layout.Row>
        </Menu>
      </div>
    );
  }
}
const mapStateToProps = state => {
  return {
    isLoggedIn: state.auth.isLoggedIn,
    token: state.auth.token,
    orgs: state.organizations.all,
    user: state.users.activeUser
  };
};

export default connect(mapStateToProps)(withRouter(UpperNavbar));
