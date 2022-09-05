using BuySubs.Common.DTO.Services;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace BuySubs.BLL.Mappings;

[Mapper]
internal sealed partial class ServiceMapper
{
    public partial IEnumerable<ServiceDTO> AdaptToDto(IEnumerable<Service> service);
}