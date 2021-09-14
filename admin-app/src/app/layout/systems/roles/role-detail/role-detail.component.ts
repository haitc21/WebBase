import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MessageConstants, NotificationService, PaginationModel, RoleModel, RoleService } from '../../../../shared';

@Component({
  selector: 'app-role-detail',
  templateUrl: './role-detail.component.html',
  styleUrls: ['./role-detail.component.scss']
})
export class RoleDetailComponent implements OnInit, OnDestroy {
  // Subscription để quản lý các subscribe lúc mà unsubscribe (ngOnDestroy)
  private subscription = new Subscription();
  // Default
  // public bsModalRef: BsModalRef; tí đổi moddal của ant
  public blockedPanel = false;
  /**
   * Paging
   */
  public pageIndex = 1;
  public pageSize = 10;
  public pageDisplay = 10;
  public totalRecords: number;
  public keyword = '';
  // Role
  public items: any[];
  public selectedItems = [];
  constructor(private rolesService: RoleService,
    private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(selectedId = null) {
    this.blockedPanel = true;
    this.subscription.add(this.rolesService.getAllPaging(this.keyword, this.pageIndex, this.pageSize)
      .subscribe((response: PaginationModel<RoleModel>) => {
        this.processLoadData(selectedId, response);
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }
  private processLoadData(selectedId = null, response: PaginationModel<RoleModel>) {
    this.items = response.item;
    this.pageIndex = this.pageIndex;
    this.pageSize = this.pageSize;
    this.totalRecords = response.totalRecord;
    if (this.selectedItems.length === 0 && this.items.length > 0) {
      this.selectedItems.push(this.items[0]);
    }
    if (selectedId != null && this.items.length > 0) {
      this.selectedItems = this.items.filter(x => x.Id === selectedId);
    }
  }
  pageChanged(event: any): void {
    this.pageIndex = event.page + 1;
    this.pageSize = event.rows;
    this.loadData();
  }

  showAddModal() {
    // this.bsModalRef = this.modalService.show(RolesDetailComponent,
    //   {
    //     class: 'modal-lg',
    //     backdrop: 'static'
    //   });
    // this.bsModalRef.content.savedEvent.subscribe((response) => {
    //   // this.bsModalRef.hide();
    //   this.loadData();
    //   this.selectedItems = [];
    // });
  }
  showEditModal() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const initialState = {
      entityId: this.selectedItems[0].id
    };
    // this.bsModalRef = this.modalService.show(RolesDetailComponent,
    //   {
    //     initialState: initialState,
    //     class: 'modal-lg',
    //     backdrop: 'static'
    //   });

    // this.subscription.add(this.bsModalRef.content.savedEvent.subscribe((response) => {
    //   // this.bsModalRef.hide();
    //   this.loadData(response.id);
    // }));
  }

  deleteItems() {
    const id = this.selectedItems[0].id;
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemsConfirm(id));
  }
  deleteItemsConfirm(id) {
    this.blockedPanel = true;
    this.subscription.add(this.rolesService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
      this.selectedItems = [];
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
