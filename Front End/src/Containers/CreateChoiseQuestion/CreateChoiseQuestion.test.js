import React from 'react';
import { shallow } from 'enzyme';
import CreateMultipleChoiseQuestion from './CreateMultipleChoiseQuestion';

describe('<CreateMultipleChoiseQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateMultipleChoiseQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
