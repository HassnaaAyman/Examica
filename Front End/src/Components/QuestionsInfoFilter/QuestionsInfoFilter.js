import React from 'react';
import { Menu } from 'element-react/next';

const QuestionsInfoFilter = (props) => {

  const onOpen = () => {

  }

  const onClose = () => {

  }

  return (
      <>
    <h3>Browse Criteria</h3>
    <Menu defaultActive="2" className="el-menu-vertical-demo" onOpen={onOpen} onClose={onClose} onSelect={props.onSelect} theme="dark">
      <Menu.SubMenu index="TYPE" title="Type">
        <Menu.Item index="TrueOrFalse">True/False</Menu.Item>
        <Menu.Item index="MCQ_MultiAnswers">Multiple Choices</Menu.Item>
        <Menu.Item index="MCQ_SingleAnswer">Single Choice</Menu.Item>
        <Menu.Item index="ALL">All</Menu.Item>
      </Menu.SubMenu >
      <Menu.SubMenu index="LEVEL" title="Level of Difficulty">
        <Menu.Item index="Beginner">Beginner</Menu.Item>
        <Menu.Item index="Intermediate">Intermediate</Menu.Item>
        <Menu.Item index="Advanced">Advanced</Menu.Item>
        <Menu.Item index="ALL">All</Menu.Item>
      </Menu.SubMenu>


    </Menu>
      </>
    )
  }



export default (QuestionsInfoFilter);
