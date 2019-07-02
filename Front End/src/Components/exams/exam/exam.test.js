import React from 'react';
import { shallow } from 'enzyme';
import Exam from './exam';

describe('<Exam />', () => {
  test('renders', () => {
    const wrapper = shallow(<Exam />);
    expect(wrapper).toMatchSnapshot();
  });
});
