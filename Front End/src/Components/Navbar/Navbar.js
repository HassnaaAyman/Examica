import React from 'react';
import UpperNavbar from './upperNavbar';
import LowerNavbar from './lowerNavbar';
import './Navbar.css';
import {connect} from 'react-redux';


const Navbar = (props) => {
  return (
    <div><UpperNavbar/><LowerNavbar/></div>
  );
};

const mapStateToProps = state => {
  return {
    isLoggedIn: state.auth.isLoggedIn
  }
}

export default connect(mapStateToProps)(Navbar);
