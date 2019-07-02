import React from 'react';
import { shallow } from 'enzyme';
import QuestionsList from './Questions-list';

describe('<QuestionsList />', () => {
  test('renders', () => {
    const wrapper = shallow(<QuestionsList />);
    expect(wrapper).toMatchSnapshot();
  });
});
