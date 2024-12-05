export interface UpdateCompanyCommand {
  username: string | null;
  companyId: number | null;
  name: string | null;
  description: string | null;
  imageFile: File | null;
}
