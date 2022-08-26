using BuySubs.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Commands.Discounts;

public readonly record struct EditDiscountCommand(
    string Name,
    double Value,
    bool IsActive
) : IHttpRequest;