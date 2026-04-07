using Core.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RoomAuthorizationService;

public interface IRoomAuthorizationService
{
    /// Kullanıcının odada belirtilen yetkiye sahip olup olmadığını kontrol eder.
    Task<bool> HasPermissionAsync(Guid userId, Guid roomId, Permission permission);

    /// Yetkisi yoksa AuthorizationException fırlatır.
    /// Service katmanında guard clause olarak kullanılır.
    Task RequirePermissionAsync(Guid userId, Guid roomId, Permission permission);

    /// Kullanıcının odadaki rolünü döner. Üye değilse null.
    Task<MemberRole?> GetRoleAsync(Guid userId, Guid roomId);
}
