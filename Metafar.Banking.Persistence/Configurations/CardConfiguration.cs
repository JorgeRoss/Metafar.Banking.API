using Metafar.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metafar.Banking.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder
            .HasMany(c => c.Transactions)
            .WithOne(t => t.Card)
            .HasForeignKey(t => t.CardId);
        }
    }
}
