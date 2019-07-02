import React from 'react';
import { shallow } from 'enzyme';
import FeatureCardList from './feature-card-list';

describe('<FeatureCardList />', () => {
  test('renders', () => {
    const wrapper = shallow(<FeatureCardList />);
    expect(wrapper).toMatchSnapshot();
  });
});
