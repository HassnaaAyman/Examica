import React from 'react';
import { shallow } from 'enzyme';
import SearchBox from './SearchBox';

describe('<SearchBox />', () => {
  test('renders', () => {
    const wrapper = shallow(<SearchBox />);
    expect(wrapper).toMatchSnapshot();
  });
});
