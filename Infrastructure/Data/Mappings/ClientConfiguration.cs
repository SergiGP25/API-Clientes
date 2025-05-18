using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Mappings
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.IdentificationNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.IdentificationType)
                   .IsRequired();

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.BirthDate)
                   .IsRequired();

            builder.Property(c => c.IsDeleted)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.Property(c => c.UpdatedAt)
                   .IsRequired();
            builder.Ignore(c => c.Age);
        }
    }
}
