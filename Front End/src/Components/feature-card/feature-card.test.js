import React from 'react';
import { shallow } from 'enzyme';
import FeatureCard from './feature-card';

describe('<FeatureCard />', () => {
  test('renders', () => {
    const wrapper = shallow(<FeatureCard />);
    expect(wrapper).toMatchSnapshot();
  });
});
