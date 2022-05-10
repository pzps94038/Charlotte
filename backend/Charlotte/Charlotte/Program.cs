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
builder.Services.Scan(scan =>  scan
        .FromAssemblyOf<Program>() 
        .AddClasses(classes =>  
            classes.Where(t => t.Name.EndsWith("Helper", StringComparison.OrdinalIgnoreCase)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);
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
