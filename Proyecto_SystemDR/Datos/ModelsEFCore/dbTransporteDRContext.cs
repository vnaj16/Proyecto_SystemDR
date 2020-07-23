using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entidades;

namespace Datos.ModelsEFCore
{
    public partial class dbTransporteDRContext : DbContext
    {
        public dbTransporteDRContext()
        {
        }

        public dbTransporteDRContext(DbContextOptions<dbTransporteDRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Conductor> Conductor { get; set; }
        public virtual DbSet<CuentasXrendir> CuentasXrendir { get; set; }
        public virtual DbSet<DocumentoViaje> DocumentoViaje { get; set; }
        public virtual DbSet<Flete> Flete { get; set; }
        public virtual DbSet<Gasto> Gasto { get; set; }
        public virtual DbSet<GastoV> GastoV { get; set; }
        public virtual DbSet<Historial> Historial { get; set; }
        public virtual DbSet<Mercaderia> Mercaderia { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Resumen> Resumen { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }
        public virtual DbSet<UnidadVehicular> UnidadVehicular { get; set; }
        public virtual DbSet<Viaje> Viaje { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                var cs = ConfigurationManager.ConnectionStrings["dbTransporteDRContext"].ConnectionString;
                optionsBuilder.UseSqlServer(cs);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Ruc);

                entity.Property(e => e.Ruc)
                    .HasColumnName("RUC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DniRl)
                    .HasColumnName("DNI_RL")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasColumnName("Razon Social")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniRlNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.DniRl)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Cliente_Persona");
            });

            modelBuilder.Entity<Conductor>(entity =>
            {
                entity.HasKey(e => e.Dni);

                entity.Property(e => e.Dni)
                    .HasColumnName("DNI")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Brevete)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha Inicio")
                    .HasColumnType("date");

                entity.Property(e => e.GradoInstruccion)
                    .HasColumnName("Grado Instruccion")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LugarNac)
                    .HasColumnName("Lugar Nac")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Personalidad)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniNavigation)
                    .WithOne(p => p.Conductor)
                    .HasForeignKey<Conductor>(d => d.Dni)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conductor_Persona");
            });

            modelBuilder.Entity<CuentasXrendir>(entity =>
            {
                entity.ToTable("CuentasXRendir");

                entity.HasIndex(e => e.IdViaje)
                    .HasName("UQ__CuentasX__DBBCA1454E8AAD33")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FleteEntregadoA)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Lugar)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithOne(p => p.CuentasXrendir)
                    .HasForeignKey<CuentasXrendir>(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CuentasXR__ID_Vi__5F7E2DAC");
            });

            modelBuilder.Entity<DocumentoViaje>(entity =>
            {
                entity.HasIndex(e => e.IdViaje)
                    .HasName("UQ__Document__DBBCA14571168B8B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Factura)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Grr)
                    .HasColumnName("GRR")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Grt)
                    .HasColumnName("GRT")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Ruc)
                    .HasColumnName("RUC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithOne(p => p.DocumentoViaje)
                    .HasForeignKey<DocumentoViaje>(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Documento__ID_Vi__73852659");

                entity.HasOne(d => d.RucNavigation)
                    .WithMany(p => p.DocumentoViaje)
                    .HasForeignKey(d => d.Ruc)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__DocumentoVi__RUC__72910220");
            });

            modelBuilder.Entity<Flete>(entity =>
            {
                entity.HasIndex(e => e.IdViaje)
                    .HasName("UQ__Flete__DBBCA1454AD19EEB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithOne(p => p.Flete)
                    .HasForeignKey<Flete>(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Flete__ID_Viaje__57DD0BE4");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Categoria)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.Gasto)
                    .HasForeignKey(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Gasto__ID_Viaje__4D5F7D71");
            });

            modelBuilder.Entity<GastoV>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Documento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Km).HasColumnName("KM");

                entity.Property(e => e.RazonSocial)
                    .HasColumnName("Razon_Social")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.GastoV)
                    .HasForeignKey(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__GastoV__ID_Viaje__503BEA1C");
            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DniConductor)
                    .HasColumnName("DNI_Conductor")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Eventualidad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdUnidad)
                    .HasColumnName("ID_Unidad")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Lugar)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniConductorNavigation)
                    .WithMany(p => p.Historial)
                    .HasForeignKey(d => d.DniConductor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Historial_Conductor");

                entity.HasOne(d => d.IdUnidadNavigation)
                    .WithMany(p => p.Historial)
                    .HasForeignKey(d => d.IdUnidad)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Historial_Unidad Vehicular");
            });

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.HasIndex(e => e.IdViaje)
                    .HasName("UQ__Mercader__DBBCA145CA1D4D5F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bultos)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Producto)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Unidad)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithOne(p => p.Mercaderia)
                    .HasForeignKey<Mercaderia>(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Mercaderi__ID_Vi__540C7B00");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Dni);

                entity.Property(e => e.Dni)
                    .HasColumnName("DNI")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNac)
                    .HasColumnName("Fecha_Nac")
                    .HasColumnType("date");

                entity.Property(e => e.Nacionalidad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .HasComment("cli: cliente, pro: proveedor, con: conductor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Ruc);

                entity.Property(e => e.Ruc)
                    .HasColumnName("RUC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DniRl)
                    .HasColumnName("DNI_RL")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Productos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasColumnName("Razon Social")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniRlNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.DniRl)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Proveedor_Persona");
            });

            modelBuilder.Entity<Resumen>(entity =>
            {
                entity.HasIndex(e => e.IdViaje)
                    .HasName("UQ__Resumen__DBBCA145762AA496")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BolsaViajeD)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DeudaAntFavorD)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IdViaje)
                    .HasColumnName("ID_Viaje")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SaldoFavorD)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithOne(p => p.Resumen)
                    .HasForeignKey<Resumen>(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Resumen__ID_Viaj__5BAD9CC8");
            });

            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Numero);

                entity.Property(e => e.Numero)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Dni)
                    .HasColumnName("DNI")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniNavigation)
                    .WithMany(p => p.Telefono)
                    .HasForeignKey(d => d.Dni)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Telefono_Persona");
            });

            modelBuilder.Entity<UnidadVehicular>(entity =>
            {
                entity.HasKey(e => e.Placa);

                entity.ToTable("Unidad Vehicular");

                entity.Property(e => e.Placa)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SerieChasis)
                    .HasColumnName("Serie Chasis")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.YFabricacion).HasColumnName("Y_Fabricacion");
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DniConductor)
                    .HasColumnName("DNI_Conductor")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Encargado)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha_Inicio")
                    .HasColumnType("date");

                entity.Property(e => e.FechaLlegada)
                    .HasColumnName("Fecha_Llegada")
                    .HasColumnType("date");

                entity.Property(e => e.FechaSalida)
                    .HasColumnName("Fecha_Salida")
                    .HasColumnType("date");

                entity.Property(e => e.KmDestino).HasColumnName("KM_Destino");

                entity.Property(e => e.KmOrigen).HasColumnName("KM_Origen");

                entity.Property(e => e.LugarDestino)
                    .HasColumnName("Lugar_Destino")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LugarOrigen)
                    .HasColumnName("Lugar_Origen")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Nota)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlacaVehiculo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.DniConductorNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.DniConductor)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Viaje__DNI_Condu__4A8310C6");

                entity.HasOne(d => d.PlacaVehiculoNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.PlacaVehiculo)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Viaje__PlacaVehi__498EEC8D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
