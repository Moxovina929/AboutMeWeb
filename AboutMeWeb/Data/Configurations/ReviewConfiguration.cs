using AboutMeWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AboutMeWeb.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasOne(r => r.Service)
                .WithOne(s => s.Review)
                .HasForeignKey<Review>(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
