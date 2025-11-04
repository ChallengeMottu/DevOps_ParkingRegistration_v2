using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseSystem.Domain.Entities;

namespace PulseSystem.Infraestructure.Persistence.Mappings
{
    public class ZoneMapping : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable("Zones");

            // Chave primária
            builder.HasKey(z => z.Id);

            // Propriedades
            builder.Property(z => z.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(z => z.Description)
                .HasMaxLength(500)
                .HasColumnType("VARCHAR(500)");

            builder.Property(z => z.Width)
                .IsRequired()
                .HasColumnType("FLOAT"); // ou DECIMAL(10,2) caso seja uma medida com casas decimais

            builder.Property(z => z.Length)
                .IsRequired()
                .HasColumnType("FLOAT"); // ou DECIMAL(10,2)

            // Relacionamento com Parking
            builder.HasOne(z => z.Parking)
                .WithMany(p => p.Zones)
                .HasForeignKey(z => z.ParkingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}