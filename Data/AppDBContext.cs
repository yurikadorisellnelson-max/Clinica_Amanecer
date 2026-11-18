using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;
using ClinicaAmanecer.Models;

namespace ClinicaAmanecer.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        // 5 Tablas de Seguridad
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Permiso> Permisos => Set<Permiso>();
        public DbSet<RolPermiso> RolPermisos => Set<RolPermiso>();
        public DbSet<UsuarioPermiso> UsuarioPermisos => Set<UsuarioPermiso>();

        // 11 Tablas Maestras y Catálogos
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<CatalogoAlergia> CatalogoAlergias => Set<CatalogoAlergia>();
        public DbSet<Especialidad> Especialidades => Set<Especialidad>();
        public DbSet<Consultorio> Consultorios => Set<Consultorio>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Cita> Citas => Set<Cita>();
        public DbSet<ConsultaMedica> ConsultasMedicas => Set<ConsultaMedica>();
        public DbSet<CatalogoDiagnosticoCIE10> CatalogoDiagnosticosCIE10 => Set<CatalogoDiagnosticoCIE10>();
        public DbSet<CatalogoMedicamento> CatalogoMedicamentos => Set<CatalogoMedicamento>();
        public DbSet<ServicioClinico> ServiciosClinicos => Set<ServicioClinico>();
        public DbSet<Factura> Facturas => Set<Factura>();

        // 4 Tablas Detalle
        public DbSet<DetallePaciente_Alergia> DetallePaciente_Alergias => Set<DetallePaciente_Alergia>();
        public DbSet<DetalleConsulta_Diagnostico> DetalleConsulta_Diagnosticos => Set<DetalleConsulta_Diagnostico>();
        public DbSet<DetalleReceta_Medicamento> DetalleReceta_Medicamentos => Set<DetalleReceta_Medicamento>();
        public DbSet<DetalleFactura_Servicio> DetalleFactura_Servicios => Set<DetalleFactura_Servicio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices Únicos
            modelBuilder.Entity<Role>().HasIndex(r => r.NombreRol).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Permiso>().HasIndex(p => p.CodigoPermiso).IsUnique();
            modelBuilder.Entity<RolPermiso>().HasIndex(rp => new { rp.Roles_RoleID, rp.Permisos_PermisoID }).IsUnique();
            modelBuilder.Entity<UsuarioPermiso>().HasIndex(up => new { up.Usuarios_UsuarioID, up.Permisos_PermisoID }).IsUnique();

            modelBuilder.Entity<Paciente>().HasIndex(p => p.NumeroExpediente).IsUnique();
            modelBuilder.Entity<Paciente>().HasIndex(p => p.Cedula).IsUnique();
            modelBuilder.Entity<Consultorio>().HasIndex(c => c.CodigoSala).IsUnique();
            modelBuilder.Entity<Medico>().HasIndex(m => m.CodigoMINSA).IsUnique();
            modelBuilder.Entity<CatalogoDiagnosticoCIE10>().HasIndex(d => d.CodigoCIE10).IsUnique();
            modelBuilder.Entity<ServicioClinico>().HasIndex(s => s.CodigoServicio).IsUnique();
            modelBuilder.Entity<Factura>().HasIndex(f => f.NumeroFactura).IsUnique();

            // Relaciones de Seguridad
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.Roles_RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.Roles_RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.Permisos_PermisoID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPermisos)
                .HasForeignKey(up => up.Usuarios_UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Permiso)
                .WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(up => up.Permisos_PermisoID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relaciones Operativas (Restrict para evitar ciclos de cascada en SQL Server)
            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Usuario)
                .WithOne(u => u.Medico)
                .HasForeignKey<Medico>(m => m.Usuarios_UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.Especialidades_EspecialidadID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.Pacientes_PacienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Citas)
                .HasForeignKey(c => c.Medicos_MedicoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Consultorio)
                .WithMany(co => co.Citas)
                .HasForeignKey(c => c.Consultorios_ConsultorioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsultaMedica>()
                .HasOne(cm => cm.Cita)
                .WithOne(c => c.ConsultaMedica)
                .HasForeignKey<ConsultaMedica>(cm => cm.Citas_CitaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.UsuarioCajero)
                .WithMany(u => u.FacturasEmitidas)
                .HasForeignKey(f => f.Usuarios_UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cita)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.Citas_CitaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Paciente)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.Pacientes_PacienteID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones Maestro-Detalle
            modelBuilder.Entity<DetallePaciente_Alergia>()
                .HasOne(d => d.Paciente)
                .WithMany(p => p.DetalleAlergias)
                .HasForeignKey(d => d.Pacientes_PacienteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetallePaciente_Alergia>()
                .HasOne(d => d.CatalogoAlergia)
                .WithMany(ca => ca.DetallePacientes)
                .HasForeignKey(d => d.CatalogoAlergias_AlergiaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleConsulta_Diagnostico>()
                .HasOne(d => d.ConsultaMedica)
                .WithMany(cm => cm.DetalleDiagnosticos)
                .HasForeignKey(d => d.ConsultasMedicas_ConsultasID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleConsulta_Diagnostico>()
                .HasOne(d => d.DiagnosticoCIE10)
                .WithMany(cie => cie.DetalleConsultas)
                .HasForeignKey(d => d.CatalogoDiagnostico_DiagnosticoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleReceta_Medicamento>()
                .HasOne(d => d.ConsultaMedica)
                .WithMany(cm => cm.DetalleRecetas)
                .HasForeignKey(d => d.ConsultasMedicas_ConsultasID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleReceta_Medicamento>()
                .HasOne(d => d.Medicamento)
                .WithMany(m => m.DetalleRecetas)
                .HasForeignKey(d => d.CatalogoMedicamentos_MedicamentoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleFactura_Servicio>()
                .HasOne(d => d.Factura)
                .WithMany(f => f.DetalleServicios)
                .HasForeignKey(d => d.Facturas_FacturaID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleFactura_Servicio>()
                .HasOne(d => d.ServicioClinico)
                .WithMany(s => s.DetalleFacturas)
                .HasForeignKey(d => d.ServicioClinico_ServicioID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}