using Charlotte.Helper;
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
