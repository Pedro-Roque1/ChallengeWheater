import { Component } from '@angular/core';
import { WeatherService } from './services/weather.service';
import { HttpClient, HttpClientModule, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { WeatherComponentComponent } from './weather-component/weather-component.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    WeatherComponentComponent, 
    HttpClientModule     	
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    WeatherService
  ]
})
export class AppComponent {
  title = 'WeatherAppUI';
}
