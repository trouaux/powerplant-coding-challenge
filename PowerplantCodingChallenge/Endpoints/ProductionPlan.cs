using Microsoft.AspNetCore.Mvc;
using PowerplantCodingChallenge.Models;
using PowerplantCodingChallenge.Services;

namespace PowerplantCodingChallenge.Endpoints;

public static class ProductionPlan
{
    public static void DefineEndpoint(WebApplication app)
    {
        app.MapPost("/productionplan", PlanProduction);
    }

    public static void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ProductionPlanner, ProductionPlanner>();
    }

    internal static IResult PlanProduction(
        [FromBody] Payload payload,
        ProductionPlanner productionPlanner
        )
    {
        //Validate payload

        var res = productionPlanner.Plan(payload);

        if (res == null)
        {
            return Results.BadRequest("Couldn't match required Load");
        }

        return Results.Ok(res);
    }
}
