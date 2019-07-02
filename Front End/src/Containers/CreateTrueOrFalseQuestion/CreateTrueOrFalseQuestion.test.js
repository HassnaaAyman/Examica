import React from 'react';
import { shallow } from 'enzyme';
import CreateTrueOrFalseQuestion from './CreateTrueOrFalseQuestion';

describe('<CreateTrueOrFalseQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateTrueOrFalseQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
