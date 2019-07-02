import React from 'react';
import {Layout,Menu} from 'element-react/next';
import {withRouter} from 'react-router-dom';
import {connect} from 'react-redux';
import './MiddleNavbar.css'

const MiddleNavbar = (props) => {
  const onSelect= (index, indexPath, item) => {
    props.history.push(item.props.to);
  }
  let items= null;
    if(props.roles && (props.roles.isOwner || props.roles.isAdmin) && props.roles.isExaminer) {
      items= (
        <>
          <Menu.Item  className="logo" index="1" to={`/organization/${props.org.id}`}>{props.org.name}</Menu.Item>
          <Menu.Item index="1" to={`/organization/${props.org.id}`}><i className="fas fa-user-circle"></i> Organization Profile</Menu.Item>
          <Menu.Item index="4" to={`/organization/${props.org.id}/exams`}><i className="fas fa-book-open"></i> Your Exams</Menu.Item>
          <Menu.Item index="2" to={`/organization/${props.org.id}/exams/add`}><i className="fas fa-scroll"></i> Build Exam</Menu.Item>
          <Menu.Item index="3" to={`/organization/${props.org.id}/questions`}><i className="fas fa-question"></i> Questions Pool</Menu.Item>
          <Menu.Item index="33" to={`/organization/${props.org.id}/questions/add`}><i className="fas fa-folder-plus"></i> Add Question</Menu.Item>
          <Menu.Item index="5" to={`/organization/${props.org.id}/myusers`}><i className="fas fa-users-cog"></i> My Users</Menu.Item>
          <Menu.Item index="6" to={`/organization/${props.org.id}/users`}><i className="fas fa-user-plus"></i> Add Users</Menu.Item>
          <Menu.Item index="7" to={`/`}><i className="fas fa-home"></i> Return to Examica Home</Menu.Item>
        </>
      );
    }
    else if(props.roles && (props.roles.isOwner || props.roles.isAdmin)) {
      items= (
        <>
          <Menu.Item  className="logo" index="1" to={`/organization/${props.org.id}`}>{props.org.name}</Menu.Item>
          <Menu.Item index="1" to={`/organization/${props.org.id}`}><i className="fas fa-user-circle"></i> Organization Profile</Menu.Item>
          <Menu.Item index="5" to={`/organization/${props.org.id}/myusers`}><i className="fas fa-users-cog"></i> My Users</Menu.Item>
          <Menu.Item index="6" to={`/organization/${props.org.id}/users`}><i className="fas fa-user-plus"></i> Add Users</Menu.Item>
          <Menu.Item index="7" to={`/`}><i className="fas fa-home"></i> Return to Examica Home</Menu.Item>
        </>
      );
    }
    else if(props.roles && props.roles.isExaminer) {
      items= (
        <>
          <Menu.Item  className="logo" index="1" to={`/organization/${props.org.id}`}>{props.org.name}</Menu.Item>
          <Menu.Item index="1" to={`/organization/${props.org.id}`}><i className="fas fa-user-circle"></i> Organization Profile</Menu.Item>
          <Menu.Item index="4" to={`/organization/${props.org.id}/exams`}><i className="fas fa-book-open"></i> Your Exams</Menu.Item>
          <Menu.Item index="2" to={`/organization/${props.org.id}/exams/add`}><i className="fas fa-scroll"></i> Build Exam</Menu.Item>
          <Menu.Item index="3" to={`/organization/${props.org.id}/questions`}><i className="fas fa-question"></i> Questions Pool</Menu.Item>
          <Menu.Item index="33" to={`/organization/${props.org.id}/questions/add`}><i className="fas fa-folder-plus"></i> Add Question</Menu.Item>
          <Menu.Item index="7" to={`/`}><i className="fas fa-home"></i> Return to Examica Home</Menu.Item>
        </>
      );
    }
  return(
    <Layout.Row >
    <Layout.Col span={24}>
      <div className="MiddleNavbar">
        <Menu className="el-menu-demo" mode="horizontal" onSelect={onSelect}>
         {items}
        </Menu>
      </div>
    </Layout.Col>
  </Layout.Row>
  );
};

export const mapStateToProps = state => {
  return {
    org : state.organizations.currentOrgnaziation,
    roles: state.users.activeUser.roles

  }
}

export default withRouter(connect(mapStateToProps)(MiddleNavbar));
