import React from "react";


const Report = ({data}) => {

    if (data.city && !data.error){

      const weatherDate=new Date( Date.parse(data.date));
      const weatherDateString=`${weatherDate.toLocaleDateString("de-DE")} ${weatherDate.getHours()}:${weatherDate.getMinutes()}`;

      return <div style={style}><h3>aktuelle Wetterlage in {data.city}</h3>
      
      <table striped bordered hover>
      <thead>
        <tr>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
      <tr>
          <td>Datum</td>
          <td>{weatherDateString}</td>
        </tr>
        
        <tr>
          <td>Temperature</td>
          <td>{data.temperature}</td>
        </tr>
        <tr>
          <td>TemperatureMin</td>
          <td>{data.temperatureMin}</td>
        </tr>
        <tr>
          <td>TemperatureMax</td>
          <td>{data.temperatureMax}</td>
        </tr>
        <tr>
          <td>AirPressure</td>
          <td>{data.airPressure}</td>
        </tr>
        <tr>
          <td>Humidity</td>
          <td>{data.humidity}</td>
        </tr>
        <tr>
          <td>WindSpeed</td>
          <td>{data.windSpeed}</td>
        </tr>
        <tr>
          <td>WindDirection</td>
          <td>{data.windDirection}</td>
        </tr>

        <tr>
          <td>CloudCoverCondition</td>
          <td>{data.cloudCoverCondition}</td>
        </tr>
      </tbody>
    </table>
      </div>;
    }

    return <div></div>
  }

const style = {
  backgroundColor: '#aaaacc',
  color: '#333333',
  fontSize: 13,
  position: 'relative',
  width:'100%'
 
};


export default Report;
