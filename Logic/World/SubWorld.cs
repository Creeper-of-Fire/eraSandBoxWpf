using eraSandBoxWpf.Logic.Pawn;
using eraSandBoxWpf.Logic.Utility.GameThing;

namespace eraSandBoxWpf.Logic.World;

/**
 * 所有的地图都是SubWorld，它们之间可以嵌套，并且每个SubWorld都有Cell，某些Cell会是通往其他SubWorld的出入口
 */
public class SubWorld(string id) : IGameObjectAsync<SubWorld.SubWorldProgressInfo>
{
    public readonly List<Cell> cells = [];
    public string ID = id;

    public SubWorld AddCell(Cell cell)
    {
        this.cells.Add(cell);
        return this;
    }

    public struct SubWorldProgressInfo(int percentage, string description)
    {
        public int percentage = percentage;
        public string description = description;
    }

    // public void TakeTurn()
    // {
    //     this.cells.ForEach(cell => cell.TakeTurn());
    // }

    public Task TakeTurn(IProgress<SubWorldProgressInfo> progress)
    {
        int totalCell = this.cells.Count;
        int index = 0;
        foreach (var cell in this.cells)
        {
            cell.TakeTurn();
            progress.Report(new SubWorldProgressInfo(index / totalCell * 100, $"正在处理单元格：{index}/{totalCell}"));
            index++;
        }

        return Task.CompletedTask;
    }
}

public static class SubWorldUtility
{
    // public static Dictionary<string,>
    public static SubWorld makeTestSubWorld()
    {
        var subWorld = new SubWorld("test");
        var cell = new Cell(subWorld, "test");
        return subWorld.AddCell(cell.AddPawn(new Animal(cell)));
    }
}