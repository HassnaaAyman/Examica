import React from 'react';
import { shallow } from 'enzyme';
import RenderExam from './render-exam';

describe('<RenderExam />', () => {
  test('renders', () => {
    const wrapper = shallow(<RenderExam />);
    expect(wrapper).toMatchSnapshot();
  });
});
