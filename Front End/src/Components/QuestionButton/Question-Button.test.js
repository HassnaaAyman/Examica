import React from 'react';
import { shallow } from 'enzyme';
import QuestionButton from './Question-Button';

describe('<QuestionButton />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionButton />);
    expect(wrapper).toMatchSnapshot();
  });
});
