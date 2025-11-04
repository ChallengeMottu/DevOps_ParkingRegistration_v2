using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseSystem.Domain.Entities;

namespace PulseSystem.Infraestructure.Persistence.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees"); // sem schema, pois o Azure SQL usa dbo por padrão

            // Chave primária
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            // Propriedades
            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("VARCHAR(150)");

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(e => e.Role)
                .HasColumnName("Role")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)");
        }
    }
}