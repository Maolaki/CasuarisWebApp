export interface AddTeamMemberCommand {
  username: string | null;
  companyId: number | null;
  teamId: number | null;
  userId: number | null;
}
