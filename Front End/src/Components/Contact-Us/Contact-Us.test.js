import React from 'react';
import { shallow } from 'enzyme';
import ContacUs from './Contac-Us';

describe('<ContacUs />', () => {
  test('renders', () => {
    const wrapper = shallow(<ContacUs />);
    expect(wrapper).toMatchSnapshot();
  });
});
