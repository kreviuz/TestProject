using Microsoft.EntityFrameworkCore;

namespace TestProject.Db.Entity
{
    public class Node
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Node? Parent { get; set; }
        public ICollection<Node>? Children { get; set; }
    }
}
