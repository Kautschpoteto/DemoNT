import React from "react";

const Input = ({click}) => {
    const [city, setCity] = React.useState(''); 

    const handleChange = event => {
      
        setCity(event.target.value);
      
    };

    const handleClick = event => {
      if(city.trim()!==''){
          click(city);
      }
  };

    return <div>

    <h3>Stadtnamen eingeben</h3>  
  <div>
    <input onChange={handleChange}  type="text" name="stadt" value={city} />
    <button onClick={handleClick}>Stadt suchen</button>
  </div>
  </div>;
  }

export default Input;
