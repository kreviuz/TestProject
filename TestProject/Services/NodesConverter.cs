using AutoMapper;
using TestProject.Db.Entity;
using TestProject.Models;
using TestProject.Services.Abstractions;

namespace TestProject.Services
{
    public class NodesConverter : INodesConverter
    {
        private readonly IMapper _mapper;
        private readonly ILogger<NodesConverter> _logger;

        public NodesConverter(IMapper mapper, ILogger<NodesConverter> log)
        {
            _mapper = mapper;
            _logger = log;
        }

        public NodeModel ConvertEntityToModel(Node node)
        {
            NodeModel nodeModel = _mapper.Map<NodeModel>(node);
            return nodeModel;
        }

        public Node ConvertModelToEntity(NodeModel nodeModel)
        {
            Node node = _mapper.Map<Node>(nodeModel);
            return node;
        }

        public IEnumerable<NodeModel> ConvertEntityToModel(IEnumerable<Node> nodes)
        {
            var nodeModel = _mapper.Map<IEnumerable<NodeModel>>(nodes);
            return nodeModel;
        }

        public IEnumerable<Node> ConvertEntityToModel(IEnumerable<NodeModel> nodeModels)
        {
            var nodes = _mapper.Map<IEnumerable<Node>>(nodeModels);
            return nodes;
        }
    }
}
