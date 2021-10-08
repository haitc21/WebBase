export class TreeMolde {
  id: string;
  parentId: string;
  data: any;
  expand?: boolean;
  level?: number;
  children?: TreeMolde[];
  parent?: TreeMolde;
  constructor() {
    this.id = '';
    this.parentId = '';
    this.data = {};
    this.expand = false;
    this.level = 0;
    this.children = [];
  }
}