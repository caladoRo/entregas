using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class EkipsContext : DbContext
    {
        public EkipsContext()
        {
        }

        public EkipsContext(DbContextOptions<EkipsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog=M_Ekips;User Id=sa;Pwd=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.HasKey(e => e.IdCargo);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento);

                entity.Property(e => e.Nome)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Funcionarios>(entity =>
            {
                entity.HasKey(e => e.IdFuncionario);

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("UQ__Funciona__5B65BF96C840FF59")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Salario).HasColumnType("money");

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdCargo)
                    .HasConstraintName("FK__Funcionar__IdCar__619B8048");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Funcionar__IdDep__60A75C0F");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithOne(p => p.Funcionarios)
                    .HasForeignKey<Funcionarios>(d => d.IdUsuario)
                    .HasConstraintName("FK__Funcionar__IdUsu__628FA481");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Permissao)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
