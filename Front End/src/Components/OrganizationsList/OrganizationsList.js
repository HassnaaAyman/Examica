import React from 'react';
import {connect} from 'react-redux';
import {Menu} from 'element-react/next';

const OrganizationsList = (props) => {
  const select = (index) => {
    console.log(index);
  }
  return (
    <Menu className="el-menu-vertical-demo" onSelect={select}>
      {props.organizations.map(o => <Menu.Item index={o.id.toString()} key={o.id}><i className="el-icon-menu"></i>{o.name}</Menu.Item>)}
    </Menu>
  );
};


const mapStateToProps = state => {
  return {
    organizations: state.organizations.all
  }
}

export default connect(mapStateToProps)(OrganizationsList);
