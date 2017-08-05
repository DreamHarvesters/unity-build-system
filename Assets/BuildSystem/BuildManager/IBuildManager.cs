
namespace BuildSystem
{
    public interface IBuildManager
    {
        string Build(IBuilder builder);
        IBuilder[] GetBuilders(string filter);
        IBuilder GetBuilder(string name);
    }
}
