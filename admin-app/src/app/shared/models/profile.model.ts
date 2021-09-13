export class ProfileModel {
    userId: string;
    userName: string;
    email: string;
    role: string;
    permissions: string;

    constructor(
        private _sub: string,
        private _name: string,
        private _email: string,
        private _role: string,
        private _Permissions: string,
    ) {
        this.userId = _sub;
        this.userName = _name;
        this.email = _email;
        this.role = _role;
        this.permissions = _Permissions;
    }
}