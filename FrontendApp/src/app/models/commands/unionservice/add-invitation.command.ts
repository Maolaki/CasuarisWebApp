import { CompanyRole } from "../../../enums/company-role.enum";
import { InvitationType } from "../../../enums/invitation-type.enum";

export interface AddInvitationCommand {
  username: string | null;
  description: string | null;
  userId: number | null;
  companyId: number | null;
  role: CompanyRole | null;
  teamId: number | null;
  type: InvitationType | null;
}
