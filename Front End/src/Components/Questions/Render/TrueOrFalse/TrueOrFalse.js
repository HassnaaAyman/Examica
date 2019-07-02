import React from 'react';
import {Switch} from 'element-react/next';
import './TrueOrFalse.css';

const TrueOrFalse = (props) => {
  // let answer = true;
  // const changeAnswer = (value) => {
  //   answer = value;
  //   console.log(answer);
  // }
  return (
    <div className="TrueOrFalse">
      <span className="TrueOrFalse-Title">
        {props.index}) {props.question.title}
      </span>
      <Switch
        value={true}
        // onChange={changeAnswer}
        width={65}
        onText="True"
        offText="False"
        onColor="#13ce66"
        offColor="#ff4949">
      </Switch>
    </div>
  );
};

TrueOrFalse.defaultProps = {
  index: 1,
  question: {
    id: 1,
    title: "Are you an ITIan ?",
    type: "T&F",
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
}

export default TrueOrFalse;
