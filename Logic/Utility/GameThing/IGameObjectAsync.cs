namespace eraSandBoxWpf.Logic.Utility.GameThing;

public interface IGameObject
{
    public void TakeTurn();
}

public interface IGameObjectAsync<out TProgress>
{
    public Task TakeTurn(IProgress<TProgress> progress);
}