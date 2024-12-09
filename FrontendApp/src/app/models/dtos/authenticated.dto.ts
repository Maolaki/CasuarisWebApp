export interface AuthenticatedDTO {
  username: string;
  accessToken: string | null;
  refreshToken: string | null;
}
