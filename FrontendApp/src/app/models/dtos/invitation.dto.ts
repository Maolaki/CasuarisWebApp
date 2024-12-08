import { CompanyRole } from "../../enums/company-role.enum";
import { InvitationType } from "../../enums/invitation-type.enum";

export interface InvitationDTO {
  id: number;
  description: string | null;
  companyId: number | null;
  companyRole: CompanyRole | null;
  teamId: number | null;
  type: InvitationType | null;
}
