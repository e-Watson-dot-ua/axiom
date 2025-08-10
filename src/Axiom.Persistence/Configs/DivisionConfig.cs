using Axiom.Domain.Entities.Divisions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axiom.Persistence.Configs;

public class DivisionConfig : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.ToTable("Divisions", d => d.HasCheckConstraint("CK_Divisions_NotSelfParent", "[Id]<>[ParentId]"));

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("Id").HasDefaultValueSql("NEWID()");

        builder.Property(d => d.ParentId).HasColumnName("ParentId");

        builder.Property(d => d.Code).HasColumnName("Code").HasMaxLength(50).IsRequired();
        builder.Property(d => d.Name).HasColumnName("Name").HasMaxLength(200).IsRequired();
        builder.Property(d => d.ShortName).HasColumnName("ShortName").HasMaxLength(100).IsRequired();
        builder.Property(d => d.SortOrder).HasColumnName("SortOrder").HasDefaultValue(0).IsRequired();

        builder.Property(d => d.IsInternal).HasColumnName("IsInternal").HasDefaultValue(false)
            .IsRequired();

        builder.HasIndex(d => d.Code).IsUnique().HasDatabaseName("UK_Divisions_Code");
        builder.HasIndex(d => d.ParentId).HasDatabaseName("IX_Divisions_ParentId");
        builder.HasIndex(d => d.Code).HasDatabaseName("IX_Divisions_Code");
        builder.HasIndex(d => d.SortOrder).HasDatabaseName("IX_Divisions_SortOrder");

        builder.HasOne<Division>()
            .WithMany()
            .HasForeignKey(d => d.ParentId)
            .HasConstraintName("FK_Divisions_Parent");
    }
}