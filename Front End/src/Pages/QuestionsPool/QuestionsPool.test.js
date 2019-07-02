import React from 'react';
import { shallow } from 'enzyme';
import QuestionsPool from './QuestionsPool';

describe('<QuestionsPool />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionsPool />);
    expect(wrapper).toMatchSnapshot();
  });
});
