import { Token } from "./Token";

export interface UserModel{
    id: string;
    age: number | null;
    username: string;
    email: string;
    accessToken: Token;
    avatar:string | null;
    fullName: string;
    registerAt:Date;
    birthDate: Date | null;
    postsIds:string[];
    bio:string | null;
}