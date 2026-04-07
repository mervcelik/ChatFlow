namespace Core.Authorization.Enums;

public static class RolePermissions
{
    private static readonly Dictionary<MemberRole, Permission> _permissions = new()
    {
        [MemberRole.Member] =
            Permission.ViewRoom |
            Permission.SendMessage |
            Permission.EditOwnMessage |
            Permission.DeleteOwnMessage |
            Permission.UploadFile,

        [MemberRole.Moderator] =
            Permission.ViewRoom |
            Permission.SendMessage |
            Permission.EditOwnMessage |
            Permission.DeleteOwnMessage |
            Permission.DeleteAnyMessage |
            Permission.PinMessage |
            Permission.InviteMember |
            Permission.RemoveMember |
            Permission.UpdateRoomInfo |
            Permission.UploadFile |
            Permission.DeleteAnyFile,

        [MemberRole.Admin] =
            Permission.ViewRoom |
            Permission.SendMessage |
            Permission.EditOwnMessage |
            Permission.DeleteOwnMessage |
            Permission.DeleteAnyMessage |
            Permission.PinMessage |
            Permission.InviteMember |
            Permission.RemoveMember |
            Permission.UpdateRoomInfo |
            Permission.DeleteRoom |
            Permission.PromoteMember |
            Permission.DemoteModerator |
            Permission.UploadFile |
            Permission.DeleteAnyFile,
    };

    public static Permission GetPermissions(MemberRole role)
    {
        return _permissions.TryGetValue(role, out var permissions)
            ? permissions
            : Permission.None;
    }

    public static bool HasPermission(MemberRole role, Permission permission)
    {
        var rolePermissions = GetPermissions(role);
        return (rolePermissions & permission) == permission;
    }
}
