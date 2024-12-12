import { CompanyRole } from "../../../enums/company-role.enum";

export interface AddCompanyWorkerCommand {
  username: string | null;
  companyId: number | null;
  memberUsername: string | null;
  role: CompanyRole | null;
  salary: number | null;
  workHours: number | null;
  workDays: number | null;
}
