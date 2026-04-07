using Application.Repositories;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RoomAuthorizationService;

public class RoomAuthorizationManager : IRoomAuthorizationService
{
    private readonly IRoomMemberRepository _memberRepository;

    public RoomAuthorizationManager(IRoomMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> HasPermissionAsync(Guid userId, Guid roomId, Permission permission)
    {
        var role = await GetRoleAsync(userId, roomId);

        if (role is null)
            return false;

        return RolePermissions.HasPermission(role.Value, permission);
    }

    public async Task RequirePermissionAsync(Guid userId, Guid roomId, Permission permission)
    {
        var hasPermission = await HasPermissionAsync(userId, roomId, permission);

        if (!hasPermission)
            throw new AuthorizationException(permission);
    }

    public async Task<MemberRole?> GetRoleAsync(Guid userId, Guid roomId)
    {
        var member = await _memberRepository.GetAsync(
            m => m.RoomId == roomId && m.UserId == userId);

        return member?.Role;
    }
}
