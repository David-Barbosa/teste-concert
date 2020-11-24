using Concert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concert.Infra.Mappings
{
    public class UserStoryMap : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.ToTable("user_story");
            builder.Ignore(x => x.Notifications);

            builder.Property(x => x.Id)
               .HasColumnName("id");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("description");
        }
    }
}
