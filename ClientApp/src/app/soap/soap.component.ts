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
  public soaps: Soap[];
  public soapTypes: DropDownList[];
  @Input() soap: Soap;
  display: boolean = false;
  baseUrl: string;
  http: HttpClient;
  form: FormGroup;
  msgs: Message[] = [];
  uploadedFiles: Image[] = [];
  soapType: string;
  dropDown: DropDownList;
  cols: any[];
  soapDetails: SoapDetail[] = [];
  soapDetailName: string;
  imageDialog: boolean = false;
  edit: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private fb: FormBuilder, private messageService: MessageService) {
    this.baseUrl = baseUrl;
    this.http = http

    http.get<Soap[]>(baseUrl + 'soaps').subscribe(result => {
      this.soaps = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [0],
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0.0, Validators.required],
      available: [false],
      soapTypeId: [0, Validators.required],
      soapDetails: [0, Validators.required],
      images: [0, Validators.required]
    });
  }

  onUpload(event) {
    for (let file of event.files) {
      this.uploadedFiles.push({
        id: 0,
        name: file.name
      });
    }

    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }

  sendPostRequest() {
    this.form.controls['id'].setValue(0);
    this.form.controls['soapTypeId'].setValue(this.dropDown.code);
    this.form.controls['soapDetails'].setValue(this.soapDetails);
    this.form.controls['images'].setValue(this.uploadedFiles);

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

  sendPutRequest() {
    this.form.controls['soapTypeId'].setValue(this.dropDown.code);
    this.form.controls['soapDetails'].setValue(this.soapDetails);
    this.form.controls['images'].setValue(this.uploadedFiles);
    const soap = this.form.getRawValue();

    return this.http.put<Soap>(this.baseUrl + 'soaps/' + soap.id, soap).subscribe(result => {
      //Code
    }, error => console.error(error));
  }

  addIngredient() {
    if (this.soapDetailName === '' || this.soapDetailName === undefined) {
      this.showError('Se debe de escribir un ingrediente');
    } else {
      this.soapDetails.push({
        id: 0,
        name: this.soapDetailName
      });

      this.soapDetailName = '';
    }
  }

  removeIngredient(ingredient: SoapDetail) {
    const index = this.soapDetails.findIndex(ingredient => ingredient.name === ingredient.name);
    this.soapDetails.splice(index, 1); 
  }

  showSuccess() {
    this.msgs = [];
    this.msgs.push({ severity: 'success', summary: 'Success Message', detail: 'Order submitted' });
  }

  showError(msg: string) {
    this.msgs = [];
    this.msgs.push({ severity: 'error', summary: msg, detail: 'Order submitted' });
  }

  showDialog() {
    this.http.get<DropDownList[]>(this.baseUrl + 'soaptypes').subscribe(result => {
      this.soapTypes = result;
      this.display = true;
    }, error => console.error(error));
  }

  showEditDialog(soap: Soap) {
    this.http.get<DropDownList[]>(this.baseUrl + 'soaptypes').subscribe(result => {
      this.soapTypes = result;
      this.soapDetails = soap.soapDetails;

      this.form.patchValue({
        id: soap.id,
        name: soap.name,
        description: soap.description,
        price: soap.price,
        available: soap.available
      });

      this.edit = true;
    }, error => console.error(error));
  }

  addImage(soap: Soap) {
    this.imageDialog = true;
  }

  closeDialog() {
    this.display = false;
  }

}
