import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';

interface DadJoke {
  joke: string;
  jokeLength: number;
}

interface DadJokeList {
  jokeList: DadJoke[];
  searchTerm: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {

  public singleJoke: string = '';
  public multipleJokes: DadJokeList = {
    jokeList: [],
    searchTerm: ''
  };

  constructor(private http: HttpClient) {}

  getDadJoke() {
    this.http.get('/dadjoke', { responseType: 'text' }).subscribe(
      (result) => {
        console.log(result);
        this.singleJoke = result;
      },
      (error) => {
        console.error(error);
        console.error(error.message);
      }
    );
  }

  getDadJokeSearch(searchTerm: string) {
    this.http.get<any>('/dadjoke/search', { params: { searchTerm } })
      .pipe(map(apiResult => ({
        jokeList: apiResult.results,
        searchTerm: apiResult.search_term
      }))
      ).subscribe(
        (result: DadJokeList) => {
          console.log(result);
          this.multipleJokes.jokeList = result.jokeList;
          this.multipleJokes.searchTerm = result.searchTerm;
          console.log(this.multipleJokes.jokeList[0].joke);
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
