import React from "react";
import { Layout, Button } from "element-react/next";
import "./Slider.css";
import {withRouter} from 'react-router-dom';
import { connect } from "react-redux";

const Slider = (props) => {
 
  const freeTrial = () => {
    if(props.isLoggedIn) props.history.push("/buyOrg");
    else props.history.push("/register");
  }
  return (
    <div>
      <Layout.Row className="Slider ">
        <div className="wraper">
          <h2>Online Exam Builder</h2>
          <p>
            Creating online exams & tests will be like a piece of cake <br />{" "}
            with our easy to use website.
          </p>
          <div className="buttons">
            <Button className="Buttonprimary" onClick={freeTrial}>Free Trial</Button>
          </div>
          <div className="background-1" />
        </div>
      </Layout.Row>
    </div>
  );
};

const mapStateToProps = state => {
  return {
    isLoggedIn: state.auth.isLoggedIn
  }
}

export default connect(mapStateToProps)(withRouter(Slider));
