using NotHeardle.Domain.Common;

namespace NotHeardle.Domain.Models;

public record class GameSession(
    PublicId Id,
    UtcDateTime CreatedAt
);
