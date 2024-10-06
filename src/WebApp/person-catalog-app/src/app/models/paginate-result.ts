
export interface PaginateResult<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
}
