using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiGatewayExamen.Repositories;
using ApiGatewayExamen.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiGatewayExamen;

public class Functions
{
    private RepositoryPeliculas repo;


    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions(RepositoryPeliculas repo)
    {
        this.repo = repo;
    }


    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/GetPeliculas")]
    public async Task<IHttpResult> GetPeliculas(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
        return HttpResults.Ok(peliculas);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/GetPeliculasActores/{actor}")]
    public async Task<IHttpResult> GetPeliculasActores(string actor, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Find' Request");
        List<Pelicula> peliculas = await this.repo.GetPeliculasActoresAsync(actor);
        return HttpResults.Ok(peliculas);
    }
}
