export interface ChangeResourcePositionCommand {
  username: string | null;
  companyId: number | null;
  taskInfoId: number | null;
  resourceId: number | null;
  newPosition: number | null;
}
