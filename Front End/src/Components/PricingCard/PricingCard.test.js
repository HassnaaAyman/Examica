import React from 'react';
import { shallow } from 'enzyme';
import PricingCard from './PricingCard';

describe('<PricingCard />', () => {
  test('renders', () => {
    const wrapper = shallow(<PricingCard />);
    expect(wrapper).toMatchSnapshot();
  });
});
