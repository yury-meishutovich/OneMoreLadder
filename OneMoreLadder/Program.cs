using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OneMoreLadder.DataAccess;
using OnreMoreLadder.BusinessLogic.Contracts;
using OnreMoreLadder.BusinessLogic;
using OneMoreLadder.DataAccess.Contracts;
using OneMoreLadder.DataAccess.Repositories;
using OneMoreLadder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    
    options.AddPolicy("CorsPolicy",
             builder =>
             builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .WithExposedHeaders("content-disposition")
             .AllowAnyHeader()             
             .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));

});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( o => {    
    
    o.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                                        builder.Configuration["OpenIdConnectConfigurationUrl"],
                                    new OpenIdConnectConfigurationRetriever(), new HttpDocumentRetriever()); 
    
    o.TokenValidationParameters = new TokenValidationParameters
    {        
        RequireSignedTokens = true,
        ValidateIssuer = true,       
        ValidateIssuerSigningKey = true,                
        ValidateLifetime = false,
        ValidateAudience = false
        
    };
});

builder.Services.AddScoped<IPlayers, Players>();
builder.Services.AddScoped<IChallenges, Challenges>();
builder.Services.AddScoped<IChallengesRepository, ChallengesRepository>();
builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();
builder.Services.AddScoped<IDataAccessSettings, DataAccessSettings>();
builder.Services.AddScoped<IMatchesRepository, MatchesRepository>();
builder.Services.AddScoped<IMatches, Matches>();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();