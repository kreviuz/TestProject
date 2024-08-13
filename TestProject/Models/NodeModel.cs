using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestProject.Models
{
    public class NodeModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
        public IEnumerable<NodeModel>? Children { get; set; }
    }

    public class NodeModelValidator : AbstractValidator<NodeModel>
    {
        public NodeModelValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ParentId)
                .NotEmpty();
        }
    }
}
