using Carter;
using CustomerAPI.ViewModels;
using Domain.Customers.Create;
using Domain.Customers.Delete;
using Domain.Customers.Read;
using Domain.Customers.Update;
using Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Endpoints
{
    public class Customers : CarterModule
    {
        public Customers() : base("/customers")
        {
            this.RequireAuthorization();
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (CreateCustomerCommand command, ISender sender) =>
            {
                var customerId = await sender.Send(command);

                return Results.Ok(customerId);
            });

            app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    var query = new GetCustomerQuery(id);

                    var customer = await sender.Send(query);

                    return Results.Ok(new CustomerResponse(customer.Id, customer.Name, customer.Address, customer.Email));
                }
                catch (CustomerNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateCustomerRequest request, ISender sender) =>
            {
                try
                {
                    var command = new UpdateCustomerCommand(id, request.Name, request.Address, request.Email);

                    await sender.Send(command);

                    return Results.NoContent();
                }
                catch (CustomerNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteCustomerCommand(id));

                    return Results.NoContent();
                }
                catch (CustomerNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
        }
    }
}
