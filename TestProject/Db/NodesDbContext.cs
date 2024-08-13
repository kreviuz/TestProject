using Microsoft.EntityFrameworkCore;
using TestProject.Db.Entity;

namespace TestProject.Db;

public partial class NodesDbContext : DbContext
{
    public NodesDbContext()
    {
    }

    public NodesDbContext(DbContextOptions<NodesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Node> Nodes { get; set; }
    public DbSet<ExceptionRecord> ExceptionJournal { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:NodesDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var nodeEntity = modelBuilder.Entity<Node>();
        nodeEntity.HasKey(n => n.Id);
        nodeEntity.HasOne(n => n.Parent)
            .WithMany(n => n.Children)
            .HasForeignKey(n => n.ParentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        nodeEntity.HasData(
            new Node { Id = new Guid("EEC6B9B3-9485-43A4-8833-08B0C0F969B3"), Name = "Root", ParentId = null });

        var exceptionEntity = modelBuilder.Entity<ExceptionRecord>();
        exceptionEntity.HasKey(er => er.Id);
        exceptionEntity.OwnsOne(er => er.Data, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
