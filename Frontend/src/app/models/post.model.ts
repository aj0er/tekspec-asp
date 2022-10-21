export interface Post {
    id: string,
    content: string,
    user: User
}

export interface User {
    id: string,
    displayName: string
}