import React from "react";
import "./feature-card.css";
import { Card } from "element-react/next";

const FeatureCard = props => {
  return (
    <Card className="FeatureCard">
      <img className="FeatureCard-Image" src={props.image} alt="timeIcon" />
      <div className="FeatureCard-content" style={{ padding: 14 }}>
        <span className="FeatureCard-Title">{props.title}</span>
        <div className="bottom clearfix">
          <p className="FeatureCard-Content">{props.content}</p>
        </div>
      </div>
    </Card>
  );
};

export default FeatureCard;
