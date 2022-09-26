import React from "react";
import './App.css';
import Headline from'./ui/Headline';
import Input from'./ui/Input';
import Report from'./ui/Report';
import useApiServices from'./Hooks/useApiServices';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'; //https://fkhadra.github.io/react-toastify/introduction/
import { SpinnerDotted } from 'spinners-react'; //https://github.com/adexin/spinners-react

const App = () => {
//function App() {
    const apiService = useApiServices();
    const [isLoading, setIsLoading] = React.useState(false); 
    const [weatherData, setWeatherData] = React.useState({}); 
      
    const handleClick =async (input) => {

      console.log(`Stadt ${input} `)
      setIsLoading(true);
      
      const result=await apiService.Query(input);

      setIsLoading(false);

      if(result.data){

        toast.success("Daten abgerufen", {
          autoClose: 1000,
          hideProgressBar: true,
          closeOnClick: true,
          });

        const x=result.data;
        setWeatherData(x);

      } else if(result.errorMessage){
        toast.error(`Api-Fehler ${result.errorMessage} `);
      }else{
        toast.error(`unbekannter Fehler `);
      }
  };


  return (
    <div className="App">
      <Headline text="aktuelle Wetterinformationen"/>
      <br/><br/><br/><br/>

      <Input click={handleClick}/>

      {isLoading
        ? <SpinnerDotted />
        : <div>
            <Report data={weatherData}/>
        </div>
      }

      <ToastContainer />
    </div>
  );
}

export default App;
