import { Token } from "./Token";

export interface UserModel{
    id: string;

    age: number | null;
    userName: string;
    email: string;   
    
    avatar:string | null;
    fullName: string;  
    birthDay: Date | null;  
    bio:string | null;

    registeredAt:Date;
    roles:string[];
    postsIds:string[];
    token: Token;
}