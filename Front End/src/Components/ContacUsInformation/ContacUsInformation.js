import React from 'react';
import './ContacUsInformation.css'


const ContacUsInformation = (props) => {
  return (
    <div className="ContacUsInformation">
        <div className="ContacUsInformation-Icon">
          <i className={props.icon}></i>
        </div>
        <div>
          <span>{props.name}</span>
          <p>{props.description}</p>
        </div>
    </div>
  );
};

export default ContacUsInformation;
