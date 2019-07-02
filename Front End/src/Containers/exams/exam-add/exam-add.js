import React, { Component } from 'react';
import {Form, Button, Input, DateRangePicker, Checkbox} from 'element-react/next';
import {toSqlFormat} from '../../../Helpers/convertDateTime';
import {connect} from 'react-redux';
import * as examActions from '../../../Store/Actions/examActions';
import './exam-add.css';

class ExamAdd extends Component {

  constructor(props) {
    super(props);

    this.state = {
      DateRangePicker: [],
      // Exam Object to bind form data with 
      form: {
        Name: "",
        Mark: "",
        StartDateTime: "",
        EndDateTime: "",
        IsPublic: false,
        OrganizationId: ""
      },
      // Form Rules and Validations to be used by Element UI Form
      rules: {
        Name: [
          { required: true, message: 'Please input the Exam Name', trigger: 'blur' },
          {
            validator: (rule, value, callback) => {
              if (value === '') {
                callback(new Error('Please input the Image URL'));
              } else {
                callback();
              }
            }
          }
        ],
        Mark: [
          { required: true, type: 'number', message: 'Please input the Exam Mark', trigger: 'blur' },
          {
            validator: (rule, value, callback) => {
              var mark = parseInt(value);
              setTimeout(() => {
                if (!Number.isInteger(mark)) {
                  callback(new Error('Please input digits'));
                } else {
                  if (mark < 1) {
                    callback(new Error('Mark must be greater than 0'));
                  } else {
                    callback();
                  }
                }
              }, 1000);
            }, trigger: 'change'
          }
        ]
      }
    };
  }

  // Event Handler to handle Form Submit Button by validate it and then submit the product
  handleSubmit(e) {
    e.preventDefault();
    this.refs.form.validate((valid) => {
      if (valid) {
        const exam = {...this.state.form};
        exam.OrganizationId = this.props.match.params.id;
        this.props.onAddExam(exam, this.props.token);
        this.props.history.push(`/organization/${this.props.match.params.id}/exams`);
      }
      else {
        return false;
      }
    });
  }

  // Event Handler to handle form reset to latest details of a product
  handleReset(e) {
    e.preventDefault();
    this.refs.form.resetFields();
    this.setState({
      form: {
        Name: "",
        Mark: "",
        StartDateTime: "",
        EndDateTime: "",
        IsPublic: false,
        OrganizationId: ""
      }
    });
  }

  // Event Handler to bind form inputs values to form object in state
  onChange(key, value) {
    if (key === "Mark" && !isNaN(parseInt(value))) value = parseInt(value);
    this.setState({
      form: Object.assign({}, this.state.form, { [key]: value })
    });
  }

  dateChanged = date => {
    if(date)
    {
      this.setState({
        DateRangePicker: date,
        form: {
          ...this.state.form,
          StartDateTime: toSqlFormat(date[0]),
          EndDateTime: toSqlFormat(date[1])
        }
      })
    }
  }

  render() {
    return (
      <Form className={`demo-ruleForm AddExam`} ref="form" model={this.state.form} rules={this.state.rules} labelWidth="100">
        <div className="title">Add Exam</div>
        <Form.Item label="Name" prop="Name">
          <Input type="text" value={this.state.form.Name} onChange={this.onChange.bind(this, 'Name')} autoComplete="off" />
        </Form.Item>
        <Form.Item label="Mark" prop="Mark">
          <Input type="number" value={this.state.form.Mark} onChange={this.onChange.bind(this, 'Mark')} autoComplete="off" />
        </Form.Item>
        <Form.Item label="Date Range">
            <DateRangePicker
              isShowTime={true}
              value={this.state.DateRangePicker}
              placeholder="Pick a range"
              onChange={this.dateChanged} 
            />
        </Form.Item>
        <Form.Item prop="IsPublic">
          <Checkbox checked={this.state.form.IsPublic} onChange={this.onChange.bind(this, 'IsPublic')}>Make it Public</Checkbox>
        </Form.Item>
        <Form.Item>
          <Button type="primary" onClick={this.handleSubmit.bind(this)}>Submit</Button>
          <Button onClick={this.handleReset.bind(this)}>Reset</Button>
        </Form.Item>
      </Form>
    );
  }
}


const mapStateToProps = state => {
  return {
    token: state.auth.token
  }
}

const mapDispatchToProps = dispatch => {
  return {
    onAddExam: (exam, token) => {dispatch(examActions.add(exam, token))}
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ExamAdd);
