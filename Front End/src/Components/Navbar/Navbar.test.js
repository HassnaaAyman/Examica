import React from 'react';
import { shallow } from 'enzyme';
import Navbar from './Navbar';

describe('<Navbar />', () => {
  test('renders', () => {
    const wrapper = shallow(<Navbar />);
    expect(wrapper).toMatchSnapshot();
  });
});
