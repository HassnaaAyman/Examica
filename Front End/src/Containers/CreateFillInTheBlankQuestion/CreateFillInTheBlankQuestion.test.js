import React from 'react';
import { shallow } from 'enzyme';
import CreateFillInTheBlankQuestion from './CreateFillInTheBlankQuestion';

describe('<CreateFillInTheBlankQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateFillInTheBlankQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
