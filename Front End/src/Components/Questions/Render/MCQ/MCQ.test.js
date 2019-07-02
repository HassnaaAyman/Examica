import React from 'react';
import { shallow } from 'enzyme';
import MCQ from './MCQ';

describe('<MCQ />', () => {
  test('renders', () => {
    const wrapper = shallow(<MCQ />);
    expect(wrapper).toMatchSnapshot();
  });
});
