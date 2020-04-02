- use http cache headers

//handling exceptions in case of 500. Enable exc..for non-development environments
//add a head for authors
//don't add the FromQuery parameter to prove a 415
problem details http api rfc rfc7807

//configureApiBehavior docs& specs

  .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https://myconference.com/modelvalidationproblem",
                        Title = "One or more model validation errors occurred.",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "See the errors property for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

PUT
-full updates, fields overwritten, or updated

Patch
- partial updates via JSON Patch document
rfc 6902
for patch, the content-type is application/json-patch/json and not 
slides about patch operations
Building a RESTful API with ASP.NET Core 3 - patch operations, multiple ops
Vendor specific media types(json + HAL)
- 2 reprs of the same type - Produces/Consumes
- semantic media types
- friendly repr with links, without links, 

- subtype without sufix: .hateoas
- full, friendly + json or hateoas
- adding [Produces] over the action it will only enforce strictness. it can be added at controller level.
No need to add that globally

- Values for input:
 -consuming different types

 - versioning:
