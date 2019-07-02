import React from 'react';
import { shallow } from 'enzyme';
import WhyToUse from './why-to-use';

describe('<WhyToUse />', () => {
  test('renders', () => {
    const wrapper = shallow(<WhyToUse />);
    expect(wrapper).toMatchSnapshot();
  });
});
