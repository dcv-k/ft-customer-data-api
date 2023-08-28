using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = "Guest",
                NormalizedName = "GUEST"
            },

            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        );
    }
}
