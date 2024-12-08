import { TaskStatus } from "../../enums/task-status.enum";
import { ResourceDTO } from "./resource.dto";
import { TaskInfoDTO } from "./task-info.dto";
import { UserDTO } from "./user.dto";

export interface TaskDataDTO {
  id: number;
  companyId: number | null;
  name: string | null;
  description: string | null;
  budget: number | null;
  status: TaskStatus | null;
  completeDate: string | null;
  resources: ResourceDTO[] | null;
  childTasks: TaskInfoDTO[] | null;
  members: UserDTO[] | null;
}
