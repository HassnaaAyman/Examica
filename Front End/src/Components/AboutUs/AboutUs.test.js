import React from 'react';
import { shallow } from 'enzyme';
import AboutUs from './AboutUs';

describe('<AboutUs />', () => {
  test('renders', () => {
    const wrapper = shallow(<AboutUs />);
    expect(wrapper).toMatchSnapshot();
  });
});
