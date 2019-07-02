import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import { Layout } from 'element-react/next';
import SideMenu from '../../Components/QuestionsInfoFilter';
import Search from '../../Components/SearchBox'
import Pool from '../../Containers/PoolOfQuestions'
import './QuestionsPool.css'


class QuestionsPool extends Component {

  state = {
    data: []
  }


  componentDidMount() {
    this.setState({
      data: this.props.questions
    });
  }

  filter = (index) => {
    let newArr = [];
    let questionsArr = [...this.props.questions];
    if (index === 'ALL') {
      newArr = this.props.questions
    } else if (index === 'Beginner' || index === 'Intermediate' || index === 'Advanced') {
      newArr = questionsArr.filter((q) => {
        return q.Level === index
      })
    }
    else {
      newArr = questionsArr.filter((q) => {
        return q.Type === index
      })
    }
    this.setState({
      data: newArr
    });

  }
  render() {
    let QuestionTable = null;
    if (this.state.data) {
      QuestionTable = (this.state.data.length > 0) ? (<Pool List={this.state.data}></Pool>) : (<h3>No Questions Found</h3>);
    }
    return (
      <>
        <Layout.Row gutter="2" className="poolNav">
          <Layout.Col span="4" offset="12">
            <Search></Search>
          </Layout.Col>
          <Layout.Col span="4"><Link to="/CreateQuestion"><div className="hyperlinks"><i className="el-icon-plus"></i>Add New Question</div></Link></Layout.Col>

        </Layout.Row><br></br> <br></br>
        <Layout.Row gutter="22">
          <Layout.Col span="4" offset="1"><SideMenu onSelect={this.filter} defaultSelection={"ALL"}></SideMenu></Layout.Col>
          <Layout.Col span="18">{QuestionTable}</Layout.Col>
        </Layout.Row>
      </>
    );
  }
}


const mapStateToProps = state => {
  return ({
    questions: state.questions.questions,

  });
}

export default connect(mapStateToProps, null)(QuestionsPool);
