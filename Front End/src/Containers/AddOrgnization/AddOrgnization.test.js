import React from 'react';
import { shallow } from 'enzyme';
import AddOrgnization from './AddOrgnization';

describe('<AddOrgnization />', () => {
  test('renders', () => {
    const wrapper = shallow(<AddOrgnization />);
    expect(wrapper).toMatchSnapshot();
  });
});
