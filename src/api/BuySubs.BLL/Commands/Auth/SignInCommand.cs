using BuySubs.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySubs.BLL.Commands.Auth;

public readonly record struct SignInCommand(
    string Email,
    string Password
) : IHttpRequest;