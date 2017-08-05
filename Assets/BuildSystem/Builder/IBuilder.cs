
namespace BuildSystem
{
    public interface IBuilder
    {
        //TODO: Need to implement unity error codes
        string Name { get; }
        string Build(BuildConfiguration config);
        BuildConfiguration GetConfiguration();
        bool RunPreBuildActions(IBuildAction[] actions);
        bool RunPostBuildActions(IBuildAction[] actions);

    }
}
