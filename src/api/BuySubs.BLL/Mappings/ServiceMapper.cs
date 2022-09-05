using BuySubs.Common.DTO.Services;
using BuySubs.Common.DTO.Sites;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Mappings;

[Mapper]
internal sealed partial class ServiceMapper
{
    public partial IEnumerable<ServiceDTO> AdaptToDto(IEnumerable<Service> service);
}