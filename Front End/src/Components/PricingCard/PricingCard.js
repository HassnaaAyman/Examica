import React from 'react';
import { Card , Button} from "element-react/next";
import {Link} from 'react-router-dom'
import './PricingCard.css'

const PricingCard = (props) => {
  return (
    <Card className="PricingCard">
      <span className="PricingCard-Title">{props.title}</span>
      <img className="PricingCard-Image" src={props.image} alt="timeIcon" />
      <div className="PricingCard-content" style={{ padding: 14 }}>
        <div className="bottom clearfix">
          <p className="PricingCard-Content">{props.content}</p>
          <h3>{props.price}</h3>
          <Link to={props.route} ><Button type="text" className="PricingCard-button">{props.buttonText}</Button></Link>
        </div>
      </div>
    </Card>
  )
};

export default PricingCard;
