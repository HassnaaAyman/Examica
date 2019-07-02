import React, { Component } from 'react';
import { Button, Table, MessageBox, Message } from "element-react/next";
import { connect } from 'react-redux';
import './PoolOfQuestions.css';
import { deleteQuestion } from '../../Store/Actions/questionActions'

class PoolOfQuestions extends Component {

  state = {
    columns: [
      {
        label: "Title",
        prop: "Title",
        width: 500,
        render: function (data) {
          return (
            <span>{data.title}</span>
          )
        }
      },
      {
        label: "Level",
        prop: "Level",
        width: 180,
        align: "center",
        render: function (data) {
          return <span>{data.level}</span>
        }
      },
      {
        label: "Type",
        prop: "Type",
        width: 180,
        align: "center",
        render: function (data) {
          return <span>{data.type}</span>
        }
      },
      {
        label: "Mark",
        prop: "Mark",
        width: 180,
        align: "center",
        render: function (data) {
          return <span>{data.mark}</span>
        }
      },
      {
        label: "IsPublic",
        prop: "isPublic",
        width: 180,
        align: "center",
        render: function (data) {
          return <span>{data.isPublic ? "True" : "False"}</span>
        }
      },
      {
        label: "Delete",
        align: "center",
        render: (data) => {
          if(data.organizationId.toString() === this.props.match.params.id)
          {

            return (
              <span>
              <Button className="questionOptions" icon="delete" size="small" onClick={this.deleteQuestion.bind(this, data.id)}></Button>
            </span>
          )}
          else {
            return (
              <span>
              <Button disabled className="questionOptions" icon="delete" size="small" onClick={this.deleteQuestion.bind(this, data.id)}></Button>
            </span>
          )}
        }
      }
    ],
    data: []
  }


  componentDidMount() {
    this.setState({
      data: this.props.questionsPool
    })
  }

  deleteQuestion(id) {
    MessageBox.confirm('Are you sure you want to delete?', 'Warning', {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning'
    }).then(() => {
      this.props.delete(id, this.props.token);
      Message({
        type: 'success',
        message: 'Deleted Successfully'
      });
    }).catch(() => {
      
    });
  }
  render() {
    let table = (<h2 className="NoQuestions">No Questions added yet!</h2>);
    if(this.props.questionsPool.length > 0 ) table = (
      <Table
        style={{ width: '100%' }}
        columns={this.state.columns}
        data={this.props.questionsPool}
        border={true}
        fit={true}
      />
    );
    return (
      <div className="QPool">
        {table}
      </div>
    );
  }
}


const mapStateToProps = state => {
  return {
    questionsPool: state.questions.questions,
    token: state.auth.token
  }
};
const mapDispatchToProps = dispatch => {
  return ({
    delete: (id, token) => dispatch(deleteQuestion(id, token))
    
  });
}
export default connect(mapStateToProps, mapDispatchToProps)(PoolOfQuestions);
