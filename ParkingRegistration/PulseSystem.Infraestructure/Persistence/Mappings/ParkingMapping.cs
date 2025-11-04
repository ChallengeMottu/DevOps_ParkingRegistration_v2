using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseSystem.Domain.Entities;

namespace PulseSystem.Infraestructure.Persistence.Mappings
{
    public class ParkingMapping : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.ToTable("Parkings"); // boas práticas: nomes sem caixa alta fixa

            // Chave primária
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("Id");

            // Propriedades principais
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150)
                   .HasColumnType("VARCHAR(150)");

            builder.Property(p => p.AvailableArea)
                   .IsRequired()
                   .HasColumnType("FLOAT"); // ou DECIMAL(10,2), se for valor numérico fixo

            builder.Property(p => p.Capacity)
                   .HasColumnName("Capacity")
                   .IsRequired()
                   .HasColumnType("INT");

            builder.Property(p => p.RegisterDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()"); // substitui SYSDATE

            builder.Property(p => p.structurePlan)
                   .HasColumnName("StructurePlan")
                   .IsRequired();

            builder.Property(p => p.floorPlan)
                   .HasColumnName("FloorPlan")
                   .IsRequired();

            // Propriedade complexa (Owned Type)
            builder.OwnsOne(x => x.Location, address =>
            {
                address.Property(e => e.Street)
                       .HasColumnName("Street")
                       .HasMaxLength(100)
                       .IsRequired();

                address.Property(e => e.Complement)
                       .HasColumnName("Complement")
                       .HasMaxLength(50);

                address.Property(e => e.Neighborhood)
                       .HasColumnName("Neighborhood")
                       .HasMaxLength(100)
                       .IsRequired();

                address.Property(e => e.City)
                       .HasColumnName("City")
                       .HasMaxLength(100)
                       .IsRequired();

                address.Property(e => e.State)
                       .HasColumnName("State")
                       .HasMaxLength(50)
                       .IsRequired();

                address.Property(e => e.Cep)
                       .HasColumnName("Cep")
                       .HasMaxLength(9)
                       .IsRequired();
            });

            // Relacionamentos
            builder.HasMany(p => p.Zones)
                   .WithOne(z => z.Parking)
                   .HasForeignKey(z => z.ParkingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Gateways)
                   .WithOne(g => g.Parking)
                   .HasForeignKey(g => g.ParkingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
