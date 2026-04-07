using Core.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions;

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

public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} bulunamadı. Id: {id}") { }
}

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}