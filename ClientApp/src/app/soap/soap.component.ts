import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';
import { Message } from 'primeng/components/common/api';


@Component({
  selector: 'app-soap',
  templateUrl: './soap.component.html',
  styles: [`
        :host ::ng-deep button {
            margin-right: .25em;
        }

        :host ::ng-deep .custom-toast .ui-toast-message {
            color: #ffffff;
            background: #FC466B;
            background: -webkit-linear-gradient(to right, #3F5EFB, #FC466B);
            background: linear-gradient(to right, #3F5EFB, #FC466B);
        }

        :host ::ng-deep .custom-toast .ui-toast-close-icon {
            color: #ffffff;
        }
    `],
  providers: [MessageService]
})
export class SoapComponent implements OnInit {
  public soaps: Soap[]
  public soapTypes: SoapType[]
  @Input() soap: Soap;
  display: boolean = false;
  baseUrl: string;
  http: HttpClient;
  form: FormGroup;
  msgs: Message[] = [];
  uploadedFiles: any[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private fb: FormBuilder, private messageService: MessageService) {
    this.baseUrl = baseUrl;
    this.http = http

    http.get<Soap[]>(baseUrl + 'soaps').subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required]
    });
  }

  onUpload(event) {
    for (let file of event.files) {
      this.uploadedFiles.push(file);
    }

    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }

  sendPostRequest() {
    if (this.form.valid) {
      const soap = this.form.getRawValue();
      return this.http.post<Soap>(this.baseUrl + 'soaps', soap).subscribe(result => {
        this.soaps.push(result);
        this.closeDialog();
        this.form.reset();
        this.showSuccess();
      }, error => console.error(error));
    }
  }

  showSuccess() {
    this.msgs = [];
    this.msgs.push({ severity: 'success', summary: 'Success Message', detail: 'Order submitted' });
  }

  showDialog() {
    this.http.get<SoapType[]>(this.baseUrl + 'soaptypes').subscribe(result => {
      this.soapTypes = result;
      console.log(this.soapTypes);
      this.display = true;
    }, error => console.error(error));
  }

  closeDialog() {
    this.display = false;
  }

}
