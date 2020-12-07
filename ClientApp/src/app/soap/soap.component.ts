import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-soap',
  templateUrl: './soap.component.html'
})
export class SoapComponent {
  public soaps: Soap[]
  public soap: Soap = {
    id: 0,
    name: 'Test'
  };
  display: boolean = false;
  baseUrl: string;
  http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http

    http.get<Soap[]>(baseUrl + 'soaps').subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

  sendPostRequest() {
    alert('ok');
    return this.http.post<Soap[]>(this.baseUrl + 'soaps', this.soap).subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

    showDialog() {
      this.display = true;
    }

    public create() {
      alert('okis');
    }

}
