import React from 'react';
import { shallow } from 'enzyme';
import CreateQuestion from './CreateQuestion';

describe('<CreateQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
