import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';
import { ConfirmationService } from 'primeng/api';
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
  formAdmin: FormGroup;
  msgs: Message[] = [];
  dialogmsgs: Message[] = [];
  uploadedFiles: Image[] = [];
  soapType: string;
  dropDown: DropDownList;
  cols: any[];
  soapDetails: SoapDetail[] = [];
  soapDetailName: string;
  imageDialog: boolean = false;
  edit: boolean = false;
  isLogin: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private fb: FormBuilder, private messageService: MessageService,
              private confirmationService: ConfirmationService) {
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
      quantity: [0, Validators.required],
      soapTypeId: [0, Validators.required],
      soapDetails: [0, Validators.required],
      images: [0, Validators.required]
    });

    this.formAdmin = this.fb.group({
      user: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.formAdmin.valid) {
      const userAdmin = this.formAdmin.getRawValue();

      return this.http.post<UserAdmin>(this.baseUrl + 'login', userAdmin).subscribe(result => {
        this.isLogin = result.isLogin;

        this.msgs = [];
        if (this.isLogin) {
          this.msgs.push({ severity: 'success', summary: 'Login success.', detail: result.message });
        } else {
          this.msgs.push({ severity: 'info', summary: 'Login was not successed.', detail: result.message });
        }
        
      }, error => console.error(error));
    }
  }

  onUpload(event) {
    for (let file of event.files) {
      this.uploadedFiles.push({
        id: 0,
        name: file.name
      });
    }

    this.dialogmsgs = [];
    this.dialogmsgs.push({ severity: 'info', summary: 'Images were uploaded.', detail: 'Images were uploaded.' });
  }

  myUploader(event) {

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
        this.showSuccess('The soap was added: ' + result.name, 'Soap added.');
      }, error => console.error(error));
    }
  }

  sendPutRequest() {
    this.form.controls['soapTypeId'].setValue(this.dropDown.code);
    this.form.controls['soapDetails'].setValue(this.soapDetails);
    this.form.controls['images'].setValue(this.uploadedFiles);
    const soap = this.form.getRawValue();

    return this.http.put<Soap[]>(this.baseUrl + 'soaps/' + soap.id, soap).subscribe(result => {
      this.soaps = result;
      this.uploadedFiles = [];
      this.edit = false;
      this.showSuccess('The Soap was updated: ' + soap.name, 'Soap uptaded.');
    }, error => console.error(error));
  }

  addIngredient() {
    if (this.soapDetailName === '' || this.soapDetailName === undefined) {
      this.showError('You must to add some ingredient', 'Minimum one ingredient.');
    } else {
      this.soapDetails.push({
        id: 0,
        name: this.soapDetailName
      });

      this.soapDetailName = '';
    }
  }

  removeIngredient(ingredientToRemove: SoapDetail) {
    const index = this.soapDetails.findIndex(ingredient => ingredient.name === ingredientToRemove.name);
    console.log(index);
    this.soapDetails.splice(index, 1); 
  }

  showRemoveSuccess(soapName: string) {
    this.msgs = [];
    this.msgs.push({ severity: 'info', summary: 'The soap: ' + soapName + ' has been deleted', detail: 'Soap deleted' });
  }

  showSuccess(message: string, messageDetail: string) {
    this.msgs = [];
    this.msgs.push({ severity: 'success', summary: message, detail: messageDetail });
  }

  showError(msg: string, messageDetail: string) {
    this.msgs = [];
    this.msgs.push({ severity: 'error', summary: msg, detail: messageDetail });
  }

  showDialog() {
    this.form.reset();
    
    this.http.get<DropDownList[]>(this.baseUrl + 'soaptypes').subscribe(result => {
      this.soapTypes = result;
      this.display = true;
    }, error => console.error(error));
  }

  removeSoap(soapId: number) {
    this.confirmationService.confirm({
      message: 'Are you sure to delete this soap?',
      header: 'Confirmation',
      accept: () => {
        return this.http.delete<Soap>(this.baseUrl + 'soaps/' + soapId).subscribe(result => {
          this.soaps.forEach((value, index) => {
            if (value.id == result.id) this.soaps.splice(index, 1);
          });

          this.showRemoveSuccess(result.name);
        }, error => console.error(error));
      }
    });
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
        available: soap.available,
        quantity: soap.quantity
      });

      this.dropDown = result.find(e => e.code === soap.soapType.id.toString())
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
