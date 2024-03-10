export interface IWeather {
    id?: string;
    date: Date;
    temperature: number;
    humidity: number;
    td: number;
    pressure: number;
    windDirection: string;
    windSpeed: number;
    cloudy: number;
    h: number;
    vv: number;
    weatherCondition: string;
}