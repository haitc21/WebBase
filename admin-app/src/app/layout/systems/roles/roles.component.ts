import { Component, EventEmitter, OnDestroy, OnInit, QueryList, TemplateRef, ViewChild, ViewChildren } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { Subscription } from 'rxjs';
import { ACTION_TYPE, COL_DATA_TYPE, Dictionary, MessageConstants, NotificationService, PaginationModel, RoleModel, RoleService } from './../../../shared';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})

export class RolesComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();
  public loading = false;
  public pageIndex = 1;
  public pageSize = 10;
  public totalRecords: number;
  public keyword = '';
  COL_DATA_TYPE = COL_DATA_TYPE;
  ACTION_TYPE = ACTION_TYPE;
  // Role
  public roles: Dictionary<any>[] = [];

  // Modal
  tplModalButtonLoading = false;
  disabled = false;
  onCancelmodal = new EventEmitter();

  // Form
  form!: FormGroup;
  errorMsg = '';
  get name() {
    return this.form.get('name');
  }
  get description() {
    return this.form.get('description');
  }
  // Validate
  validation_messages = {
    'name': [
      { type: 'required', message: 'Tên nhóm quyền không được để trống' },
      { type: 'minlength', message: 'Tên nhóm quyền có ít nhất 5 ký tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 25 kí tự' }
    ],
    'description': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'minlength', message: 'Mô tả có ít nhất 5 ký tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 50 kí tự' }
    ]
  };

  @ViewChild('editTitle') editTitleTpl!: TemplateRef<any>;
  @ViewChild('modalContent') modalContentTpl!: TemplateRef<any>;
  @ViewChild('editFooter') editFooterTpl!: TemplateRef<any>;
  @ViewChild('closeIcon') closeIconTpl!: TemplateRef<any>;

  constructor(private rolesService: RoleService,
    private notificationService: NotificationService,
    private modal: NzModalService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadData();
    this.form = this.fb.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      description: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]],
    });
    
  }

  loadData() {
    this.loading = true;
    this.subscription.add(this.rolesService.getAllPaging(this.keyword, this.pageIndex, this.pageSize)
      .subscribe((response: PaginationModel<RoleModel>) => {
        this.processLoadData(response);
        this.loading = false;
      }, error => {
        this.loading = false;
        this.notificationService.showError(error);
      }));
  }
  private processLoadData(response: PaginationModel<RoleModel>) {
    this.roles = response.items;
    this.pageIndex = this.pageIndex;
    this.pageSize = this.pageSize;
    this.totalRecords = response.totalRecords;
  }

  changePage(value: any) {
    this.pageIndex = value;
    this.loadData();
  }
  changeSize(value) {
    this.pageSize = value;
    this.loadData();
  }
  delete(entity: RoleModel) {
    console.log(entity);
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteConfirm(entity.id));
  }
  deleteConfirm(id: string) {
    this.loading = true;
    this.subscription.add(this.rolesService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
    }, error => {
      this.notificationService.showError(error);
    }));
  }
  update(entity: RoleModel) {
    this.form.patchValue({
      id: entity.id,
      name: entity.name,
      description: entity.description
    });
    this.createTplModal(this.editTitleTpl, this.modalContentTpl, this.editFooterTpl, entity);
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
      nzOnCancel: this.onCancelmodal
    });
  }
  saveChange(modelRef: NzModalRef, action: number): void {
    this.tplModalButtonLoading = true;
    console.log(this.form.value);
    if (action === this.ACTION_TYPE.UPDATE) {
      console.log('UPDATE');
      this.subscription.add(
        this.rolesService.update(this.form.value.id, this.form.value)
          .subscribe(() => {
            this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
            this.tplModalButtonLoading = false;
            modelRef.destroy();
            this.loadData();
          }, error => {
            this.errorMsg = error.replace('<br/>','');
            this.tplModalButtonLoading = false;
            setTimeout(() => { this.errorMsg = null; }, 3000);
          })
      );
    }
  else  if (action === this.ACTION_TYPE.CREATE) {
      console.log('CREATE');
      this.subscription.add(
        this.rolesService.add(this.form.value)
          .subscribe(() => {
            this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
            this.tplModalButtonLoading = false;
            modelRef.destroy();
            this.loadData();
          }, error => {
            this.errorMsg = error.replace('<br/>','');
            this.tplModalButtonLoading = false;
            setTimeout(() => { this.errorMsg = null; }, 3000);
          })
      );
    }
    this.form.reset();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
