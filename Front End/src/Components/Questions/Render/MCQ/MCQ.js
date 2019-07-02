import React, {Component} from 'react';
import './MCQ.css';
import {Radio, Checkbox} from 'element-react/next';

class  MCQ extends Component {
  constructor(props) {
    super(props);
    this.state = {
      answers: []
    }
  }
  onChange(type, optionId) {
    if(type === "single") 
    {
      this.setState({
        answers: [optionId]
      })
    }
  }
  render() {
      let options;
      if(this.props.question.type === "MCQ_SingleAnswer")
      {
        options = this.props.question.options.map(option =>  <div key={option.id}><Radio checked={this.state.answers.includes(option.id)} onChange={this.onChange.bind(this, "single", option.id)} value={option.id}><span className="MCQ-Option">{option.name}</span></Radio></div>);
      }
      else
      {
        options =(
          <Checkbox.Group value={this.state.checkList}>
            {this.props.question.options.map(option => <Checkbox className="MCQ-CheckBox" key={option.id} label={option.name}></Checkbox>)}
          </Checkbox.Group>
        ); 
      }
      return (
        <div className="MCQ">
        <span className="MCQ-Title">
          {this.props.index}) {this.props.question.title}
        </span>
        <ul>
          {options}
        </ul>
      </div>
    );
  }
};

MCQ.defaultProps = {
  index: 1,
  question: {
    id: 1,
    title: "Are you an ITIan ?",
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
  }
}

export default MCQ;
