import React from 'react';
import { shallow } from 'enzyme';
import CreateMatchingQuestion from './CreateMatchingQuestion';

describe('<CreateMatchingQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateMatchingQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
