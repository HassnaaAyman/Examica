import React from 'react';
import { shallow } from 'enzyme';
import ExamList from './exam-list';

describe('<ExamList />', () => {
  test('renders', () => {
    const wrapper = shallow(<ExamList />);
    expect(wrapper).toMatchSnapshot();
  });
});
