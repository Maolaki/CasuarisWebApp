import { ResourceType } from "../../../enums/resource-type.enum";

export interface AddResourceCommand {
  username: string | null;
  companyId: number | null;
  taskInfoId: number | null;
  resourceData: string | null;
  imageFile: File | null;
  type: ResourceType | null;
}
