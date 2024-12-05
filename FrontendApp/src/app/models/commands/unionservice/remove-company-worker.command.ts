import { CompanyRole } from "../../../enums/company-role.enum";

export interface RemoveCompanyWorkerCommand {
  username: string | null;
  companyId: number | null;
  userId: number | null;
  role: CompanyRole | null;
}
