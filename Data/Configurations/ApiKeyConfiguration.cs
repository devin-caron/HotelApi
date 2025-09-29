using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.HasIndex(k => k.Key).IsUnique();
            builder.HasData(
                new ApiKey
                {
                    Id = 1,
                    AppName = "app",
                    CreatedAtUtc = new DateTime(2025, 01, 01),
                    Key = "dXNlcjFAbG9jYWxob3N0LmNvbTpQQHNzd29yZDE="
                }
            );
        }
    }
}
