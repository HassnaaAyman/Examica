import React from 'react';
import { shallow } from 'enzyme';
import UntiteldQuestion from './Untiteld-Question';

describe('<UntiteldQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<UntiteldQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
