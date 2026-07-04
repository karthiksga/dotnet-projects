using System.Collections.Concurrent;

namespace PolicyBasedAuthorization.Services;

public class SubscriptionStore
{
    private readonly ConcurrentDictionary<string, string> _tiers = new();
    public void SetTier(string userId, string tier) => _tiers[userId] = tier;
    public string? GetTier(string userId) => _tiers.GetValueOrDefault(userId,"free");
}
