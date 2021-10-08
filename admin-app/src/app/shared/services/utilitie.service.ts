import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { TreeMolde as TreeModel } from '../models';

/* @internal 
* Chứa cá phương thức xử lý mảng, chuỗi
*/
@Injectable({ providedIn: 'root' })
export class UtilitiesService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }
  /** 
   * @internal Menu lấy từ api là 1 mảng phẳng
   * tức là các menu con nó sẽ thành các bản ghi khác nhau
   * chuyển về các menu con là các mảng
   * 
  */
  UnflatteringForLeftMenu = (arr: any[]): any[] => {
    const map = {};
    const roots: any[] = [];
    for (let i = 0; i < arr.length; i++) {
      const node = arr[i];
      node.children = [];
      map[node.id] = i; // use map to look-up the parents
      if (node.parentId !== null) {
        delete node['children'];
        arr[map[node.parentId]].children.push(node);
      } else {
        roots.push(node);
      }
    }
    return roots;
  }

  UnflatteringForTree = (roots: any[]): TreeModel[] => {
    // console.log('roots truyen vao');
    // console.log(roots);
    // map là đối tượng kiểu {[key: string]: number} number là index trong roots
    // dùng để đánh dấu vị trí các phần tử trong roots
    const map: { [key: string]: number } = {};
    const result: TreeModel[] = [];
    let node: TreeModel = new TreeModel();
    for (let i = 0; i < roots.length; i += 1) {
      map[roots[i].id] = i;
      roots[i].data = roots[i]; // thêm trường data
      roots[i].children = []; // thêm trường children
    }
    for (let i = 0; i < roots.length; i += 1) {
      node = {
        id: roots[i].id,
        parentId: roots[i].parentId,
        data: roots[i].data,
        expand: false,
        level: 0,
        children: roots[i].children
      };
      if (node.parentId !== null && roots[map[node.parentId]] != undefined) {
        // nếu phần tử thứ i trong roots có parentId 
        // và phần tử có id  = parentId tồn tại
        // thì đẩy phần tử node vào phần tử có id  = parentId trong roots
        // console.log('node có cha');
        // console.log(node.id);
        roots[map[node.parentId]].children.push(node);
      } else {
        // Đẩy node mồ côi vào result
        result.push(node);
      }
    }
    return result;
  }
  convertTreeToList(root: TreeModel): TreeModel[] {
    // console.log('convertTreeToList');
    // console.log(root);
    const stack: TreeModel[] = [root];
    const result: TreeModel[] = [];
    const hashMap = {};

    while (stack.length !== 0) {
      // console.log('stack');
      // console.log(stack);
      // console.log(stack.length);
      const node = stack.pop(); // phần tử cuối của stack
      // console.log('node stack');
      // console.log(node);
      if (!hashMap[node.id]) {
        hashMap[node.id] = true;
        result.push(node);
      }
      // console.log(hashMap);
      if (node.children) {
        for (let i = node.children.length - 1; i >= 0; i--) {
          // console.log(`node.level: ${node.level}`);
          stack.push({ ...node.children[i], level: node.level! + 1, expand: false, parent: node });
        }
      }
    }
    // console.log('result');
    // console.log(result);
    return result;
  }
  MakeSeoTitle(input: string) {
    if (input == undefined || input == '') {
      return '';
    }
    // Đổi chữ hoa thành chữ thường
    let slug = input.toLowerCase();

    // Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    // Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    // Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, '-');
    // Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    // Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    // Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');

    return slug;
  }

  ToFormData(formValue: any) {
    const formData = new FormData();
    for (const key of Object.keys(formValue)) {
      const value = formValue[key];
      formData.append(key, value);
    }

    return formData;
  }
}