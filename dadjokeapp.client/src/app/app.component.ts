import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {

  public joke: string = '';

  constructor(private http: HttpClient) {}

  getDadJoke() {
    this.http.get('/dadjoke', { responseType: 'text' }).subscribe(
      (result) => {
        console.log(result);
        this.joke = result;
      },
      (error) => {
        console.error(error);
        console.error(error.message);
      }
    );
  }

  //will need to change this to a method that gets a dad joke from the controller.
  /*getForecasts() {
    this.http.get('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }*/

  title = 'dadjokeapp.client';
}
