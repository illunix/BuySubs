using BuySubs.Common.DTO.Sites;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace BuySubs.BLL.Mappings;

[Mapper]
internal sealed partial class SiteMapper
{
    public partial IEnumerable<SiteDTO> AdaptToDto(IEnumerable<Site> site);
}