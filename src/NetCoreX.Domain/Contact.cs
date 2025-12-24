using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace NetCoreX.Domain
{
    public class Contact : Entity, IEntityTypeConfiguration<Contact>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DoB { get; set; }

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.DoB)
                .HasColumnType("date")
                .IsRequired();
        }
    }
}
