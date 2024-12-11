import { ResourceType } from "../../enums/resource-type.enum";

export interface ResourceDTO {
  id: number;
  data: string | null;
  contentType: string | null;
  imageFile: File | null;
  type: ResourceType;
  imageFileUrl: string | null;
}
