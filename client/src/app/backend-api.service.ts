import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class BackendApiService {

  private url = 'https://localhost:7030/WeatherForecast'

  constructor(private http:HttpClient) { }

  getForecast() {
    return this.http.get<Day[]>(this.url)
  }
}

interface Day {
  date: string
  tempratureC: number
  summary: string
}
