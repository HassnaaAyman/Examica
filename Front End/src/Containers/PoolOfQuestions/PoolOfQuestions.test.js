import React from 'react';
import { shallow } from 'enzyme';
import PoolOfQuestions from './PoolOfQuestions';

describe('<PoolOfQuestions />', () => {
  test('renders', () => {
    const wrapper = shallow(<PoolOfQuestions />);
    expect(wrapper).toMatchSnapshot();
  });
});
