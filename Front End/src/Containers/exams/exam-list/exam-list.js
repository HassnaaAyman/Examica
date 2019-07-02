import React, { Component } from 'react';
import Exam from '../../../Components/exams/exam'
import { Layout } from 'element-react/next';
import { connect } from 'react-redux';
import './exam-list.css';

class ExamList extends Component {

  render() {
    let examlist = (<h2 className="NoExams">No Exams added yet</h2>);
    if (this.props.exams.length > 0) 
    {
        examlist = this.props.exams.map((ex,index) => {
          return (
        <Layout.Col key={index} span={5} offset={1}>
        <Exam
          id={ex.id}
          name={ex.name}
          startDate={ex.startDateTime}
          endDate={ex.endDateTime}
          numberOfQuestions={ex.questions.length}
          isExaminee={false}
          />
        </Layout.Col>
        );
      });
    }
    return (
      <Layout.Row className="ExamList">
        {examlist}
      </Layout.Row>
    );
  }
}
const mapStateToProps = state =>{
     return{
        exams : state.exams.orgExams,
        token: state.auth.token
   }
}

export default connect(mapStateToProps) (ExamList);
