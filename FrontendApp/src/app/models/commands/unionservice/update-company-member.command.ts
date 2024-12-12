export interface UpdateCompanyMemberCommand {
  username: string | null;
  companyId: number | null;
  memberId: number | null;
  salary: number | null;
  workHours: number | null;
  workDays: number | null;
}
