import React from 'react';
import { Link } from "react-router-dom";
import {Button} from 'element-react/next';

const QuestionButton = (props) => {
  return  (
            <Link to ={`${props.type}`}>
                <Button type="primary"  size="small"><i className={props.icon}></i>{props.name}</Button> 
            </Link>
  );
};

export default QuestionButton;
