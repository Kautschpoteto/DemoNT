import React from "react";

const Headline = ({text}) => {
    return <div style={style}><h3>{text}</h3></div>;
}

const style = {
  backgroundColor: '#8080ff',
  color: '#ffffff',
  fontSize: 20,
  position: 'absolute',
  width:'100%',
  top:0
};

export default Headline;
