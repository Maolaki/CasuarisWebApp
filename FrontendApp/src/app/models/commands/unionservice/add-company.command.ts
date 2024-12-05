export interface AddCompanyCommand {
  userId: number | null;
  name: string | null;
  description: string | null;
  imageFile: File | null;
}
