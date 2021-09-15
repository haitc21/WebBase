import { Component, OnDestroy, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { Subscription } from 'rxjs';
import { MessageConstants, NotificationService, PaginationModel, RoleModel, RoleService } from './../../../shared';
import { RoleDetailComponent } from './role-detail/role-detail.component';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})

export class RolesComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();
  // Default
  public blockedLayout = false;
  /**
   * Paging
   */
  public pageIndex = 1;
  public pageSize = 10;
  public totalPages = 10;
  public totalRecords: number;
  public keyword = '';
  // Role
  public items: any[];

  // Modal
  tplModalButtonLoading = false;
  disabled = false;

  constructor(private rolesService: RoleService,
    private notificationService: NotificationService,
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(selectedId = null) {
    this.blockedLayout = true;
    this.subscription.add(this.rolesService.getAllPaging(this.keyword, this.pageIndex, this.pageSize)
      .subscribe((response: PaginationModel<RoleModel>) => {
        this.processLoadData(selectedId, response);
        setTimeout(() => { this.blockedLayout = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedLayout = false; }, 1000);
      }));
  }
  private processLoadData(selectedId = null, response: PaginationModel<RoleModel>) {
    this.items = response.item;
    this.pageIndex = this.pageIndex;
    this.pageSize = this.pageSize;
    this.totalRecords = response.totalRecord;
    this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
  }

  changePage(value: any) {
    this.pageIndex = value;
    this.loadData();
  }
  changeSize(value) {
    this.pageSize = value;
    this.loadData();
  }
  deleteItem(id: string) {
    console.log(id);
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemsConfirm(id));
  }
  deleteItemsConfirm(id: string) {
    this.blockedLayout = true;
    this.subscription.add(this.rolesService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
      setTimeout(() => { this.blockedLayout = false; }, 1000);
    }, error => {
      this.notificationService.showError(error.message);
      setTimeout(() => { this.blockedLayout = false; }, 1000);
    }));
  }
  editItem(entity: any) {
    console.log(entity);
  }
  createTplModal(tplTitle: TemplateRef<{}>, tplContent: TemplateRef<{}>, tplFooter: TemplateRef<{}>,entity: any | null): void {
    this.modal.create({
      nzTitle: tplTitle,
      nzContent: tplContent,
      nzFooter: tplFooter,
      nzMaskClosable: false,
      nzClosable: false,
      nzComponentParams: {
        value: entity
      },
      nzOnOk: () => console.log('ok')
    });
  }
  destroyTplModal(modelRef: NzModalRef): void {
    this.tplModalButtonLoading = true;
    setTimeout(() => {
      this.tplModalButtonLoading = false;
      modelRef.destroy();
    }, 5000);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
