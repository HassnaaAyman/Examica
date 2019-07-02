import React from 'react';
import './QuestionSideNav.css';
import {Layout} from 'element-react/next';


const QuestionSideNav = () => {
  return (
    <Layout.Row >
    <Layout.Col span="8"><div className="grid-content bg-purple"></div><div className="QuestionSideNav"></div> </Layout.Col>
    </Layout.Row>
  );
};

export default QuestionSideNav;
