import React, { Component } from "react";
import {
  Form,
  Input,
  Button,
  Select,
  Switch,
  Checkbox,
  Radio,
  InputNumber
} from "element-react/next";
import { Rate } from "element-react";
import "./CreateChoiseQuestion.css";
import { Layout } from "element-react/next";
import { connect } from "react-redux";
import * as questionActions from "../../Store/Actions/questionActions";
import {withRouter} from 'react-router-dom';

class CreateMultipleChoiseQuestion extends Component {
  constructor(props) {
    super(props);
    this.state = {
      form: {
        Title: "",
        Type: this.props.type,
        Level: 1,
        Mark: 1,
        IsPublic: false,
        Options: [{name: "Option1"}, {name: "Option2"}],
        OrganizationId: 2
      },
      rules: {
        Title: [
          {
            required: true,
            message: "Please input Question Title",
            trigger: "blur"
          }
        ],
        Level: [
          {
            required: true,
            message: "Please input the Question Difficulty level",
            trigger: "blur"
          }
        ],
        Mark: [
          {
            required: true,
            type: "number",
            message: "Please input the Exam Mark",
            trigger: "blur"
          }
        ]
      },
      index: -1,
      path: this.props.match.path
    };
  }

  componentDidUpdate() {
    const path= this.props.match.path;
    let Type;
    if(this.state.path !== path) 
    {
      if(path === "/organization/:id/questions/add/mcqSingle") Type="MCQ_SingleAnswer";
      else Type= "MCQ_MultiAnswers";
      this.setState({
        form: {
          ...this.state.form,
          Type: Type
        },
        path
      });
    }
  }

  handleSubmit(e) {
    e.preventDefault();

    this.refs.form.validate(valid => {
      if (valid) {
        const ques = {...this.state.form};
        ques.OrganizationId = Number(this.props.match.params.id);
        if(this.props.isExam) {
          this.props.onAddQuestionToExam(this.props.examId ,ques, this.props.token);
          this.props.history.push(`/organization/${this.props.match.params.id}/questions/add/exam/${this.props.examId}`);
        }
        else {
          this.props.onAddQuestion(ques, this.props.token);
          this.props.history.push(`/organization/${this.props.match.params.id}/questions`);
        }
      } else {
        console.log("error submit!!");
        return false;
      }
    });
  }

  onChange(key, value) {
    this.setState({
      form: Object.assign({}, this.state.form, { [key]: value })
    });
  }

  onOptionChange(index, value) {
    let addOption = [...this.state.form.Options];
    addOption[index].name = value
    this.setState({
      form: {
        ...this.state.form,
        Options: addOption
      }
    });
  }

  onRadioChange(index) {
    this.setState({
      index
    })
  }

  getOptionsType() {
    let answerOptions = null;
    if (this.state.form.Type === "MCQ_MultiAnswers") {
      answerOptions = this.state.form.Options.map( (opt, index) => (
        <Layout.Row key={index}>
          <Layout.Col span={1}>
            <Checkbox />
          </Layout.Col>
          <Layout.Col xs={9} sm={15} md={10} lg={10}>
            <Input placeholder="Enter your option" value={opt.name} onChange={this.onOptionChange.bind(this, index) } />
          </Layout.Col>
          <Layout.Col span={1}>
            <Button
              onClick={this.onOptionDeleted.bind(this, index)}
              type="primary"
              icon="delete"
            />
          </Layout.Col>
        </Layout.Row>
      ));
    } else {
      answerOptions = this.state.form.Options.map((opt, index) => (
        <Layout.Row key={index}>
          <Layout.Col span={1}>
            <Radio value="" checked={index === this.state.index} onChange={this.onRadioChange.bind(this, index)}/>
          </Layout.Col>
          <Layout.Col xs={9} sm={15} md={10} lg={10}>
            <Input placeholder="Enter your option" onChange={this.onOptionChange.bind(this, index) } />
          </Layout.Col>
          <Layout.Col span={1}>
            <Button
              onClick={this.onOptionDeleted.bind(this, index)}
              type="primary"
              icon="delete"
            />
          </Layout.Col>
        </Layout.Row>
      ));
    }
    return answerOptions;
  }

  onAddOption() {
    let addOption = [...this.state.form.Options];
    addOption.push({name: `Option${addOption.length+1}`});
    this.setState({
      form: {
        ...this.state.form,
        Options: addOption
      }
    });
  }

  onOptionDeleted(index) {
    let addOption = [...this.state.form.Options];
    addOption.splice(index, 1);
    if(index === this.state.index) this.setState({index: -1});
    this.setState({
      form: {
        ...this.state.form,
        Options: addOption
      }
    });
  }

  onCheckedBox(optNumb, event) {
    let value = document.getElementById("option" + optNumb).value;
    let options = [...this.state.form.Options];
    if (event) options.push({ name: value });
    else {
      let opt = options.findIndex(opt => opt.name === value);
      options.splice(opt, 1);
    }
    this.setState({
      isChecked: event,
      form: {
        ...this.state.form,
        Options: options
      }
    });
  }

  render() {
    let answerOptions = this.getOptionsType();
    return (
      <Form
        ref="form"
        className="en-US CreateMultipleChoiseQuestion"
        model={this.state.form}
        rules={this.state.rules}
        labelWidth="120"
      >
        <Form.Item className="CreateMultipleChoiseQuestion-form-controles">
          <Button type="primary" onClick={this.handleSubmit.bind(this)}>
            Create
          </Button>
        </Form.Item>

        <Form.Item prop="Title">
          <Layout.Row>
            <Layout.Col span={13}>
              <Input
                className="CreateMultipleChoiseQuestion-Title"
                value={this.state.form.Title}
                onChange={this.onChange.bind(this, "Title")}
                placeholder="Please Type Your Question Here"
              />
            </Layout.Col>

            <Layout.Col span={4} offset={1}>
              <label className="CreateMultipleChoiseQuestion-Mark">
                Question Mark
              </label>
            </Layout.Col>
            <Layout.Col span={3}>
              <InputNumber
                className="CreateMultipleChoiseQuestion-NumberInput"
                defaultValue={this.state.form.Mark}
                onChange={this.onChange.bind(this, "Mark")}
                min="1"
                max="100"
              />
            </Layout.Col>
          </Layout.Row>
        </Form.Item>

        <Form.Item>
          <label className="CreateMultipleChoiseQuestion-level-label">
            Question Level Of Difficulty
          </label>
          <Rate
            onChange={this.onChange.bind(this, "Level")}
            showText={true}
            texts={["easy", "easy", "intermidiate", "intermidiate", "advanced"]}
          />
        </Form.Item>
        <Form.Item>
          <div id="extraOption">{answerOptions}</div>
        </Form.Item>

        <Form.Item>
          <Button onClick={() => this.onAddOption()} type="primary" icon="plus">
            Add New Option
          </Button>
        </Form.Item>

        <Layout.Row>
          <Layout.Col xs={11} sm={12} md={8} lg={8}>
            <Form.Item prop="choiceType">
              <Select
                value={this.state.form.Type}
                onChange={this.onChange.bind(this, "Type")}
              >
                <Select.Option label="Multi Choices" value="MCQ_MultiAnswers" />
                <Select.Option label="Single Choice" value="MCQ_SingleAnswer" />
              </Select>
            </Form.Item>
          </Layout.Col>

          <Layout.Col
            className="CreateMultipleChoiseQuestion-privacy"
            xs={7}
            sm={9}
            md={9}
            lg={6}
            prop="IsPublic"
          >
            <label className="CreateMultipleChoiseQuestion-Question-type">
              Question Privacy
            </label>
            <Switch
              onText="Public"
              offText="Private"
              width={75}
              value={this.state.form.IsPublic}
              onChange={this.onChange.bind(this, "IsPublic")}
            />
          </Layout.Col>
        </Layout.Row>
      </Form>
    );
  }
}

const mapStateToProps = state => {
  return {
    token: state.auth.token
  };
};
const mapDispatchToProps = dispatch => {
  return {
    onAddQuestion: (question, token) => {
      dispatch(questionActions.addNewQuestion(question, token));
    },
    onAddQuestionToExam: (examId, question, token) => dispatch(questionActions.addNewQuestionToExam(examId, question, token))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(CreateMultipleChoiseQuestion));
