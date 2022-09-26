import WeatherService from "../WebApi/WeatherService";

const useApiServices=()=>{

    // eslint-disable-next-line no-undef
    const x= new WeatherService(window.AppConfig);
    return x;
};

export default useApiServices;