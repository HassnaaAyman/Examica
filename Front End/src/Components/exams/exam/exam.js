import React from 'react';
import { Card } from 'element-react/next';
import '../exam/exam.css';
import Moment from 'moment';
import {withRouter} from 'react-router-dom';

const Exam = (props) => {
  const onClick = () => {
    if(props.isExaminee) props.history.push(`/exams/examinee/${props.id}`);
    else props.history.push(`/organization/${props.match.params.id}/questions/add/exam/${props.id}`);
  }
  return (
    <div className="Exam" onClick={onClick}>
          <Card className="Exam-card" >
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRqw8Gh5-Kn4tJ0BA6JIAJvLzzRW33hIRTrEg5Oseg8QGOGp0KrkQ" alt="Exam Img"/>
            <div className="bottom clearfix Exam-Details">
              <span className="Exam-Name">{props.name}</span>
              <time className="time Exam-Date"><span className="Exam-Date-Label">From: </span>{Moment(props.startDate).format('DD MMMM, YYYY')}</time>
              <time className="time Exam-Date"><span className="Exam-Date-Label">To: </span>{Moment(props.endDate).format('DD MMMM, YYYY')}</time>
            </div>
          </Card>
    </div>
  );
};


export default withRouter(Exam);
