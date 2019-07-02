import React from 'react';
import { shallow } from 'enzyme';
import CreateImportQuestion from './CreateImportQuestion';

describe('<CreateImportQuestion />', () => {
  test('renders', () => {
    const wrapper = shallow(<CreateImportQuestion />);
    expect(wrapper).toMatchSnapshot();
  });
});
