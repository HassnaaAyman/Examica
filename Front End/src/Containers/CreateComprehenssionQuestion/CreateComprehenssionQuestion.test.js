import React from 'react';
import { shallow } from 'enzyme';
import CreateComprehenssionQuestion from './CreateComprehenssionQuestion';

describe('<CreateComprehenssionQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateComprehenssionQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
