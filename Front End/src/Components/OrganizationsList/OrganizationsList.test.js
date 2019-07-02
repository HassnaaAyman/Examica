import React from 'react';
import { shallow } from 'enzyme';
import OrganizationsList from './OrganizationsList';

describe('<OrganizationsList />', () => {
  test('renders', () => {
    const wrapper = shallow(<OrganizationsList />);
    expect(wrapper).toMatchSnapshot();
  });
});
