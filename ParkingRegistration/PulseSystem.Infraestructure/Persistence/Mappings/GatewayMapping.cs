using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseSystem.Domain.Entities;

namespace PulseSystem.Infraestructure.Persistence.Mappings
{
    public class GatewayMapping : IEntityTypeConfiguration<Gateway>
    {
        public void Configure(EntityTypeBuilder<Gateway> builder)
        {
            builder.ToTable("Gateways");

            // Chave primária
            builder.HasKey(g => g.Id);

            // Propriedades principais
            builder.Property(g => g.Model)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(g => g.Status)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(g => g.MacAddress)
                .IsRequired()
                .HasMaxLength(17)
                .HasColumnType("VARCHAR(17)");

            builder.Property(g => g.LastIP)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("VARCHAR(15)");

            builder.Property(g => g.RegisterDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); // substitui SYSDATE do Oracle

            builder.Property(g => g.MaxCoverageArea)
                .IsRequired()
                .HasColumnType("FLOAT"); // ou DECIMAL(10,2) se for uma medida fixa

            builder.Property(g => g.MaxCapacity)
                .IsRequired()
                .HasColumnType("INT");

            // Relacionamento com Parking
            builder.HasOne(g => g.Parking)
                .WithMany(p => p.Gateways)
                .HasForeignKey(g => g.ParkingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}