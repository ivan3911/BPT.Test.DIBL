using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BPT.Test.DIBL.API.Models
{
    public partial class PagaTodoDbContext : DbContext
    {
        public PagaTodoDbContext()
        {
        }

        public PagaTodoDbContext(DbContextOptions<PagaTodoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignacione> Asignaciones { get; set; }
        public virtual DbSet<AsignacionesEstudiante> AsignacionesEstudiantes { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.\\SQL2019EXPRESS;Initial Catalog=PagaTodoDB;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Asignacione>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AsignacionesEstudiante>(entity =>
            {
                entity.ToTable("AsignacionesEstudiante");

                entity.HasOne(d => d.IdAsignacionNavigation)
                    .WithMany(p => p.AsignacionesEstudiantes)
                    .HasForeignKey(d => d.IdAsignacion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AsignacionesEstudiante_Asignaciones");

                entity.HasOne(d => d.IdEstudianteNavigation)
                    .WithMany(p => p.AsignacionesEstudiantes)
                    .HasForeignKey(d => d.IdEstudiante)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AsignacionesEstudiante_Estudiantes");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
