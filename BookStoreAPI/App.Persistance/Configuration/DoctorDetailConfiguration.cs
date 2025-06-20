using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistance.Configuration
{
    public class DoctorDetailConfiguration : IEntityTypeConfiguration<DoctorDetail>
    {
        public void Configure(EntityTypeBuilder<DoctorDetail> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(x=>x.Specialty)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ClinicAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<DoctorDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict); 



        }
    }
}
