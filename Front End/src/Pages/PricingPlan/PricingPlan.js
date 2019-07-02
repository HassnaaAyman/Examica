import React, { Component } from "react";
import PricingCard from "../../Components/PricingCard"
import { Layout } from "element-react/next";
import "./PricingPlan.css";

class PricingPlan extends Component {
  constructor(props) {
    super(props);
    this.state = {
      plans: [
        {
          id: 1,
          title: 'BASE',
          image: "https://i.ibb.co/cJfMr8L/undraw-dashboard-nklg.png",
          content: 'Get to know Type form',
          price: 'FREE',
          buttonText: 'Start Now!',
          route:'/register'
        },
        {
          id: 2,
          title: 'PRO',
          image: "https://i.ibb.co/0h0cNG1/undraw-setup-wizard-r6mr.png",
          content: 'More power and personalization',
          price: '59$/month',
          buttonText: 'Buy Now!',
          route:'/register'
        },
        {
          id: 3,
          title: 'PRO+',
          image: "https://i.ibb.co/zJk3Jsk/undraw-in-progress-ql66-1.png",
          content: 'Advanced features',
          price: '100$/month',
          buttonText: 'Buy Now!',
          route:'/register'
        },

      ]
    };
  }

  render() {
    let plans = this.state.plans.map(plan => (
      <Layout.Col key={plan.id} xs="20" sm="17" md="15" lg="7">
        <PricingCard
          title={plan.title}
          image={plan.image}
          content={plan.content}
          price={plan.price}
          buttonText={plan.buttonText}
          route={plan.route}
        />
      </Layout.Col>
    ));
    return (
      <Layout.Row className="PricingCardList">
        <Layout.Row>
          <h2 className="PricingCardList-title">Our plans</h2>
        </Layout.Row>
        <Layout.Row className="PricingCardList-featrues">{plans}</Layout.Row>
      </Layout.Row>
    );
  }
}

export default PricingPlan;
