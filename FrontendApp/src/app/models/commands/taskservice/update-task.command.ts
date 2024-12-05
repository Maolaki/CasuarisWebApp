import { TaskStatus } from "../../../enums/task-status.enum";

export interface UpdateTaskCommand {
  username: string | null;
  companyId: number | null;
  taskId: number | null;
  name: string | null;
  description: string | null;
  budget: number | null;
  status: TaskStatus | null;
}
