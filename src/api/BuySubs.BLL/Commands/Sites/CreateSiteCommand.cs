using BuySubs.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Commands.Sites;

public readonly record struct CreateSiteCommand(
    string Name,
    bool IsActive
) : IHttpRequest;
