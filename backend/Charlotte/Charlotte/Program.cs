using Charlotte.Helper;
using Charlotte.Helper.Factory;
using Charlotte.Helper.Login;
using Charlotte.Helper.ManagerLogin;
using Charlotte.Helper.ManagerMenu;
using Charlotte.Helper.ManagerRefreshToken;
using Charlotte.Helper.ManagerRole;
using Charlotte.Helper.ManagerRoleAuth;
using Charlotte.Helper.ManagerRouter;
using Charlotte.Helper.ManagerUser;
using Charlotte.Helper.Product;
using Charlotte.Helper.Register;
using Charlotte.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// �[�JCORS�A��
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:4200"); // �}�����
        builder.WithMethods("GET", "POST", "HEAD", "PUT", "PATCH","DELETE", "OPTIONS"); // �}�񪺽ШD�榡
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service �`�J
builder.Services.AddTransient<IFactoryHelper, FactoryHelper>();
builder.Services.AddTransient<ILoginHelper, LoginHelper>();
builder.Services.AddTransient<IManagerLoginHelper, ManagerLoginHelper>();
builder.Services.AddTransient<IManagerMenuHelper, ManagerMenuHelper>();
builder.Services.AddTransient<IManagerRefreshTokenHelper, ManagerRefreshTokenHelper>();
builder.Services.AddTransient<IManagerRoleHelper, ManagerRoleHelper>();
builder.Services.AddTransient<IManagerRoleAuthHelper, ManagerRoleAuthHelper>();
builder.Services.AddTransient<IManagerRouterHelper, ManagerRouterHelper>();
builder.Services.AddTransient<IManagerUserHelper, ManagerUserHelper>();
builder.Services.AddTransient<IProductHelper, ProductHelper>();
builder.Services.AddTransient<IRegisterHelper, RegisterHelper>();
// ����JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetAppSettingsHelper.GetAppSettingsValue("JWT", "Key"))),
                    ValidateIssuer = true,
                    ValidIssuer = GetAppSettingsHelper.GetAppSettingsValue("JWT", "Issuer"),
                    ValidateAudience = true,
                    ValidAudience = GetAppSettingsHelper.GetAppSettingsValue("JWT", "Audience"),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
// �|�۰��নhttps�ШD�A�S��������http���ШD���|�Q�צ�
// app.UseHttpsRedirection();
// �ҥ�����
app.UseAuthentication();
// �ҥ�Cors
app.UseCors();
// �M�����Ҧb����
app.UseAuthorization();
app.MapControllers();
app.Run();
