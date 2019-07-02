import React from 'react';
import { shallow } from 'enzyme';
import Complex from './Complex';

describe('<Complex />', () => {
  test('renders', () => {
    const wrapper = shallow(<Complex />);
    expect(wrapper).toMatchSnapshot();
  });
});
