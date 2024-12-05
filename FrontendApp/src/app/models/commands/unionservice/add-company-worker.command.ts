import { CompanyRole } from "../../../enums/company-role.enum";

export interface AddCompanyWorkerCommand {
  username: string | null;
  userId: number | null;
  companyId: number | null;
  role: CompanyRole | null;
  salary: number | null;
  workHours: number | null;
  workDays: number | null;
}
