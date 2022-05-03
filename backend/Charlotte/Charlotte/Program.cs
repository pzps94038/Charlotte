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
// 加入CORS服務
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:4200"); // 開放網域
        builder.WithMethods("GET", "POST", "HEAD", "PUT", "PATCH","DELETE", "OPTIONS"); // 開放的請求格式
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service 注入
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
// 驗證JWT
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
// 會自動轉成https請求，沒關閉的話http的請求都會被擋住
// app.UseHttpsRedirection();
// 啟用驗證
app.UseAuthentication();
// 啟用Cors
app.UseCors();
// 套用驗證在路由
app.UseAuthorization();
app.MapControllers();
app.Run();
