import React from 'react';
import { shallow } from 'enzyme';
import TrueOrFalse from './TrueOrFalse';

describe('<TrueOrFalse />', () => {
  test('renders', () => {
    const wrapper = shallow(<TrueOrFalse />);
    expect(wrapper).toMatchSnapshot();
  });
});
