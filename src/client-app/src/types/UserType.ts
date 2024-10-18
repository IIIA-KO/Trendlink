export interface UserType {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    instagramAccountUsername: string | null;
    profilePhotoId: string;
    profilePhotoUri: string;
    birthDate: string;
    countryName: string;
    stateName: string;
    phoneNumber: string;
    bio: string;
    accountCategory: string;
    followersCount: number;
    mediaCount: number;
}