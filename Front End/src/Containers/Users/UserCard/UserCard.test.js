import React from 'react';
import { shallow } from 'enzyme';
import UserCard from './UserCard';

describe('<UserCard />', () => {
  test('renders', () => {
    const wrapper = shallow(<UserCard />);
    expect(wrapper).toMatchSnapshot();
  });
});
