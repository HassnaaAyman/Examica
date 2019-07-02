import React from 'react';
import { shallow } from 'enzyme';
import LogoutButton from './LogoutButton';

describe('<LogoutButton />', () => {
  test('renders', () => {
    const wrapper = shallow(<LogoutButton />);
    expect(wrapper).toMatchSnapshot();
  });
});
