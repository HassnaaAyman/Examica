import React from 'react';
import './Complex.css';
import TrueOrFalse from '../TrueOrFalse';
import MCQ from '../MCQ';

const Complex = (props) => {
  let questions = props.complex.questions.map((q, i) => {
    var question;
    switch(q.type) {
      case "TrueOrFalse":
        question = <TrueOrFalse key={q.id} index={i} question={q} />
          break;
      case "MCQ_SingleAnswer":
      case "MCQ_MultiAnswers":
        question = <MCQ key={q.id} index={i+1} question={q} />
        break;
      default:
        break;
    }
    return question;
  });
  return (
    <div className="Complex">
      <span className="Complex-Title">
        {props.index}) {props.complex.title}
      </span>
      {questions}
    </div>
  );
};

Complex.defaultProps = {
  index: 1,
  complex: {
    title: "I'm a Complex Question",
    questions:[
      {
        id: 1,
        title: "Are you an ITIan T&F?",
        type: "TrueOrFalse",
        level: "Advanced",
        mark: 5,
        isPublic: true,
        organizationId: 1,
        options: [
          {id: 1, name: "Option 1"},
          {id: 2, name: "Option 2"},
          {id: 3, name: "Option 3"},
          {id: 4, name: "Option 4"}
        ]
      },
      {
        id: 1,
        title: "Are you an ITIan MCQ single?",
        type: "MCQ_SingleAnswer",
        level: "Advanced",
        mark: 5,
        isPublic: true,
        organizationId: 1,
        options: [
          {id: 1, name: "Option 1"},
          {id: 2, name: "Option 2"},
          {id: 3, name: "Option 3"},
          {id: 4, name: "Option 4"}
        ]
      },
      {
        id: 1,
        title: "Are you an ITIan MCQ Mul? ",
        type: "MCQ_MultipleAnswers",
        level: "Advanced",
        mark: 5,
        isPublic: true,
        organizationId: 1,
        options: [
          {id: 1, name: "Option 1"},
          {id: 2, name: "Option 2"},
          {id: 3, name: "Option 3"},
          {id: 4, name: "Option 4"}
        ]
      }
    ] 
  }
}

export default Complex;
