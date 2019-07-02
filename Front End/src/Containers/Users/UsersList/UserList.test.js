import React from 'react';
import { shallow } from 'enzyme';
import UserList from './UserList';

describe('<UserList />', () => {
  test('renders', () => {
    const wrapper = shallow(<UserList />);
    expect(wrapper).toMatchSnapshot();
  });
});
