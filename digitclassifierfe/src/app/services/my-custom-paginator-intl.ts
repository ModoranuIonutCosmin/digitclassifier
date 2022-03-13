import {MatPaginatorIntl} from "@angular/material/paginator";

export class MyCustomPaginatorIntl extends MatPaginatorIntl {
  constructor() {
    super();
    // copy super.getRangeLabel reference before overriding it
    const superGetRangeLabel = this.getRangeLabel;
    this.getRangeLabel = (page: number, pageSize: number, length: number) => {
      return `${superGetRangeLabel(page, pageSize, length)} total`;
    };
  }
}
