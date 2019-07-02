import React from 'react';
import { shallow } from 'enzyme';
import Organization from './organization';

describe('<Organization />', () => {
  test('renders', () => {
    const wrapper = shallow(<Organization />);
    expect(wrapper).toMatchSnapshot();
  });
});
