import React, { Component } from 'react';
import { Layout, Menu, Button} from 'element-react/next';
import QuestionsList from '../Questions-list';
import './QuestionTypes.css';
import {Switch, Route, withRouter} from 'react-router-dom';
import MCQMul from '../CreateChoiseQuestion';
import TOrF from '../CreateTrueOrFalseQuestion';
import {connect} from 'react-redux';
import * as examActions from '../../Store/Actions/examActions';

class QuestionTypes extends Component {
  state = {
    open: false,
    isExam: false,
    examId: ""
  }

  Toggle = () => {
    this.setState({
      open: !this.state.open
    })
  }

  onSubmit() {
    // this.props.onAdd(this.props.exam, this.props.token);
    this.props.history.push(`/organization/${this.props.match.params.id}/exams`);
  }

  componentDidMount () {
    if(this.props.match.params.examId) {
      this.props.onGetExam(this.props.match.params.examId ,this.props.token);
      this.setState({isExam: this.props.isExam, examId: this.props.match.params.examId});
    }
  }

  render() {
    return (
      <Layout.Row className="tac QuestionTypes" >
        <Layout.Col span={8} className="QuestionMenuCol" lg="10">
          <Menu defaultActive="1" className="el-menu-vertical-demo"  onOpen={this.Toggle.bind(this)}>
          <Menu.SubMenu index="1" title={<span><i className="far fa-question-circle"></i>Questions</span>}>
            <Menu.ItemGroup title="">
               <QuestionsList/>
            </Menu.ItemGroup>
          </Menu.SubMenu>
        </Menu>
        </Layout.Col>
        <Layout.Col span={16} className="AddToExamBtn">
            <Switch>
              <Route path="/organization/:id/questions/add/mcqMul" render={()=> <MCQMul type="MCQ_MultiAnswers" isExam={this.state.isExam} examId={this.state.examId} />} />
              <Route path="/organization/:id/questions/add/mcqSingle" render={()=> <MCQMul type="MCQ_SingleAnswer" isExam={this.state.isExam} examId={this.state.examId} />} />
              <Route path="/organization/:id/questions/add/tf" render={()=> <TOrF type="MCQ_SingleAnswer" isExam={this.state.isExam} examId={this.state.examId} />} /> 
              <Route path="/organization/:id/questions/add" render={() => (this.state.isExam) ? <Button onClick={this.onSubmit.bind(this)}>Submit Questions to Exam</Button> : null } /> 
            </Switch>
        </Layout.Col>
      </Layout.Row>
      
    )
  }
}


const mapStateToProps = state => {
  return {
    token: state.auth.token,
    exam: state.addExam
  };
};

const mapDispatchToProps = dispatch => {
  return {
    onGetExam: (examId, token) => dispatch(examActions.getById(examId, token)),
    // onAdd: (exam, token) => dispatch(examActions.assign(exam, token))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(QuestionTypes));
