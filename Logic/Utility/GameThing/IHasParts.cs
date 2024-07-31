using eraSandBoxWpf.Logic.CoitusSimple;

namespace eraSandBoxWpf.Logic.Utility.GameThing;

public interface IHasParts : INeedInitialize
{
    public PartManager parts { get; }
    public string partsTemplate { get; }
}