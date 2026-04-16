using Domain.Enums;

namespace Application.Features.Messages.Commands.Update;

public class UpdatedMessageResponse
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime? EditedAt { get; set; }
}
