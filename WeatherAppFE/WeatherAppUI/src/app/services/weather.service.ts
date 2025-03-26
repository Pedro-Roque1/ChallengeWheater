import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { weather } from "../models/weather";
import { favoriteCityRequest } from "../models/favoriteCity";

@Injectable({
    providedIn: 'root'
})
export class WeatherService {
    private apiUrl = "https://localhost:44341/api/"
    constructor(private http: HttpClient) { }

    public getWeather(city : string) : Observable<weather> {
        return this.http.get<any>(`${this.apiUrl}Weather/current/${city}`);
    }

    public getWeatherForecast(city : string) : Observable<weather[]> {
        return this.http.get<any>(`${this.apiUrl}Weather/forecast/${city}/5`);
    }

    addFavoriteCity(data: favoriteCityRequest) : Observable<any> {
        return this.http.post<any>(`${this.apiUrl}FavoriteCity/create/`, data);
    }

}