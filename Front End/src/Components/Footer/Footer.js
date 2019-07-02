import React from 'react';
import { Menu} from 'element-react/next';
import './Footer.css';
import { withRouter} from 'react-router-dom';

const Footer = (props) => {

  const onSelect = (index,indexPath,item) => {
    props.history.push(item.props.to);
  }

  return (
    <>
      <div className="Footer">
        <Menu theme="dark"  className="el-menu-demo" mode="horizontal" onSelect={onSelect}>
          <Menu.Item index="0" to="/">Home</Menu.Item>
          <Menu.Item index="1" to="/pricing">Pricing</Menu.Item>
          <Menu.Item index="2" to="/aboutus">About Us</Menu.Item>
          <Menu.Item index="3" to="/contactus">Contact</Menu.Item>
        </Menu>
          <p>Copyright &copy; 2019 | Designed by  <span> Examica</span></p>
          <div className="SocialLinks">
            <a href="https://facebook.com"><i className="fab fa-facebook-square"></i></a>
            <a href="https://twitter.com"><i className="fab fa-twitter-square"></i></a>
            <a href="https://linkedin.com"><i className="fab fa-linkedin"></i></a>
          </div>
      </div>
    </>
  );
};

export default withRouter(Footer);
