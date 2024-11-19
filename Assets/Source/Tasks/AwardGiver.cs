using System;

namespace Tasks
{
    public static class AwardGiver
    {
        public static event Action<string, int> Rewarding;

        public static void Reward(string name, int amount)
        {
            Rewarding?.Invoke(name, amount);
        }
    }
}