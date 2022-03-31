export class ApiUrl{
  static baseUrl : string= "http://localhost:5165"
  static apiUrl : string = this.baseUrl + '/api/'
  static login : string = this.apiUrl + 'ManagerLogin'
  static role: string = this.apiUrl + 'ManagerRole'
  static user: string = this.apiUrl + 'ManagerUser'
}
