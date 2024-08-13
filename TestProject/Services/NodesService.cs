using Microsoft.EntityFrameworkCore;
using TestProject.Db;
using TestProject.Db.Entity;
using TestProject.Services.Abstractions;

namespace TestProject.Services
{
    public class NodesService : INodesService
    {
        private readonly NodesDbContext _context;
        private readonly ILogger<NodesService> _logger;

        public NodesService(
            NodesDbContext context, ILogger<NodesService> log)
        {
            _context = context;
            _logger = log;
        }

        public async Task<Node?> GetByIdAsync(Guid id, bool includeChildren = true)
        {
            var query = _context.Nodes.AsNoTracking();
            if (includeChildren)
            {
                query = query.Include(x => x.Children);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Node?> AddAsync(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            node.Id = Guid.NewGuid();

            _context.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return node;
        }

        public async Task DeleteAsync(Guid id)
        {
            var node = await _context.Nodes.FirstOrDefaultAsync(x => x.Id == id);
            if (node != null)
            {
                _context.Nodes.Remove(node);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Node?> UpdateAsync(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var nodeDb = await _context.Nodes.FirstOrDefaultAsync(x => x.Id == node.Id);
            if (nodeDb != null)
            {
                UpdateFields(nodeDb, node);
                await _context.SaveChangesAsync();
                return node;
            }
            return null;
        }

        public async Task<IEnumerable<Node>> GetAllAsync(bool includeChildren = true)
        {
            var query = _context.Nodes.AsNoTracking();
            if (includeChildren)
            {
                query = query.Include(x => x.Children);
            }
            return await query.ToListAsync();
        }

        private void UpdateFields(Node oldValue, Node newValue)
        {
            oldValue.Name = newValue.Name;
            oldValue.ParentId = newValue.ParentId;
        }
    }
}
