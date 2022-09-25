export class ApiUrl {
  static baseUrl: string = 'http://charlottecoffe.ga:8080';
  static apiUrl: string = this.baseUrl + '/api/';
  static login: string = this.apiUrl + 'ManagerLogin';
  static role: string = this.apiUrl + 'ManagerRole';
  static roleAuth: string = this.apiUrl + 'ManagerRoleAuth';
  static managerUser: string = this.apiUrl + 'ManagerUser';
  static user: string = this.apiUrl + 'ManagerUser';
  static router: string = this.apiUrl + 'ManagerRouter';
  static menu: string = this.apiUrl + 'ManagerMenu';
  static refreshToken: string = this.apiUrl + 'ManagerRefreshToken';
  static factory: string = this.apiUrl + 'ManagerFactory';
  static productType: string = this.apiUrl + 'ManagerProductType';
  static product: string = this.apiUrl + 'ManagerProduct';
  static productFileUpload: string = this.apiUrl + 'ManagerProduct\\FileUpload';
  static productEditorFileUpload: string =
    this.apiUrl + 'ManagerProduct\\EditorFileUpload';
  static order: string = this.apiUrl + 'ManagerOrder';
  static orderDetail: string = this.apiUrl + 'ManagerOrderDetail';
  static dashbordRegisteredMember: string =
    this.apiUrl + 'Dashbord' + '/RegisteredMember';
  static dashbordWeekSale: string = this.apiUrl + 'Dashbord' + '/WeekSale';
  static dashbordMonthSale: string = this.apiUrl + 'Dashbord' + '/MonthSale';
  static dashbordHub: string = this.baseUrl + '/' + 'dashbordHub';
}
