import React from 'react';
import { shallow } from 'enzyme';
import ContacUsInformation from './ContacUsInformation';

describe('<ContacUsInformation />', () => {
  test('renders', () => {
    const wrapper = shallow(<ContacUsInformation />);
    expect(wrapper).toMatchSnapshot();
  });
});
