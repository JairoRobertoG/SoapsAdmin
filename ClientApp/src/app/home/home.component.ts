import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  baseUrl: string;
  imageBase: string;
  public soaps: Soap[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.imageBase = environment.baseImage;

    http.get<Soap[]>(baseUrl + 'soaps').subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

}
