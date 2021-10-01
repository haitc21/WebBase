import { UserService, UserModel, COL_DATA_TYPE, ACTION_TYPE, Dictionary, NotificationService, PaginationModel, MessageConstants } from './../../../shared';
import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { TransferItem } from 'ng-zorro-antd/transfer';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();
  public loading = false;
  public pageIndex = 1;
  public pageSize = 10;
  public totalRecords: number;
  public keyword = '';
  COL_DATA_TYPE = COL_DATA_TYPE;
  ACTION_TYPE = ACTION_TYPE;
  // User
  public users: Dictionary<any>[] = [];

  // Modal
  tplModalButtonLoading = false;

  //Transfer riley
  listRole: TransferItem[] = [];
  userRole: string[] = [];
  userIdAddRole = '';
  transferStyles = {
    "width": "100%",
    "height": "400px",
    "border": "1px solid",
    'background-color': '#acaced'
  }

  // Form
  createForm!: FormGroup;
  editForm!: FormGroup;
  errorMsg = '';
  get userName() {
    return this.createForm.get('userName');
  }
  get password() {
    return this.createForm.get('password');
  }
  get email() {
    return this.createForm.get('email');
  }
  get phoneNumber() {
    return this.createForm.get('phoneNumber');
  }
  get firstName() {
    return this.createForm.get('firstName');
  }
  get lastName() {
    return this.createForm.get('lastName');
  }
  get dob() {
    return this.createForm.get('dob');
  }


  get emailEdit() {
    return this.editForm.get('email');
  }
  get phoneNumberEdit() {
    return this.editForm.get('phoneNumber');
  }
  get firstNameEdit() {
    return this.editForm.get('firstName');
  }
  get lastNamEdit() {
    return this.createForm.get('lastName');
  }
  get dobEdit() {
    return this.editForm.get('dob');
  }

  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validation_messages = {
    'firstName': [
      { type: 'required', message: 'Bạn phải nhập họ người dùng' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' }
    ],
    'lastName': [
      { type: 'required', message: 'Bạn phải nhập tên người dùng' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' }
    ],
    'userName': [
      { type: 'required', message: 'Bạn phải nhập tên tài khoản' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' }
    ],
    'password': [
      { type: 'required', message: 'Bạn phải nhập tên tài khoản' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 6 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
      { type: 'pattern', message: 'Mật khẩu không đủ độ phức tạp' }
    ],
    'email': [
      { type: 'required', message: 'Bạn phải nhập email' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
      { type: 'pattern', message: 'Bạn phải nhập đúng định dạng Email' }
    ]
  };

  @ViewChild('closeIcon') closeIconTpl!: TemplateRef<any>;
  @ViewChild('editTitle') editTitleTpl!: TemplateRef<any>;
  @ViewChild('editContent') editContentTpl!: TemplateRef<any>;
  @ViewChild('editFooter') editFooterTpl!: TemplateRef<any>;
  @ViewChild('detailTitle') detailTitleTpl!: TemplateRef<any>;
  @ViewChild('detailContent') detailContentTpl!: TemplateRef<any>;
  @ViewChild('detailFooter') detailFooterTpl!: TemplateRef<any>;

  @ViewChild('addRoleTitle') addRoleTitleTpl!: TemplateRef<any>;
  @ViewChild('addRoleContent') addRoleContentTpl!: TemplateRef<any>;
  @ViewChild('addRoleFooter') addRoleFooterTpl!: TemplateRef<any>;

  constructor(private userService: UserService,
    private notificationService: NotificationService,
    private modal: NzModalService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadData();
    this.createForm = this.fb.group({
      'id': new FormControl(''),
      'firstName': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255)
      ])),
      'lastName': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255)
      ])),
      'userName': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255),
        Validators.minLength(3)
      ])),
      'password': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255),
        Validators.minLength(8),
        Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$')
      ])),
      confirmPassword: [''],
      'email': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255),
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')
      ])),
      'phoneNumber': new FormControl(),
      'dob': new FormControl()
    }, { validator: this.PasswordValidator });
    this.editForm = this.fb.group({
      'id': new FormControl(''),
      'firstName': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255)
      ])),
      'lastName': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255)
      ])),
      'email': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255),
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')
      ])),
      'phoneNumber': new FormControl(),
      'dob': new FormControl()
    });
  }


  change(ret: {}): void {
    let lst = this.listRole.filter(x => x.direction == 'right');
    this.userRole = [];
    lst.forEach(x => {
      this.userRole.push(x.title);
    });
  }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  filterOption(inputValue: string, item: any): boolean {
    return item.key.indexOf(inputValue) > -1;
  }

  private PasswordValidator(control: AbstractControl): { [key: string]: boolean } | null {
    let password = control.get('password');
    let confirmPassword = control.get('confirmPassword');
    if (password?.pristine || confirmPassword?.pristine) {
      return null;
    }
    let check = (password && confirmPassword && password?.value != confirmPassword?.value)
    return check ? { 'misMatch': true } : null;
  }

  loadData() {
    this.loading = true;
    this.subscription.add(this.userService.getAllPaging(this.keyword, this.pageIndex, this.pageSize)
      .subscribe((response: PaginationModel<UserModel>) => {
        this.users = response.items;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      }, error => {
        this.loading = false;
        this.notificationService.showError(error);
      }));
  }

  search() {
    this.pageIndex = 1;
    this.loadData();
  }

  changePage(value: any) {
    this.pageIndex = value;
    this.loadData();
  }
  changeSize(value) {
    this.pageSize = value;
    this.loadData();
  }
  delete(entity: UserModel) {
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteConfirm(entity.id));
  }
  deleteConfirm(id: string) {
    this.loading = true;
    this.subscription.add(this.userService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
    }, error => {
      this.notificationService.showError(error);
      this.loadData();
    }));
  }
  update(entity: UserModel) {
    this.editForm.patchValue({
      id: entity.id,
      firstName: entity.firstName,
      lastName: entity.lastName,
      email: entity.email,
      phoneNumber: entity.phoneNumber,
      dob: entity.dob
    });
    this.createTplModal(this.editTitleTpl, this.editContentTpl, this.editFooterTpl, entity);
  }
  details(entity: UserModel) {
    this.createTplModal(this.detailTitleTpl, this.detailContentTpl, this.detailFooterTpl, entity);
  }
  async addRole(entity: UserModel) {
    this.errorMsg = '';
    this.userIdAddRole = entity.id;
    let userRoles = await this.userService.getUserRoles(entity.id)
      .toPromise()
      .catch(error => {
        this.notificationService.showError(error);
      });
    let rolesUserNotHas = await this.userService.getRolesUserNotHas(entity.id)
      .toPromise()
      .catch(error => {
        this.notificationService.showError(error);
      });
    if (this.errorMsg === '') {
      userRoles = userRoles as string[];
      rolesUserNotHas = rolesUserNotHas as string[];
      userRoles.forEach(role => {
        this.listRole.push({
          key: role,
          title: role,
          description: role,
          direction: 'right'
        });
      });
      rolesUserNotHas.forEach(role => {
        this.listRole.push({
          key: role,
          title: role,
          description: role,
          direction: 'left'
        });
      });
    }
    this.createTplModal(this.addRoleTitleTpl, this.addRoleContentTpl, this.addRoleFooterTpl, entity);
  }
  restValue() {
    this.createForm.reset();
    this.editForm.reset();
    this.errorMsg = '';
    this.userRole = [];
    this.userIdAddRole = '';
    this.listRole = [];
  }
  createTplModal(tplTitle: TemplateRef<{}>, tplContent: TemplateRef<{}>, tplFooter: TemplateRef<{}>, entity: any | null): void {
    this.modal.create({
      nzTitle: tplTitle,
      nzContent: tplContent,
      nzFooter: tplFooter,
      nzClosable: true,
      nzComponentParams: {
        value: entity
      },
      nzWidth: 700,
      nzCloseIcon: this.closeIconTpl,
      nzOnOk: () => {
        this.restValue();
      },
      nzOnCancel: () => {
        this.restValue();
      }
    });
  }
  saveChange(modelRef: NzModalRef, action: number): void {
    this.tplModalButtonLoading = true;
    if (action === this.ACTION_TYPE.UPDATE) {
      this.subscription.add(
        this.userService.update(this.editForm.value.id, this.editForm.value)
          .subscribe(() => {
            this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
            this.restValue();
            this.tplModalButtonLoading = false;
            modelRef.destroy();
            this.loadData();
          }, error => {
            this.errorMsg = error.replace('<br/>', '');
            this.tplModalButtonLoading = false;
            setTimeout(() => { this.errorMsg = null; }, 5000);
          })
      );
    }
    else if (action === this.ACTION_TYPE.CREATE) {
      this.subscription.add(
        this.userService.add(this.createForm.value)
          .subscribe(() => {
            this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
            this.restValue();
            this.tplModalButtonLoading = false;
            modelRef.destroy();
            this.loadData();
          }, error => {
            this.errorMsg = error.replace('<br/>', '');
            this.tplModalButtonLoading = false;
            setTimeout(() => { this.errorMsg = null; }, 5000);
          })
      );
    }
    else if (action === this.ACTION_TYPE.ADDROLE) {
      let assignRolesToUser = {
        roleNames: this.userRole
      }
      this.subscription.add(
        this.userService.assignRolesToUser(this.userIdAddRole, assignRolesToUser)
          .subscribe(() => {
            this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
            this.restValue();
            this.tplModalButtonLoading = false;
            modelRef.destroy();
            this.loadData();
          }, error => {
            this.errorMsg = error.replace('<br/>', '');
            this.tplModalButtonLoading = false;
            setTimeout(() => { this.errorMsg = null; }, 5000);
          })
      );
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
