using AboutMeWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AboutMeWeb.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasOne(s => s.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
