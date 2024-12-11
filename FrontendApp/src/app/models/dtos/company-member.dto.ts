import { CompanyRole } from "../../enums/company-role.enum";

export interface CompanyMemberDTO {
  id: number;
  username: string | null;
  email: string | null;
  joinDate: Date;
  companyRole: CompanyRole;
  salary: number;
  workHours: number;
  workDays: number;
}
