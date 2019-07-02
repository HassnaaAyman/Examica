import React from 'react';
import { shallow } from 'enzyme';
import PricingPlan from './PricingPlan';

describe('<PricingPlan />', () => {
  test('renders', () => {
    const wrapper = shallow(<PricingPlan />);
    expect(wrapper).toMatchSnapshot();
  });
});
