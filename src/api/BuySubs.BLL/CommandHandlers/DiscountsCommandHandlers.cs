using Amazon.DynamoDBv2.DataModel;
using BuySubs.BLL.Commands.Discounts;
using BuySubs.BLL.Exceptions;
using BuySubs.BLL.Exceptions.Discounts;
using BuySubs.Common.DTO.Discounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuySubs.BLL.CommandHandlers;

internal sealed class DiscountsCommandHandlers :
    IRequestHandler<ActivateDiscountCommand, IResult>,
    IRequestHandler<CreateDiscountCommand, IResult>,
    IRequestHandler<DeactivateDiscountCommand, IResult>,
    IRequestHandler<DeleteDiscountCommand, IResult>,
    IRequestHandler<EditDiscountCommand, IResult>
{
    private readonly IDynamoDBContext _ctx;

    public DiscountsCommandHandlers(IDynamoDBContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost("discounts")]
    public async Task<IResult> Handle(
        CreateDiscountCommand req,
        CancellationToken ct
    )
    {
        if (await _ctx.LoadAsync<DiscountWithThisNameAlreadyExistDTO>(req.Name) is not null)
        {
            throw new DiscountWithThisNameAlreadyExistException();
        }

        await _ctx.SaveAsync(new CreateDiscountDTO
        {
            Name = req.Name,
            Value = req.Value,
            IsActive = req.IsActive
        });

        return Results.Ok();
    }

    [HttpPost("discounts/activate")]
    public async Task<IResult> Handle(
        ActivateDiscountCommand req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.LoadAsync<ActivateDiscountDTO>(req.Name);

        if (discount is null)
        {
            throw new NotFoundException(nameof(ActivateDiscountDTO));
        }

        if (discount.IsActive is true)
        {
            throw new DiscountIsAlreadyActiveException();
        }

        await _ctx.SaveAsync(new ActivateDiscountDTO
        {
            Name = req.Name,
            IsActive = true
        });

        return Results.Ok();
    }

    [HttpPost("discounts/deactivate")]
    public async Task<IResult> Handle(
        DeactivateDiscountCommand req,
        CancellationToken ct
)
    {
        var discount = await _ctx.LoadAsync<DeactivateDiscountDTO>(req.Name);

        if (discount is null)
        {
            throw new NotFoundException(nameof(DeactivateDiscountDTO));
        }

        if (discount.IsActive is false)
        {
            throw new DiscountIsAlreadyInactiveException();
        }

        await _ctx.SaveAsync(new DeactivateDiscountDTO
        {
            Name = req.Name,
            IsActive = true
        });

        return Results.Ok();
    }

    [HttpDelete("discounts")]
    public async Task<IResult> Handle(
        DeleteDiscountCommand req,
        CancellationToken ct
)
    {
        var discount = await _ctx.LoadAsync<DeleteDiscountDTO>(req.Name);

        if (discount is null)
        {
            throw new NotFoundException(nameof(DeleteDiscountDTO));
        }

        await _ctx.DeleteAsync(discount);

        return Results.Ok();
    }

    [HttpPut("discounts")]
    public async Task<IResult> Handle(
        EditDiscountCommand req,
        CancellationToken ct
    )
    {
        var discount = await _ctx.LoadAsync<EditDiscountDTO>(req.Name);
        
        if (discount is null)
        {
            throw new NotFoundException(nameof(EditDiscountDTO));
        }

        await _ctx.SaveAsync(new EditDiscountDTO
        {
            Name = req.Name,
            Value = req.Value,
            IsActive = req.IsActive
        });

        return Results.Ok();
    }
}