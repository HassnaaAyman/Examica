import React from 'react';
import { shallow } from 'enzyme';
import OrganizationPage from './organizationPage';

describe('<OrganizationPage />', () => {
  test('renders', () => {
    const wrapper = shallow(<OrganizationPage />);
    expect(wrapper).toMatchSnapshot();
  });
});
