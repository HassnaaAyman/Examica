import React, { Component } from "react";
import { Layout, Form, Input, Button, Switch } from "element-react/next";
import Questions from "../../Containers/Questions-list/Questions-list";
import './CreateComprehenssionQuestion.css';

class CreateComprehenssionQuestion extends Component {
  constructor(props) {
    super(props);
    this.state = {
      form: {
        Title: "",
        Type: "",
        IsPublic: false,
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
      }
    };
  }

  handleSubmit(e) {
    e.preventDefault();

    this.refs.form.validate(valid => {
      if (valid) {
        this.props.onAddQuestion(this.state.form, this.props.token);
      } else {
        console.log("error submit!!");
        return false;
      }
    });
  }

  handleReset(e) {
    e.preventDefault();
    this.refs.form.resetFields();
    this.setState({
      form: {
        Title: "",
        Type: "",
        IsPublic: false,
        OrganizationId: 2
      }
    });
  }

  onChange(key, value) {
    this.setState({
      form: Object.assign({}, this.state.form, { [key]: value })
    });
  }

  componentDidMount() {
    var qType = this.props.history.location.pathname.split("/");
    this.setState({
      form: {
        ...this.state.form,
        Type: qType[qType.length - 2]
      }
    });
  }

  render() { 
    return (
      <Form
        ref="form"
        className="en-US ComprehenssionQuestion"
        model={this.state.form}
        rules={this.state.rules}
        labelWidth="120"
      >
        <Form.Item className="ComprehenssionQuestion-form-controles">
          <Button type="primary" onClick={this.handleSubmit.bind(this)}>
            Create
          </Button>
          <Button onClick={this.handleReset.bind(this)}>Reset</Button>
        </Form.Item>

        <Form.Item prop="Type">
          <Layout.Row>
            <Layout.Col span={20}>
              <label>{this.state.form.Type}</label>
            </Layout.Col>
          </Layout.Row>
        </Form.Item>

        <Form.Item prop="Title">
          <Layout.Row>
            <Layout.Col span={15}>
              <Input
                type="textarea"
                value={this.state.form.Title}
                onChange={this.onChange.bind(this, "Title")}
                placeholder="Please Type Your Paragraph here followed by the question title... "
                autosize={{ minRows: 4, maxRows: 4 }}
              />
            </Layout.Col>
          </Layout.Row>
        </Form.Item>
       
        <Form.Item>
          <Layout.Col
            className="ComprehenssionQuestion-privacy"
            xs={5}
            sm={7}
            md={7}
            lg={4}
            prop="IsPublic"
          >
            <label className="CreateMultipleChoiseQuestion-Question-type">
              Question Privacy
            </label>
            <Switch
              onText="Private"
              offText="Public"
              value={this.state.form.IsPublic}
              onChange={this.onChange.bind(this, "IsPublic")}
            />
          </Layout.Col>
        </Form.Item>

        <Form.Item>
          <Layout.Row>
            <Layout.Col span={15}>
              <Questions />
            </Layout.Col>
          </Layout.Row>
        </Form.Item>
      
      </Form>
    );
  }
}

export default CreateComprehenssionQuestion;
