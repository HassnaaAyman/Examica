import React from 'react';
import { shallow } from 'enzyme';
import QuestionSideNav from './QuestionSideNav';

describe('<QuestionSideNav />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionSideNav />);
    expect(wrapper).toMatchSnapshot();
  });
});
