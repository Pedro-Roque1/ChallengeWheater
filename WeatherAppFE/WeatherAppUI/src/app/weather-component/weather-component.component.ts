import { Component } from '@angular/core';
import { WeatherService } from '../services/weather.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { weather } from '../models/weather';
import { favoriteCityRequest } from '../models/favoriteCity';

@Component({
  selector: 'app-weather-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './weather-component.component.html',
  styleUrl: './weather-component.component.css'
})
export class WeatherComponentComponent {
  public city: string = '';
  public forecasts: weather[] = [];
  public forecastsDays: weather[] = [];
  public isFavorite: boolean = false;
  public showfivedays = false;

  constructor(private weatherService: WeatherService) { }

  getWeather() {
    this.forecasts = [];
    this.weatherService.getWeather(this.city).subscribe({
        next: (data) => this.forecasts.push(data),
        error: (err) => console.error(`Erro: ${err}`),
        complete: () => console.log(this.forecasts)
    });
  }
  
  getForecast() {
    if (this.showfivedays) {
      this.showfivedays = false;
      return;
    }

    this.weatherService.getWeatherForecast(this.city).subscribe({
      next: (data) => this.forecastsDays = data,
      error: (err) => console.error(`Erro: ${err}`),
      complete: () => {
        this.showfivedays = true;
        console.log(this.forecastsDays);
      }
  });
  }

  toggleFavorite() {
    this.isFavorite = !this.isFavorite;
    let req = new favoriteCityRequest()
    req.name = this.city;
    req.userId = 1;

    this.weatherService.addFavoriteCity(req).subscribe(
      (response) => {
        console.log('Cidade favoritada com sucesso!', response);
      },
      (error) => {
        console.error('Erro ao favoritar cidade:', error);
      }
    );
  }

}
