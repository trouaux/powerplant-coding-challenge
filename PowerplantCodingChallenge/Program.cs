using PowerplantCodingChallenge.Endpoints;

var builder = WebApplication.CreateBuilder(args);

ProductionPlan.DefineServices(builder.Services);


var app = builder.Build();

ProductionPlan.DefineEndpoint(app);

app.Run();
