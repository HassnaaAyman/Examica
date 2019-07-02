import React from 'react';
import { shallow } from 'enzyme';
import UpperNavbar from './upperNavbar';

describe('<UpperNavbar />', () => {
  test('renders', () => {
    const wrapper = shallow(<UpperNavbar />);
    expect(wrapper).toMatchSnapshot();
  });
});
