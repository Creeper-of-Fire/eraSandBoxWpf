using eraSandBoxWpf.Logic.Utility.GameThing;
using static eraSandBoxWpf.Logic.World.SubWorld;
using static eraSandBoxWpf.Logic.World.TotalWorld;

namespace eraSandBoxWpf.Logic.World;

/**
 * 全世界
 */
public class TotalWorld : IGameObjectAsync<TotalWorldProgressInfo>
{
    private TotalWorld()
    {
        this.subWorlds.Add(SubWorldUtility.makeTestSubWorld());
    }

    private readonly List<SubWorld> subWorlds = [];
    private Dictionary<Action, int> Actions { get; } = new();

    public static TotalWorld Instance { get; } = new();

    public readonly struct TotalWorldProgressInfo(
        int percentage,
        string description,
        IReadOnlyList<IProgress<SubWorldProgressInfo>> subWorldProgress)
    {
        public readonly int percentage = percentage;
        public readonly string description = description;
        public readonly IReadOnlyList<IProgress<SubWorldProgressInfo>> subWorldProgress = subWorldProgress;
    }

    public Task TakeTurn(IProgress<TotalWorldProgressInfo> progress)
    {
        int totalProgress = this.subWorlds.Count;
        int totalWorld = this.subWorlds.Count;
        var subWorldProgress = new List<Progress<SubWorldProgressInfo>>(totalWorld);

        for (int index = 0; index < this.subWorlds.Count; index++)
        {
            var world = this.subWorlds[index];
            progress.Report(new TotalWorldProgressInfo(
                index / totalProgress * 100, $"正在处理子世界：{index}/{totalWorld}", subWorldProgress));
            world.TakeTurn(subWorldProgress[index]);
        }

        foreach (var keyValuePair in this.Actions.OrderBy(pair => pair.Value)) keyValuePair.Key.DynamicInvoke();

        this.Actions.Clear();
        return Task.CompletedTask;
    }


    public static class TotalWorldUtility
    {
        // public static void AddToTop(this Action action)
        // {
        //     TotalWorld.Instance.Actions.Add(action, int.MinValue);
        // }
        //
        // public static void AddToBot(this Action action)
        // {
        //     TotalWorld.Instance.Actions.Add(action, int.MaxValue);
        // }
        //
        // public static void AddToMid(this Action action)
        // {
        //     TotalWorld.Instance.Actions.Add(action, 0);
        // }
        //
        // public static void AddAction(this Action action, int order)
        // {
        //     TotalWorld.Instance.Actions.Add(action, order);
        // }

        public static void AddToTop(params Action[] actions)
        {
            foreach (var action in actions)
                Instance.Actions.Add(action, int.MinValue);
        }

        public static void AddToBot(params Action[] actions)
        {
            foreach (var action in actions)
                Instance.Actions.Add(action, int.MaxValue);
        }

        public static void AddToMid(params Action[] actions)
        {
            foreach (var action in actions)
                Instance.Actions.Add(action, 0);
        }
    }
}