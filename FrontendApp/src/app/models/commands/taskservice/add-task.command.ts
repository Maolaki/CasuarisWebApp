export interface AddTaskCommand {
  username: string | null;
  companyId: number | null;
  parentId: number | null;
  name: string | null;
  description: string | null;
  budget: number | null;
}
