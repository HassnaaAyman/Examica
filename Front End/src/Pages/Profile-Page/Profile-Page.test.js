import React from 'react';
import { shallow } from 'enzyme';
import ProfilePage from './Profile-Page';

describe('<ProfilePage />', () => {
  test('renders', () => {
    const wrapper = shallow(<ProfilePage />);
    expect(wrapper).toMatchSnapshot();
  });
});
