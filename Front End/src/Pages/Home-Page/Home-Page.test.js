import React from 'react';
import { shallow } from 'enzyme';
import HomePage from './Home-Page';

describe('<HomePage />', () => {
  test('renders', () => {
    const wrapper = shallow(<HomePage />);
    expect(wrapper).toMatchSnapshot();
  });
});
