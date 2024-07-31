using eraSandBoxWpf.Logic.Thought;

namespace eraSandBoxWpf.Logic.Utility.GameThing;

public interface ICanMakeMessage
{
    public IEnumerable<MessageSpreader> MakeMessageSpreader();
    public View ProcessView(View view);
}