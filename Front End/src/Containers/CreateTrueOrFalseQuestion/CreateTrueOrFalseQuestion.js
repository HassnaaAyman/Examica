import React, { Component } from "react";
import { Layout, Button, Input, Radio, InputNumber, Switch } from 'element-react/next';
import { Rate, Form } from 'element-react';
import { connect } from "react-redux";
import * as questionActions from "../../Store/Actions/questionActions";
import {withRouter} from 'react-router-dom';

import './CreateTrueOrFalseQuestion.css'

class CreateTrueOrFalseQuestion extends Component {
  constructor(props) {
    super(props);

    this.state = {
      question: {
        Title: "",
        Type: "TrueOrFalse",
        Level: null,
        Mark: null,
        IsPublic: false,
        Options: [{ name: "" }],
        OrganizationId: "",
      },
    }
  }

  changeHandlerTitle = (e) => {
    const addquestion = { ...this.state.question };
    addquestion.Title = e;
    this.setState({ question: addquestion });
  }

  changeHandlerRate = (e) => {
    const addquestion = { ...this.state.question };
    addquestion.Level = e;
    this.setState({ question: addquestion })
  }

  changeHandlerInputNumber = (e) => {
    const addquestion = { ...this.state.question };
    addquestion.Mark = e;
    this.setState({ question: addquestion })
  }

  changeHandlerIsPublic = (e) => {
    const addquestion = { ...this.state.question };
    addquestion.IsPublic = e;
    this.setState({ question: addquestion })
  }

  onChange(value) {
    const addquestion = { ...this.state.question };
    addquestion.Options = [{ name: value }];
    this.setState({ question: addquestion });
  }


  onAddTrueOrFalseQuestion = (e) => {
    const ques = {...this.state.question};
    ques.OrganizationId = Number(this.props.match.params.id);
    if(this.props.isExam) {
      this.props.onAddQuestionToExam(this.props.examId ,ques, this.props.token);
      this.props.history.push(`/organization/${this.props.match.params.id}/questions/add/exam/${this.props.examId}`);
    }
    else {
      this.props.onAddQuestion(ques, this.props.token);
      this.props.history.push(`/organization/${this.props.match.params.id}/questions`);
    }
  }




  render() {
    return (
      <Form className="CreateTrueOrFalseQuestion" >

        <Layout.Row lg="10">
          <Layout.Col span={6} offset={14} className="CreateTrueOrFalseQuestion-col6-done">
            <Button type="success" className="CreateTrueOrFalseQuestion-done" onClick={this.onAddTrueOrFalseQuestion}>Create</Button>
          </Layout.Col>
        </Layout.Row>


        <Layout.Row className="CreateTrueOrFalseQuestion-questionhead">
          <Layout.Col span="12" push={3}>
            <Input  className="CreateTrueOrFalseQuestion-questionhead-input"  placeholder="Is [Your Statment Here] True or False ?" onChange={this.changeHandlerTitle} />
            <div className="CreateTrueOrFalseQuestion-questionhead-radios">
              <Radio value="True" checked={this.state.question.Options[0].name === "True"} onChange={this.onChange.bind(this)}>True</Radio>
              <Radio value="False" className="CreateTrueOrFalseQuestion-questionhead-optionB" checked={this.state.question.Options[0].name === "False"} onChange={this.onChange.bind(this)}>False</Radio>
            </div>
          </Layout.Col>
        </Layout.Row>

        <Layout.Row>
          <Layout.Col span="12">
            <div className="CreateTrueOrFalseQuestion-level">
              <label >Question Level Of Difficulty</label>
              <Rate
                className="CreateTrueOrFalseQuestion-rate"
                onChange={this.changeHandlerRate.bind(this)}
                showText={true}
                texts={["easy", "easy", "intermidiate", "intermidiate", "advanced"]}
              />
            </div>
            <div className="CreateTrueOrFalseQuestion-IsPuplic">
              <label >Question Privacy</label>
              <Switch
                className="CreateTrueOrFalseQuestion-switch"
                value={this.state.question.IsPublic}
                onText="Public"
                offText="Private"
                width={75}
                onChange={this.changeHandlerIsPublic.bind(this)}
              >
              </Switch>
            </div>
          </Layout.Col>

          <Layout.Col span="12" className="CreateTrueOrFalseQuestion-mark">
            <label >Question Mark</label>
            <InputNumber className="CreateTrueOrFalseQuestion-NumberInput" defaultValue={0} onChange={this.changeHandlerInputNumber.bind(this)} min="1" max="100"></InputNumber>
          </Layout.Col>
          
        </Layout.Row>
      </Form>
    )
  }
};


const mapStateToProps = state => {
  return {
    token: state.auth.token
  }
}

const mapDispatchToProps = dispatch => {
  return {
    onAddQuestion: (question, token) => { dispatch(questionActions.addNewQuestion(question, token)) },
    onAddQuestionToExam: (examId, question, token) => dispatch(questionActions.addNewQuestionToExam(examId, question, token))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(CreateTrueOrFalseQuestion));
