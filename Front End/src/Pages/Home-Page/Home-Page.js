import React, { Fragment } from "react";
import Slider from "./Slider/Slider";
import WhyToUse from "./why-to-use/why-to-use";
import Features from './feature-card-list/feature-card-list'

const HomePage = () => {
  return (
    <Fragment>
      <Slider />
      <WhyToUse />
      <Features/>
    </Fragment>
    
  );
};

export default HomePage;
