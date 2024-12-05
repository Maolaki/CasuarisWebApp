import { ResourceType } from "../../enums/resource-type.enum";

export interface ResourceDTO {
  id: number;
  name: string | null;
  imageFile: File | null;
  resourceType: ResourceType | null;
}
