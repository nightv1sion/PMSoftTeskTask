export interface Book {
    id: string;
    name: string;
    genre: string;
    author: string;
    year: number;
}

export interface UserForLoginDto {
    userName: string;
    password: string;
}