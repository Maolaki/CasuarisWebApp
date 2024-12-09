import { TaskStatus } from "../../enums/task-status.enum";
import { UserDTO } from "./user.dto";

export interface TaskInfoDTO {
  id: number;
  companyId: number | null;
  companyName: string | null;
  parentId: number | null;
  name: string | null;
  description: string | null;
  budget: number | null;
  status: TaskStatus | null;
  completeDate: string | null;
  members: UserDTO[] | null;
}
