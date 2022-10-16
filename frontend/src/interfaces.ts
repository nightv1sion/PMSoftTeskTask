export interface Book {
    id: string;
    name: string;
    genre: string;
    author: string;
    year: number;
}

export interface BookForManipulateDto  {
    name: string;
    genre: string;
    author: string;
    year: number;
}

export interface BookForUpdateDto extends BookForManipulateDto{

}

export interface BookForCreateDto extends BookForManipulateDto {

}
export interface UserForLoginDto {
    userName: string;
    password: string;
}

export interface UserForRegisterDto {
    userName: string;
    password: string;
    confirmPassword: string;
}

export interface User {
    userName: string;
    role: string;
}