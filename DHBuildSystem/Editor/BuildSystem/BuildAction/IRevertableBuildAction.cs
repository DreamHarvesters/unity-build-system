namespace DH.BuildSystem
{
    public interface IRevertableBuildAction
    {
        bool WillBeReverted { get; }
        bool Revert();
    }
}