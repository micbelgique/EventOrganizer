export interface Picture {
  idFromPlat: number;
  pictureUrl: string;
  text: string;
  createdAt: Date;
  removed: boolean;
  user: UserPicture;
}
export interface UserPicture {
  idFromPlat: number;
  username: string;
  name: string;
  userProfilePictureUrl: string;
}
