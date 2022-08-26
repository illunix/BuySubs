using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Exceptions.Discounts;

public sealed class DiscountWithThisNameAlreadyExistException : Exception
{
    public DiscountWithThisNameAlreadyExistException() : base($"There is already discount with this name.") { }
}