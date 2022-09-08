using BuySubs.BLL.Commands.Discounts;
using BuySubs.BLL.Commands.Services;
using BuySubs.Common.DTO.Discounts;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace BuySubs.BLL.Mappings;

[Mapper]
public sealed partial class DiscountMapper
{
    public partial IEnumerable<DiscountDTO> AdaptToDto(IEnumerable<Discount> discount);
    public partial Discount AdaptToEntity(CreateDiscountCommand req);
    public partial Discount AdaptToEntity(UpdateDiscountCommand req);
}