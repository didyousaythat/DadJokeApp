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

  getDadJokeSearch(searchTerm: string) {
    //calls the search controller endpoint and uses url parameters to pass the search term.
    this.http.get<any>('/dadjoke/search', { params: { searchTerm } })
      .pipe(map(apiResult => ({
        //maps the api result to the interface values
        jokeList: apiResult.results,
        searchTerm: apiResult.search_term
      }))
      ).subscribe(
        (result: DadJokeList) => {
          this.multipleJokes.jokeList = result.jokeList;
          this.multipleJokes.searchTerm = result.searchTerm;

          //need to reset the lists each time method is called so that we don't just keep stacking onto the lists with each new request.
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
    // Create a case-insensitive global regex
    const regex = new RegExp(term, 'gi');
    // Replace matches with highlighted span
    const highlighted = joke.replace(
      regex,
      match => `<span style="background-color: yellow">${match}</span>`
    );
    // Mark the HTML as safe for Angular
    return this.sanitizer.bypassSecurityTrustHtml(highlighted);
  }

  title = 'dadjokeapp.client';
}
