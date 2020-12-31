import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html'
})
export class BuyComponent implements OnInit {
  soapId: number = 0;
  soap: Soap;
  baseUrl: string;
  http: HttpClient;
  imageBase: string;
  images: any[] = [];

  constructor(private route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    this.route.params.subscribe(result => {
      this.soapId = Number(result.id);
    });

    this.http.get<Soap>(this.baseUrl + 'soaps/' + this.soapId.toString()).subscribe(result => {
      this.soap = result;
      const images = [];
      this.soap.images.forEach(function (image) {;
        images.push({ source: environment.baseImage + image.name, alt: 'Description for ' + image.name, title: image.name, height: '500', width: "500" });
      });

      this.images = images;
    }, error => console.error(error));

  }
}
