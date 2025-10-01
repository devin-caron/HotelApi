using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "87b3be97-263d-4df0-940f-1137e872fbc7",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "fdae598e-9d14-4cd6-a31c-360d2ee39b23",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "36aac992-4c8a-4527-9008-98394b071953",
                    Name = "Hotel Admin",
                    NormalizedName = "HOTEL ADMIN"
                }
            );
        }
    }
}
