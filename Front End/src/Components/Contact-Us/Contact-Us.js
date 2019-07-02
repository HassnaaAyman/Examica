import React from 'react';
import { Layout } from 'element-react/next'
import ContacUsInformation from '../ContacUsInformation'
import './Contact-Us.css'

const ContacUs = () => {
  return (
    <div className="ContactUs">

      <Layout.Row className="ContactUs-Info">
        <h3>Contact Information</h3>
        <p>We are Examica Team build this website to offer you an awesome service to faciliate your work! </p>
      </Layout.Row>
    

      <Layout.Row className="ContactUs-details">
        <Layout.Col span={8}>
          <ContacUsInformation icon={"fas fa-map-marker-alt"} description={"26 ahmedhelmy street , naser city ,cairo,Egypt"} name={"Address "} />
        </Layout.Col>
        <Layout.Col span={8}>
          <ContacUsInformation icon={"fas fa-phone"} description={"+02 875369208 - Central Office+02 353363114 - Fax"} name={"Phone Numbers "} />
        </Layout.Col>
        <Layout.Col span={8}>
          <ContacUsInformation icon={"far fa-envelope"} description={"support@globals.cominfo@globals.com"} name={"Email Address "} />
        </Layout.Col>
      </Layout.Row>
    </div>

  );
};

export default ContacUs;
