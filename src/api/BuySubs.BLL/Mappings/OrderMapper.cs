using BuySubs.BLL.Commands.Orders;
using BuySubs.Common.DTO.Orders;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace BuySubs.BLL.Mappings;

[Mapper]
public sealed partial class OrderMapper
{
    public partial IEnumerable<OrderDTO> AdaptToDto(IEnumerable<Order> order);
    public partial Order AdaptToEntity(CreateOrderCommand req);
    public partial Order AdaptToEntity(UpdateOrderCommand req);
}