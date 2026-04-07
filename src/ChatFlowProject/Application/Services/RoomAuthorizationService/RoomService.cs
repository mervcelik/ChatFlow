using Application.Repositories;
using Core.Authorization.Enums;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Services.RoomAuthorizationService;

public class RoomService
{
    private readonly IRoomMemberRepository _memberRepository;
    private readonly IRoomAuthorizationService _authService;
    public RoomService(
        IRoomMemberRepository memberRepository,
        IRoomAuthorizationService authService,
        IMessageRepository messageRepository)
    {
        _memberRepository = memberRepository;
        _authService = authService;
    }

    public async Task RemoveMemberAsync(Guid requesterId, Guid roomId, Guid targetUserId)
    {
        // Kendi kendini çıkaramaz
        if (requesterId == targetUserId)
            throw new BusinessException("Kendinizi odadan çıkaramazsınız.");

        // Yetki kontrolü — yetkisi yoksa AuthorizationException fırlatır
        await _authService.RequirePermissionAsync(
            requesterId, roomId, Permission.RemoveMember);

        // Admin'i odadan çıkarmaya çalışıyorsa engelle
        var targetRole = await _authService.GetRoleAsync(targetUserId, roomId);

        if (targetRole == MemberRole.Admin)
            throw new AuthorizationException("Admin kullanıcıyı odadan çıkaramazsınız.");

        var member = await _memberRepository.GetAsync(
            m => m.RoomId == roomId && m.UserId == targetUserId)
            ?? throw new NotFoundException("RoomMember", targetUserId);

        await _memberRepository.DeleteAsync(member);
    }

    public async Task PromoteMemberAsync(Guid requesterId, Guid roomId, Guid targetUserId)
    {
        await _authService.RequirePermissionAsync(
            requesterId, roomId, Permission.PromoteMember);

        var member = await _memberRepository.GetAsync(
            m => m.RoomId == roomId && m.UserId == targetUserId)
            ?? throw new NotFoundException("RoomMember", targetUserId);

        if (member.Role == MemberRole.Admin)
            throw new BusinessException("Kullanıcı zaten admin.");

        await _memberRepository.UpdateFieldAsync(member, m => m.Role, MemberRole.Moderator);
    }
}