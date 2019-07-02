import React from 'react';
import { shallow } from 'enzyme';
import MiddleNavbar from './MiddleNavbar';

describe('<MiddleNavbar />', () => {
  test('renders', () => {
    const wrapper = shallow(<MiddleNavbar />);
    expect(wrapper).toMatchSnapshot();
  });
});
