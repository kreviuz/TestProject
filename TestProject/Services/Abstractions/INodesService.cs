using TestProject.Db.Entity;

namespace TestProject.Services.Abstractions
{
    public interface INodesService
    {
        Task<IEnumerable<Node>> GetAllAsync(bool includeChildren = true);
        Task<Node?> GetByIdAsync(Guid id, bool includeChildren = true);
        Task<Node?> AddAsync(Node node);
        Task DeleteAsync(Guid id);
        Task<Node?> UpdateAsync(Node node);
    }
}
