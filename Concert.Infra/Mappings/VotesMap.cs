using Concert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concert.Infra.Mappings
{
    public class VotesMap : IEntityTypeConfiguration<Votes>
    {
        public void Configure(EntityTypeBuilder<Votes> builder)
        {
            builder.ToTable("votes");
            builder.Ignore(x => x.Notifications);

            builder.Property(x => x.Id)
               .HasColumnName("id");


            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("id_user");

            builder.HasOne(x => x.User);

            builder.Property(x => x.UserStoryId)
                .IsRequired()
                .HasColumnName("id_user_story");

            builder.HasOne(x => x.UserStory);

            builder.Property(x => x.CardId)
                .IsRequired()
                .HasColumnName("id_card");

            builder.HasOne(x => x.Card);
        }
    }
}
