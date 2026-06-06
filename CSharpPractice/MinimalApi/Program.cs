using Azure.Core.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using MinimalApi.Endpoints;
using MinimalApi.Services;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;

// explain the CreateBuilder method and its parameters
// The CreateBuilder method is a static method of the WebApplication class in ASP.NET Core. It is used to create a new instance of the WebApplicationBuilder class, which is responsible for configuring and building the web application. The CreateBuilder method takes an array of strings as parameters, which represent the command-line arguments passed to the application. These arguments can be used to configure various aspects of the application, such as the environment, configuration sources, and logging settings. The CreateBuilder method returns an instance of the WebApplicationBuilder class, which can be further configured before building the final WebApplication instance.
// Kestrel is a cross-platform web server for ASP.NET Core applications. It is designed to be fast, lightweight, and scalable, making it suitable for hosting web applications in various environments, including cloud and on-premises. Kestrel is the default web server used by ASP.NET Core applications and can be configured to listen on specific ports and protocols. It supports features such as HTTPS, WebSockets, and HTTP/2, allowing developers to build modern web applications with high performance and security.
// how Kestrel related to the WebApplicationBuilder: 
// Kestrel is the default web server used by ASP.NET Core applications, and it is configured through the WebApplicationBuilder. When you create a new instance of the WebApplicationBuilder using the CreateBuilder method, it automatically sets up Kestrel as the web server for your application. You can further configure Kestrel by using the ConfigureKestrel method on the WebApplicationBuilder, which allows you to specify options such as the ports to listen on, SSL settings, and other server-related configurations. In summary, Kestrel is an integral part of the ASP.NET Core application hosting model, and it is configured through the WebApplicationBuilder to serve your web application efficiently.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<UserService>();


// explanation of AddProblemDetails:
// AddProblemDetails is used to configure the application to return standardized error responses
// in a consistent format, following the RFC 7807 specification for problem details.
// This allows clients to easily understand and handle errors returned by the API, providing a structured way to convey error information such as status codes, error messages, and additional details.
// For example, if a client makes a request that results in a validation error, the API can return a response with a status code of 400 (Bad Request) and include a problem details object in the response body that describes the validation errors in a standardized format.
builder.Services.AddProblemDetails();

// example of adding a custom JSON converter globally
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// example of RouteOptions configuration to add a custom route configurations
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // UseExceptionHandler is used to configure a global exception handling middleware that catches unhandled exceptions and returns a standardized error response to the client.
    // It will use IProbemDetailsService to generate a problem details response when an unhandled exception occurs, providing a consistent format for error responses across the application.
    // This allows developers to easily identify and handle errors in a standardized way, improving the overall error handling and debugging experience during development.
    app.UseExceptionHandler();

    // If want to show page with error details, use DeveloperExceptionPage instead of UseExceptionHandler.

    app.MapOpenApi();
    app.MapScalarApiReference();
    

    // dotnet add package Scalar.AspNetCore
    // https://localhost:7218/openapi/v1.json
    // https://localhost:7218/scalar
}

// UseStatusCodePages is used to configure the application to return a default status code response for HTTP status codes that do not have a specific response body defined.
// For example, if a client makes a request that results in a 404 (Not Found) status code, the application will return a default response with the status code and a generic message indicating that the resource was not found.
// This helps to ensure that clients receive a consistent response format for all status codes, even those that do not have a specific response body defined, improving the overall user experience and making it easier for clients to handle different types of responses from the API.
// It intercepts responses with status codes in the 400-599 range and generates a default response body based on the status code, providing a consistent format for error responses across the application.
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// The Webapplication stores the registered endpoints in a collection, and when a request is made to the application, it matches the incoming request to the appropriate endpoint based on the HTTP method and route template defined for each endpoint.
// The collection is shared by the RoutingMiddleware and the EndpointMiddleware, which are responsible for routing incoming requests to the correct endpoint and executing the associated logic for that endpoint.
// When a request is made to the application, the RoutingMiddleware checks the collection of registered endpoints to find a match for the incoming request's HTTP method and route template. If a match is found, the RoutingMiddleware sets the matched endpoint as the current endpoint for the request and passes control to the EndpointMiddleware.
// The EndpointMiddleware then executes the logic associated with the matched endpoint, which may include processing the request, generating a response, and returning it to the client.
// Middleware placed before the routine middleware cann't tell which endpoint will be executed, so it can't do anything with the endpoint. Middleware placed after the routine middleware can access the endpoint and do something with it, such as logging or modifying the response based on the endpoint's metadata.
// So right order of middleware is as following list: 
// 1. Middleware that doesn't need to know which endpoint will be executed (e.g., authentication, logging, etc.)
// 2. Routing middleware (e.g., app.UseRouting())
// 3. Middleware that needs to know which endpoint will be executed (e.g., authorization, endpoint-specific middleware, etc.)
// 4. Endpoint middleware (e.g., app.UseEndpoints())

app.MapHealthChecks("/healthz");
app.MapImageEndpoints();
app.MapUserEndpoints();

app.Run();
