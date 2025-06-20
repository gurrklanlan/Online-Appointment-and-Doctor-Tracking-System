using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistance.Configuration
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
           builder.HasKey(mr => mr.Id);

            builder.Property(mr => mr.PatientId)
                  .IsRequired();

            builder.HasOne(x=>x.Doctor)
                   .WithOne()
                   .HasForeignKey<MedicalRecord>(x => x.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(mr => mr.Diagnosis)
                    .IsRequired()
                    .HasMaxLength(500);

            builder.Property(mr => mr.RecordDate)
                    .IsRequired()
                    .HasColumnType("date");

            builder.Property(x=>x.Prescription)
                    .IsRequired()
                    .HasMaxLength(1000);

            builder.HasOne(mr => mr.Patient)
                     .WithMany()
                     .HasForeignKey(mr => mr.PatientId)
                     .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
