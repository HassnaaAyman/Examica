import React, { Component } from 'react';
import {Button, Layout } from 'element-react/next';
import './organizationPage.css'
import {connect} from 'react-redux';
import {withRouter} from 'react-router-dom';
// import * as orgActions from '../../Store/Actions/organizationActions';

class OrganizationPage extends Component {
  
  nextPath(path) {
    this.props.history.push(path);
  }


  render() {
    let PricingPlanId = this.props.org.pricingPlanId;
    let PricingPlan= <div></div>;
    switch (PricingPlanId) {
      case 2:
        PricingPlan = "Basic";
        break;
        case 3:
          PricingPlan = "Pro";
          break;
          case 4:
            PricingPlan = "Pro +";
            break;
      default:
        break;
    }
    let buttons = null;
    if(this.props.roles && this.props.roles.isExaminer) {
      buttons= (
        <>
        <Button onClick={this.nextPath.bind(this, `/organization/${this.props.org.id}/questions/add`)} className="Buttonsecondry">Question  <i className="fas fa-plus"></i></Button> 
        <Button onClick={this.nextPath.bind(this, `/organization/${this.props.org.id}/exams/add`)} className="Buttonsecondry">Exam  <i className="fas fa-plus"></i></Button> 
        </>
      );
    }
    return (
      <div className="organization-page">
      <Layout.Row>
        <Layout.Col span="4" offset="20">
        <div className="grid-content bg-purple-light buttons">
          {buttons}
        </div>
        </Layout.Col>
      </Layout.Row>
      <Layout.Row className="container">
        <Layout.Col offset="5" lg={8}>
          <div className="OrganizationPage-pic"/> 
        </Layout.Col>
        <Layout.Col lg={10}>
        <Layout.Row className="container-rows">
        <span>Company Name: </span>  {this.props.org.name}
        </Layout.Row>
        <Layout.Row className="container-rows">
        <span>Pricing Plan: </span>  {PricingPlan}
        </Layout.Row>
        </Layout.Col>
      </Layout.Row>
      </div>
    )
  }
}


const mapStateToprops = state => {
  return {
    org: state.organizations.currentOrgnaziation,
    isLoggedIn: state.auth.isLoggedIn,
    roles: state.users.activeUser.roles
  }
}

export default withRouter(connect(mapStateToprops)(OrganizationPage));
