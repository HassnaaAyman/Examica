import React from 'react';
import './SearchBox.css';

const SearchBox = () => {

  return (
    <div className="SearchBox">
      <input placeholder="Search" className="SearchBox-input"></input><i className="fas fa-search SearchBox-icon"></i>
    </div>

  );

};

export default SearchBox;

