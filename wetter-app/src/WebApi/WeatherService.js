
import axios from 'axios';

export default  class WeatherService  {
    
    queryApiUrl="";

    constructor(appConfig) {

        if(!appConfig){
            throw new Error("WeatherService: appConfig leer");
        };
    
        if(!appConfig.ApiUrl){
          throw new Error("WeatherService: Api-Url leer");
        };

         this.queryApiUrl = appConfig.ApiUrl;
    };
  
    async Query(city) {
        console.log(this.queryApiUrl);

        try {
            const response=await axios.get(this.queryApiUrl+"?city="+encodeURI(city));
            if(!response.data){
                return {errorMessage:"keine Daten empfangen"};
            }

            if(response.data.error){
              return {errorMessage:response.data.error};
            }

            return { data:response.data };

        } catch (e) {
          return {errorMessage:e.message};
        }
      };
  }
