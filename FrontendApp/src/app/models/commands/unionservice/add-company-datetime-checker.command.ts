import { DateTimeCheckerType } from "../../../enums/datetime-checker-type.enum";

export interface AddCompanyDateTimeCheckerCommand {
  username: string | null;
  companyId: number | null;
  type: DateTimeCheckerType | null;
  address: string | null;
  model: string | null;
}
