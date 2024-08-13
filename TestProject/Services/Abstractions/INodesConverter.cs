using TestProject.Db.Entity;
using TestProject.Models;

namespace TestProject.Services.Abstractions
{
    public interface INodesConverter
    {
        NodeModel ConvertEntityToModel(Node node);
        Node ConvertModelToEntity(NodeModel nodeModel);
        IEnumerable<NodeModel> ConvertEntityToModel(IEnumerable<Node> nodes);
        IEnumerable<Node> ConvertEntityToModel(IEnumerable<NodeModel> nodeModels);
    }
}
