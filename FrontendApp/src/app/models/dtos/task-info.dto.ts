import { TaskStatus } from "../../enums/task-status.enum";

export interface TaskInfoDTO {
  id: number;
  companyId: number | null;
  name: string | null;
  description: string | null;
  budget: number | null;
  status: TaskStatus | null;
  completeDate: string | null; 
}
