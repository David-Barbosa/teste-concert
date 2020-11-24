using Concert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concert.Infra.Mappings
{
    public class CardMap : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("card");
            builder.Ignore(x => x.Notifications);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.ValueCard)
                .IsRequired()
                .HasColumnName("value_card");
        }
    }
}
