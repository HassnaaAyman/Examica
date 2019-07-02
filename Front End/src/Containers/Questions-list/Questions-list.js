import React, { Component } from "react";
import { connect } from "react-redux";
import { Layout } from "element-react/next";
import QuestionButton from "../../Components/QuestionButton";
import "./Questions-list.css";
import {withRouter} from 'react-router-dom';

class QuestionsList extends Component {
  state = {
    questionButtons: [
      {
        id: 1,
        name: "MCQ SingleAns",
        icon: "fas fa-dot-circle",
        type: `/organization/${this.props.match.params.id}/questions/add/mcqSingle`
      },
      {
        id: 2,
        name: "MCQ MultiAns",
        icon: "fas fa-check-square",
        type: `/organization/${this.props.match.params.id}/questions/add/mcqMul`
      },
      {
        id: 3,
        name: "TrueOrFalse",
        icon: "fas fa-times-circle",
        type: `/organization/${this.props.match.params.id}/questions/add/tf`
      }
    ]
  };
  render() {
    const questionButton = this.state.questionButtons.map((btn,index) => {
      return (
      <Layout.Col key={index} span="10" offset="1"  lg="10">
      <QuestionButton
        id={btn.id}
        name={btn.name}
        icon={btn.icon}
        type={btn.type}
        className="questionlist"
        />
        </Layout.Col>
      );
    });
    return <Layout.Row className="QuestionList">{questionButton}</Layout.Row>;
  }
}

const mapStateToProps = state => {
  return {
    questionbuttons: state.questionbuttons
  };
};

export default connect(
  mapStateToProps,
  null
)(withRouter(QuestionsList));
