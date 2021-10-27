import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { strict } from 'assert';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  baseUrl: string;
  imageBase: string;
  urlImage: string;
  public soaps: Soap[];
  selectedSoap: Soap;
  displayDialog: boolean;
  images: any[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.baseUrl = baseUrl;
    this.imageBase = environment.baseImage;

    http.get<Soap[]>(baseUrl + 'soaps').subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

  selectSoap(event: Event, soap: Soap) {
    this.images = [];
    soap.images.forEach((soap, index) => {
      this.urlImage = this.imageBase + soap.name;
      this.images.push({ source: this.urlImage, alt: 'Imagen', title: 'Title 1', height: '350', width: '300' });
    });
    console.log(this.images);
    this.selectedSoap = soap;
    this.displayDialog = true;
    event.preventDefault();
  }

  goToSoap(soap: Soap) {
    this.router.navigate(['/buy', soap]);
  }

  onDialogHide() {
    this.selectedSoap = null;
  }
}
