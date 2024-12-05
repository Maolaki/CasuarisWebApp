export interface GetCompanyStatisticsQuery {
  username: string | null;
  companyId: number | null;
  startDate: Date | null;
  endDate: Date | null;
}
