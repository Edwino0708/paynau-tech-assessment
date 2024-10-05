

using PersonCatalog.Domain.ValueObjects;

namespace PersonCatalog.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                personId => personId.Value,
                dbId => PersonId.Of(dbId)
            );

        builder.Property(c => c.FullName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.DateOfBirth)
            .IsRequired();

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(c => c.Address)
            .HasMaxLength(100);

        builder.Property(c => c.Gender)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Nationality)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(c => c.Occupation)
            .HasMaxLength(100)
            .IsRequired(false);
    }

}
