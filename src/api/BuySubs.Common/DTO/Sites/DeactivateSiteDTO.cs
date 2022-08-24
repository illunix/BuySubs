using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.Common.DTO.Sites;

[DynamoDBTable("Sites")]
public record DeactivateSiteDTO
{
    public string? Name { get; init; }

    public bool? isActive { get; init; }
}
