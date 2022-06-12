import { Component } from '@angular/core';
import { BackendApiService } from "./backend-api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  constructor(private backendApi:BackendApiService) {
  }

  ngOnInit() {
    this.backendApi.getForecast().subscribe((response) => {this.title = response[0].summary}, (error) => {console.log(error)})
  }
}
