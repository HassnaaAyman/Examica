import React, { Component, Fragment } from "react";
import { Layout } from "element-react";
import { connect } from "react-redux";
import Exam from "../../Components/exams/exam/exam";
import * as examActions from "../../Store/Actions/examActions";
import "./Profile-Page.css";

class ProfilePage extends Component {
  
  componentDidMount() {
    this.props.getUserExams(this.props.token);
  }

  render() {
    let examlist = (<div>You Have no exams to be taken</div>);
    if (this.props.exams && this.props.exams.length > 0) {
      examlist = this.props.exams.map((ex, index) => {
        return (
          <Layout.Col key={index} span={4} offset={1}>
            <Exam
              id={ex.id}
              name={ex.name}
              startDate={ex.startDateTime}
              endDate={ex.endDateTime}
              numberOfQuestions={ex.questions.length}
              isExaminee
            />
          </Layout.Col>
        );
      });
    }

    return (
      <Fragment>
        <Layout.Row className="ProfilePage">
          <Layout.Col sm={11} md={8} lg={5}>
            <Layout.Row>
              <Layout.Col className="ProfilePage-left-section">
                <div className="ProfilePage-pic" />
              </Layout.Col>
            </Layout.Row>
            <Layout.Row>
              <Layout.Col>
                <div className="ProfilePage-info">
                  <h3 className="ProfilePage-info-name">
                    {this.props.activeUser.userName}
                  </h3>
                  <p>
                    <i className="fas fa-envelope ProfilePage-info-icon" />
                    {this.props.activeUser.email}
                  </p>
                </div>
              </Layout.Col>
            </Layout.Row>
          </Layout.Col>

          <Layout.Col
            sm={11}
            md={14}
            lg={17}
            className="ProfilePage-right-section"
          >
            {examlist}
          </Layout.Col>
        </Layout.Row>
      </Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    token: state.auth.token,
    userId: state.auth.userId,
    orgId: state.organizations.currentOrgnaziation,
    activeUser: state.users.activeUser,
    exams: state.users.exams
  };
};

const mapDispatchToProps = dispatch => {
  return {
    getUserExams: (token) => dispatch(examActions.getByUser(token))
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProfilePage);
