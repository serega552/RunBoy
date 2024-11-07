using System;

public static class AwardGiver
{
    public static event Action<string, int> OnReward;

    public static void Reward(string name, int amount)
    {
        OnReward?.Invoke(name, amount);
    }
}
