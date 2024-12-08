import { ResourceType } from "../../enums/resource-type.enum";

export interface ResourceDTO {
  id: number;
  data: string | null;
  imageFile: File | null;
  resourceType: ResourceType | null;
}
