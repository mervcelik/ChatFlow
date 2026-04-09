using Core.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class AuthorizationException : Exception
{
    public Permission RequiredPermission { get; }

    public AuthorizationException(Permission permission)
        : base($"Bu işlem için yetkiniz yok: {permission}")
    {
        RequiredPermission = permission;
    }

    public AuthorizationException(string message) : base(message) { }
}
