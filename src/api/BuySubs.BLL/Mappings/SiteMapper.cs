using BuySubs.BLL.Commands.Sites;
using BuySubs.Common.DTO.Sites;
using BuySubs.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace BuySubs.BLL.Mappings;

[Mapper]
public sealed partial class SiteMapper
{
    public partial IEnumerable<SiteDTO> AdaptToDto(IEnumerable<Site> site);
    public partial Site AdaptToEntity(CreateSiteCommand req);
    public partial Site AdaptToEntity(UpdateSiteCommand req);
}