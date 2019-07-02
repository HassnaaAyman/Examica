import React from 'react';
import { shallow } from 'enzyme';
import QuestionSettings from './QuestionSettings';

describe('<QuestionSettings />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionSettings />);
    expect(wrapper).toMatchSnapshot();
  });
});
