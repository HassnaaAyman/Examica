import React from 'react';
import { shallow } from 'enzyme';
import Exams from './Exams';

describe('<Exams />', () => {
  test('renders', () => {
    const wrapper = shallow(<Exams />);
    expect(wrapper).toMatchSnapshot();
  });
});
