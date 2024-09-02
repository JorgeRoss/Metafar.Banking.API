using Metafar.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metafar.Banking.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
            .Property(a => a.Amount)
            .HasPrecision(18, 2);

            builder
            .HasOne(t => t.TransactionType)
            .WithMany()
            .HasForeignKey(t => t.TransactionTypeId);
        }
    }
}
