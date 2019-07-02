import React from 'react';
import { shallow } from 'enzyme';
import LowerNavbar from './lowerNavbar';

describe('<LowerNavbar />', () => {
  test('renders', () => {
    const wrapper = shallow(<LowerNavbar />);
    expect(wrapper).toMatchSnapshot();
  });
});
