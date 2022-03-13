export class RegisterUser {
  UserName: string
  FirstName: string
  LastName: string
  Email: string
  Password: string

  constructor(UserName: string, FirstName: string, LastName: string, Email: string, Password: string) {
    this.UserName = UserName;
    this.FirstName = FirstName;
    this.LastName = LastName;
    this.Email = Email;
    this.Password = Password;
  }
}
