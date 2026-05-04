var builder = WebApplication.CreateBuilder(args);

// Added CORS so the Flutter app can talk to this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

// --- MOCK API ENDPOINTS ---

// 1. LOGIN
app.MapPost("/api/login", (LoginRequest request) =>
{
    // Simple logic: if email is test@test.com and password is '12345'
    if (request.Email == "test@test.com" && request.Password == "12345")
    {
        return Results.Ok(new { 
            Token = "secret-jwt-mock-token-123", 
            UserName = "Elizabeth",
            Message = "Login successful!" 
        });
    }
    return Results.Unauthorized();
});

// 2. REGISTER
app.MapPost("/api/register", (RegisterRequest request) =>
{
    return Results.Ok(new { 
        Message = $"Account for {request.Name} created successfully!" 
    });
});

app.Run();

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string Name);