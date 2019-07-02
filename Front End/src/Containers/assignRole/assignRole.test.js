import React from 'react';
import { shallow } from 'enzyme';
import AssignRole from './assignRole';

describe('<AssignRole />', () => {
  test('renders', () => {
    const wrapper = shallow(<AssignRole />);
    expect(wrapper).toMatchSnapshot();
  });
});
