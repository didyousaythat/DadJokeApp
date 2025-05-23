import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
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

  public listOfShortJokes: string[] = [];
  public listOfMediumJokes: string[] = [];
  public listOfLongJokes: string[] = [];

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  getDadJoke() {
    //calls the controller endpoint to get a single joke.
    this.http.get('/dadjoke', { responseType: 'text' }).subscribe(
      (result) => {
        this.singleJoke = result;
      },
      (error) => {
        console.error(error);
        console.error(error.message);
      }
    );
  }

  getDadJokeSearch(inputSearchTerm: string) {

    if (inputSearchTerm == undefined) {
      inputSearchTerm = "";
    }

    //Sanatize the input
    //regex given removes any non alphanumeric characters
    var searchTerm = inputSearchTerm.replace(/[^\w\s]/gi, '');

    //checks if search is longer than one word and alerts the user if it is.
      //empty strings are technically valid as they will return a full list of jokes.
    if (searchTerm.split(' ').length > 1) {
      return alert('Please enter a single word.');
    }

    //calls the search controller endpoint and uses url parameters to pass the search term.
    this.http.get<any>('/dadjoke/search', { params: { searchTerm } })
      .pipe(map(apiResult => ({
        //maps the api result to the interface values
        jokeList: apiResult.results,
        searchTerm: apiResult.search_term
      }))
      ).subscribe(
        (result: DadJokeList) => {
          //assign the result to the multipleJokes object
          this.multipleJokes.jokeList = result.jokeList;
          this.multipleJokes.searchTerm = result.searchTerm;

          //reset the lists each time method is called so that we don't just keep stacking onto the lists with each new request.
          this.listOfShortJokes = [];
          this.listOfMediumJokes = [];
          this.listOfLongJokes = [];

          //sort the jokes by checking the length enum values and putting them in the associated lists
          for (var joke of this.multipleJokes.jokeList) {
            if (joke.jokeLength == 0) {
              this.listOfShortJokes.push(joke.joke);
            }
            else if (joke.jokeLength == 1) {
              this.listOfMediumJokes.push(joke.joke);
            }
            else {
              this.listOfLongJokes.push(joke.joke);
            }
          }
      },
      (error) => {
        console.error(error);
        console.error(error.message);
      }
    );
  }

  highlightTerm(joke: string, term: string): SafeHtml {
    if (!term) {
      return joke;
    }
    //create a case-insensitive global regex
    const regex = new RegExp(term, 'gi');
    //replace matches with highlighted span
    const highlighted = joke.replace(
      regex,
      match => `<span style="background-color: yellow">${match}</span>`
    );
    //mark the HTML as safe for Angular
      //not reccommended to utilize this in applications handling sensitive information.
    return this.sanitizer.bypassSecurityTrustHtml(highlighted);
  }

  title = 'dadjokeapp.client';
}
