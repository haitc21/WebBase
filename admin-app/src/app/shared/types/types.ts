import { NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';


// Định nghĩa ra 1 kiểu Dictionary
// Nếu generic là mảng thì dùng ArrayDictionary không thì ObjectDictionary
type ObjectDictionary<T> = { [key: string]: T }; // {foo: 'abc' , bar: "abcd"}
type ArrayDictionary<T> = { [key: string]: T[] }; // {foo: [1,2,3] , bar:['s1','s2']}
export type Dictionary<T> = T extends [] ? ArrayDictionary<T[any]> : ObjectDictionary<T>


// export interface Dictionary {
//   [key: string]: any
// }

export enum COL_DATA_TYPE {
  TEXT,
  NUMBER,
  CURRENCY,
  DATE,
  ACTION,
  COUNT
}

export enum ACTION_TYPE {
  CREATE,
  UPDATE,
  DELETE,
  DETAILS,
  APPROVE
}

export type SortOrder = NzTableSortOrder;
export type SortFunc = NzTableSortFn;
