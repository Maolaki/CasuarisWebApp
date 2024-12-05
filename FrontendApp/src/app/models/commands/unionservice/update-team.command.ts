export interface UpdateTeamCommand {
  username: string | null;
  companyId: number | null;
  teamId: number | null;
  name: string | null;
  description: string | null;
}
