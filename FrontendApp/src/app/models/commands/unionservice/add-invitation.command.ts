import { CompanyRole } from "../../../enums/company-role.enum";
import { InvitationType } from "../../../enums/invitation-type.enum";

export interface AddInvitationCommand {
  username: string | null;
  companyId: number | null;
  memberUsername: string | null;
  description: string | null;
  role: CompanyRole | null;
  teamId: number | null;
  type: InvitationType | null;
}
