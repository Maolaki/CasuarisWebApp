import { ResourceType } from "../../../enums/resource-type.enum";

export interface UpdateResourceCommand {
  username: string | null;
  companyId: number | null;
  resourceId: number | null;
  data: string | null;
  imageFile: File | null;
  type: ResourceType | null;
}
