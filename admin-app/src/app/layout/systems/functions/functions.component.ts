import { map } from 'rxjs/operators';
import {
  FunctionModel,
  COL_DATA_TYPE,
  ACTION_TYPE,
  NotificationService,
  MessageConstants,
  UtilitiesService,
  FunctionService,
  TreeMolde
} from './../../../shared';
import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { TransferItem } from 'ng-zorro-antd/transfer';

@Component({
  selector: 'app-functions',
  templateUrl: './functions.component.html',
  styleUrls: ['./functions.component.scss']
})
export class FunctionsComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public loading = false;
  public functions: FunctionModel[] = [];
  public dataDisplay: TreeMolde[] = [];
  public keyword = '';
  COL_DATA_TYPE = COL_DATA_TYPE;
  ACTION_TYPE = ACTION_TYPE;

  mapOfExpandedData: { [key: string]: TreeMolde[] } = {};

  // Modal
  tplModalButtonLoading = false;

  //Transfer riley
  lstCmd: TransferItem[] = [];
  cmdInFunc: string[] = [];
  funcIdAddCmd = '';
  transferStyles = {
    "width": "100%",
    "height": "400px",
    "border": "1px solid",
    'background-color': '#acaced'
  }

  // Form
  funcForm!: FormGroup;
  errorMsg = '';
  get id() {
    return this.funcForm.get('id');
  }
  get name() {
    return this.funcForm.get('name');
  }
  get parentId() {
    return this.funcForm.get('parentId');
  }
  
  get url() {
    return this.funcForm.get('url');
  }
  
  get icon() {
    return this.funcForm.get('icon');
  }
  
  get sortOrder() {
    return this.funcForm.get('sortOrder');
  }
  // Validate 
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validation_messages = {
    'name': [
      { type: 'required', message: 'Bạn phải nhập tên trang' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' }
    ],
    'id': [
      { type: 'required', message: 'Bạn phải nhập mã duy nhất' }
    ],
    'url': [
      { type: 'required', message: 'Bạn phải nhập đường dẫn' }
    ],
    'sortOrder': [
      { type: 'required', message: 'Bạn phải nhập thứ tự' }
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

  constructor(private _functionService: FunctionService,
    private notificationService: NotificationService,
    private modal: NzModalService,
    private fb: FormBuilder,
    private utilitiesService: UtilitiesService) { }

  ngOnInit(): void {
    this.loadData();
    this.funcForm = this.fb.group({
      'id': new FormControl('', Validators.required),
      'parentId': new FormControl(),
      'name': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(255),
        Validators.minLength(3)
      ])),
      'url': new FormControl('', Validators.required),
      'icon': new FormControl(),
      'sortOrder': new FormControl(1, Validators.required)
    });
  }

  loadData() {
    this.loading = true;
    this.subscription.add(this._functionService.getAll()
      .subscribe((response: FunctionModel[]) => {
        this.functions = response;
        this.dataDisplay = this.utilitiesService.UnflatteringForTree(response);
        // console.log(this.functions);
        this.dataDisplay.forEach(item => {
          this.mapOfExpandedData[item.id] = this.utilitiesService.convertTreeToList(item);
        });
        // console.log(this.mapOfExpandedData);
        this.loading = false;
      }, error => {
        this.loading = false;
        this.notificationService.showError(error);
      }));
  }

  // tree
  collapse(array: TreeMolde[], data: TreeMolde, $event: boolean): void {
    if (!$event) {
      if (data.children) {
        data.children.forEach(d => {
          const target = array.find(a => a.id === d.id)!;
          target.expand = false;
          this.collapse(array, target, false);
        });
      } else {
        return;
      }
    }
  }
  change(ret: {}): void {
    let lst = this.lstCmd.filter(x => x.direction == 'right');
    this.cmdInFunc = [];
    lst.forEach(x => {
      this.cmdInFunc.push(x.title);
    });
  }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  filterOption(inputValue: string, item: any): boolean {
    return item.title.indexOf(inputValue) > -1;
  }

  search() {
    this.keyword = this.keyword.toUpperCase();
    this.mapOfExpandedData = {};
    console.log(this.mapOfExpandedData);
    if (this.keyword === '') {
      this.dataDisplay = this.utilitiesService.UnflatteringForTree(this.functions);;
      this.dataDisplay.forEach(item => {
        this.mapOfExpandedData[item.id] = this.utilitiesService.convertTreeToList(item);
      });
    }
    else {
      let data = this.functions.filter(x =>
        x.id.includes(this.keyword)
        || x.name.toUpperCase().includes(this.keyword));
      this.dataDisplay = this.utilitiesService.UnflatteringForTree(data);
      this.dataDisplay.forEach(item => {
        this.mapOfExpandedData[item.id] = this.utilitiesService.convertTreeToList(item);
      });
    }
  }
  delete(entity: FunctionModel) {
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteConfirm(entity.id));
  }
  deleteConfirm(id: string) {
    this.loading = true;
    this.subscription.add(this._functionService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
    }, error => {
      this.notificationService.showError(error);
      this.loadData();
    }));
  }
  update(entity: FunctionModel) {
    this.funcForm.patchValue({
      id: entity.id,
    });
    this.createTplModal(this.editTitleTpl, this.editContentTpl, this.editFooterTpl, entity);
  }
  details(entity: FunctionModel) {
    this.createTplModal(this.detailTitleTpl, this.detailContentTpl, this.detailFooterTpl, entity);
  }
  // async addRole(entity: FunctionModel) {
  //   this.errorMsg = '';
  //   this.funcIdAddCmd = entity.id;
  //   let userRoles = await this._functionService.getUserRoles(entity.id)
  //     .toPromise()
  //     .catch(error => {
  //       this.notificationService.showError(error);
  //     });
  //   if (this.errorMsg === '') {
  //     userRoles = userRoles as UserRoleModel;
  //     userRoles?.roles.forEach(role => {
  //       this.lstCmd.push({
  //         key: role,
  //         title: role,
  //         description: role,
  //         direction: 'right'
  //       });
  //     });
  //     userRoles?.roleNotHas.forEach(role => {
  //       this.lstCmd.push({
  //         key: role,
  //         title: role,
  //         description: role,
  //         direction: 'left'
  //       });
  //     });
  //   }
  //   this.createTplModal(this.addRoleTitleTpl, this.addRoleContentTpl, this.addRoleFooterTpl, entity);
  // }
  restValue() {
    this.funcForm.reset();
    this.errorMsg = '';
    this.cmdInFunc = [];
    this.funcIdAddCmd = '';
    this.lstCmd = [];
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
    // if (action === this.ACTION_TYPE.UPDATE) {
    //   this.subscription.add(
    //     this._functionService.update(this.funcForm.value.id, this.funcForm.value)
    //       .subscribe(() => {
    //         this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
    //         this.restValue();
    //         this.tplModalButtonLoading = false;
    //         modelRef.destroy();
    //         this.loadData();
    //       }, error => {
    //         this.errorMsg = error.replace('<br/>', '');
    //         this.tplModalButtonLoading = false;
    //         setTimeout(() => { this.errorMsg = null; }, 5000);
    //       })
    //   );
    // }
     if (action === this.ACTION_TYPE.CREATE) {
      this.subscription.add(
        this._functionService.add(this.funcForm.value)
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
    // else if (action === this.ACTION_TYPE.ADDROLE) {
    //   let assignRolesToUser = {
    //     roleNames: this.cmdInFunc
    //   }
    //   this.subscription.add(
    //     this._functionService.assignRolesToUser(this.funcIdAddCmd, assignRolesToUser)
    //       .subscribe(() => {
    //         this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
    //         this.restValue();
    //         this.tplModalButtonLoading = false;
    //         modelRef.destroy();
    //         this.loadData();
    //       }, error => {
    //         this.errorMsg = error.replace('<br/>', '');
    //         this.tplModalButtonLoading = false;
    //         setTimeout(() => { this.errorMsg = null; }, 5000);
    //       })
    //   );
    // }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
