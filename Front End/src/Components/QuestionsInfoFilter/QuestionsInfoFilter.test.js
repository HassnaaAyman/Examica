import React from 'react';
import { shallow } from 'enzyme';
import QuestionsInfoFilter from './QuestionsInfoFilter';

describe('<QuestionsInfoFilter />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionsInfoFilter />);
    expect(wrapper).toMatchSnapshot();
  });
});
