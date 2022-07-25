export class ApiUrl{
  static baseUrl : string= "http://172.30.144.1:8080"
  static apiUrl : string = this.baseUrl + '/api/'
  static login : string = this.apiUrl + 'ManagerLogin'
  static role: string = this.apiUrl + 'ManagerRole'
  static roleAuth: string = this.apiUrl + 'ManagerRoleAuth'
  static user: string = this.apiUrl + 'ManagerUser'
  static router: string = this.apiUrl + 'ManagerRouter'
  static menu: string = this.apiUrl + 'ManagerMenu'
  static refreshToken: string = this.apiUrl + 'ManagerRefreshToken'
  static factory: string = this.apiUrl + 'ManagerFactory'
  static productType: string = this.apiUrl + 'ManagerProductType'
  static product: string = this.apiUrl + 'ManagerProduct'
  static productFileUpload: string = this.apiUrl + 'ManagerProduct\\FileUpload'
  static order: string = this.apiUrl + 'ManagerOrder'
}
