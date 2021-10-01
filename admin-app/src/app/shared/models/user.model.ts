export class UserModel {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    dob: string;
}
export class UserRoleModel {
    roles: string[];
    roleNotHas: string[];
}