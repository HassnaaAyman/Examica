import React from 'react';
import { shallow } from 'enzyme';
import CreateEssayQuestion from './CreateEssayQuestion';

describe('<CreateEssayQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateEssayQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
