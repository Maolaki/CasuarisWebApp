export interface RemoveInvitationCommand {
  username: string | null;
  invitationId: number | null;
  answer: boolean | null;
}
