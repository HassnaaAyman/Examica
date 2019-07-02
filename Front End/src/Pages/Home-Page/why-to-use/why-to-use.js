import React from "react";
import { Layout, Button } from "element-react/next";
import "./why-to-use.css";
const WhyToUse = props => {
  return (
    <Layout.Row className="WhyToUse">
      <Layout.Col xs="17" sm="15" md="15" lg="9" span="9" className="WhyToUse-paragraphs">
        <h2 className="WhyToUse-title">Why To use?</h2>
        <p>
          Do you need some serious testing software for your students? Or do you
          need to hand out a lot of certificates?
          <br />
          <br />
          Do you want to easily setup your exam software and invite your users
          or do you need to connect your testing software with your internal
          software? Start using the Online Exam Builder all this is within
          reach.
          <br /> <br /> Take a look at all the features or ask for a demo.
        </p>
        <Button className="Buttonprimary">Free Trial</Button>
      </Layout.Col>
      <Layout.Col span="13">
        <img
          className="WhyToUse-Img"
          src="https://i.ibb.co/k9XRZDF/undraw-online-page-cq94.png"
          alt="whyToUe"
        />
      </Layout.Col>
    </Layout.Row>
  );
};

export default WhyToUse;
