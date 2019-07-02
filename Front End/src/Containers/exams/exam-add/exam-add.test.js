import React from 'react';
import { shallow } from 'enzyme';
import ExamAdd from './exam-add';

describe('<ExamAdd />', () => {
  test('renders', () => {
    const wrapper = shallow(<ExamAdd />);
    expect(wrapper).toMatchSnapshot();
  });
});
