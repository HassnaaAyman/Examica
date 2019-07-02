import React from 'react';
import ExamList from '../../Containers/exams/exam-list'
import { Layout } from 'element-react/next';
import Navbar from '../../Components/Navbar';
import MiddleNavbar from '../../Components/Navbar/MiddleNavbar';


const Exams = () => {
  return (
    <>
      <Layout.Row>
        <Layout.Col span={24}>
          <Navbar />
        </Layout.Col>
      </Layout.Row>
      <MiddleNavbar />
      <ExamList />

    </>
  );
};

export default Exams;
