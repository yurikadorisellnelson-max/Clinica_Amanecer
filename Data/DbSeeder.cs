using ClinicaAmanecer.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaAmanecer.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(AppDBContext context)
        {
            if (context.Database.GetMigrations().Any())
                await context.Database.MigrateAsync();
            else
                await context.Database.EnsureCreatedAsync();

            await using var transaction = await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            await SeedRolesAsync(context);
            await SeedPermissionsAsync(context);
            await SeedRolePermissionsAsync(context);
            await SeedUsersAsync(context);
            await SeedCatalogosClinicosAsync(context);
            await SeedDatosOperativosDemoAsync(context);

            await transaction.CommitAsync();
        }

        private static async Task SeedRolesAsync(AppDBContext context)
        {
            if (await context.Roles.AnyAsync()) return;

            context.Roles.AddRange(
                new Role { NombreRol = "Administrador", Descripcion = "Acceso total a los 6 módulos y matriz de seguridad" },
                new Role { NombreRol = "Médico", Descripcion = "Acceso a Dashboard, Pacientes, Citas y Consultas/Expediente" },
                new Role { NombreRol = "Recepcionista", Descripcion = "Acceso a Dashboard, Pacientes y Citas Médicas" },
                new Role { NombreRol = "Enfermería", Descripcion = "Acceso a Dashboard, Pacientes y Consultas/Signos Vitales" },
                new Role { NombreRol = "Cajero", Descripcion = "Acceso a Dashboard, Pacientes y Facturación/Caja" }
            );
            await context.SaveChangesAsync();
        }

        private static async Task SeedPermissionsAsync(AppDBContext context)
        {
            if (await context.Permisos.AnyAsync()) return;

            context.Permisos.AddRange(
                // 1. Dashboard
                new Permiso { CodigoPermiso = "dashboard.ver", Modulo = "Dashboard", NombreVisible = "Ver Dashboard" },

                // 2. Pacientes
                new Permiso { CodigoPermiso = "paciente.ver", Modulo = "Pacientes", NombreVisible = "Ver Pacientes" },
                new Permiso { CodigoPermiso = "paciente.crear", Modulo = "Pacientes", NombreVisible = "Registrar Pacientes" },
                new Permiso { CodigoPermiso = "paciente.editar", Modulo = "Pacientes", NombreVisible = "Editar Pacientes" },

                // 3. Citas Médicas
                new Permiso { CodigoPermiso = "cita.ver", Modulo = "Citas Médicas", NombreVisible = "Ver Citas Médicas" },
                new Permiso { CodigoPermiso = "cita.crear", Modulo = "Citas Médicas", NombreVisible = "Programar Citas" },
                new Permiso { CodigoPermiso = "cita.anular", Modulo = "Citas Médicas", NombreVisible = "Anular o Reprogramar Citas" },

                // 4. Consultas y Expediente
                new Permiso { CodigoPermiso = "consulta.ver", Modulo = "Consultas y Expediente", NombreVisible = "Ver Consultas y Expediente" },
                new Permiso { CodigoPermiso = "consulta.crear", Modulo = "Consultas y Expediente", NombreVisible = "Registrar Consultas y Recetas" },

                // 5. Facturación/Caja
                new Permiso { CodigoPermiso = "factura.ver", Modulo = "Facturación/Caja", NombreVisible = "Ver Caja y Facturación" },
                new Permiso { CodigoPermiso = "factura.crear", Modulo = "Facturación/Caja", NombreVisible = "Emitir Facturas" },
                new Permiso { CodigoPermiso = "factura.anular", Modulo = "Facturación/Caja", NombreVisible = "Anular Facturas" },

                // 6. Usuarios y Permisos
                new Permiso { CodigoPermiso = "seguridad.ver", Modulo = "Usuarios y Permisos", NombreVisible = "Ver Usuarios y Permisos" },
                new Permiso { CodigoPermiso = "seguridad.administrar", Modulo = "Usuarios y Permisos", NombreVisible = "Administrar Roles y Permisos" }
            );
            await context.SaveChangesAsync();
        }

        private static async Task SeedRolePermissionsAsync(AppDBContext context)
        {
            if (await context.RolPermisos.AnyAsync()) return;

            var roles = await context.Roles.ToListAsync();
            var permisos = await context.Permisos.ToListAsync();

            int GetRolId(string nombre) => roles.First(r => r.NombreRol == nombre).RoleID;

            void AsignarModulos(int roleId, params string[] modulosPermitidos)
            {
                var permisosRol = permisos.Where(p => modulosPermitidos.Contains(p.Modulo));
                foreach (var p in permisosRol)
                {
                    context.RolPermisos.Add(new RolPermiso
                    {
                        Roles_RoleID = roleId,
                        Permisos_PermisoID = p.PermisoID
                    });
                }
            }

            AsignarModulos(GetRolId("Administrador"), "Dashboard", "Pacientes", "Citas Médicas", "Consultas y Expediente", "Facturación/Caja", "Usuarios y Permisos");
            AsignarModulos(GetRolId("Médico"), "Dashboard", "Pacientes", "Citas Médicas", "Consultas y Expediente");
            AsignarModulos(GetRolId("Recepcionista"), "Dashboard", "Pacientes", "Citas Médicas");
            AsignarModulos(GetRolId("Enfermería"), "Dashboard", "Pacientes", "Consultas y Expediente");
            AsignarModulos(GetRolId("Cajero"), "Dashboard", "Pacientes", "Facturación/Caja");

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(AppDBContext context)
        {
            if (await context.Usuarios.AnyAsync()) return;

            var roles = await context.Roles.ToListAsync();
            int GetRolId(string nombre) => roles.First(r => r.NombreRol == nombre).RoleID;

            string defaultPass = BCrypt.Net.BCrypt.EnhancedHashPassword("Clinica2026*");

            context.Usuarios.AddRange(
                new Usuario
                {
                    UserName = "admin",
                    NombreCompleto = "Admin General",
                    Email = "admin@clinicaamanecer.com",
                    PasswordHash = defaultPass,
                    Roles_RoleID = GetRolId("Administrador"),
                    Activo = true
                },
                new Usuario
                {
                    UserName = "espinoza",
                    NombreCompleto = "Dra. Emely Espinoza",
                    Email = "emely.espinoza@clinicaamanecer.com",
                    PasswordHash = defaultPass,
                    Roles_RoleID = GetRolId("Médico"),
                    Activo = true
                },
                new Usuario
                {
                    UserName = "nelson",
                    NombreCompleto = "Dra. Yurika Nelson",
                    Email = "yurika.nelson@clinicaamanecer.com",
                    PasswordHash = defaultPass,
                    Roles_RoleID = GetRolId("Médico"),
                    Activo = true
                },
                new Usuario
                {
                    UserName = "gomez",
                    NombreCompleto = "Lic. Ana Gómez",
                    Email = "ana.gomez@clinicaamanecer.com",
                    PasswordHash = defaultPass,
                    Roles_RoleID = GetRolId("Recepcionista"),
                    Activo = true
                },
                new Usuario
                {
                    UserName = "cajero1",
                    NombreCompleto = "Lic. Roberto Caja",
                    Email = "caja@clinicaamanecer.com",
                    PasswordHash = defaultPass,
                    Roles_RoleID = GetRolId("Cajero"),
                    Activo = true
                }
            );
            await context.SaveChangesAsync();
        }

        private static async Task SeedCatalogosClinicosAsync(AppDBContext context)
        {
            if (!await context.CatalogoAlergias.AnyAsync())
            {
                context.CatalogoAlergias.AddRange(
                    new CatalogoAlergia { NombreAlergeno = "Penicilina", CategoriaAlergia = "Medicamento" },
                    new CatalogoAlergia { NombreAlergeno = "Aspirina (AINEs)", CategoriaAlergia = "Medicamento" },
                    new CatalogoAlergia { NombreAlergeno = "Polen", CategoriaAlergia = "Ambiental" },
                    new CatalogoAlergia { NombreAlergeno = "Mariscos", CategoriaAlergia = "Alimentaria" }
                );
            }

            if (!await context.Especialidades.AnyAsync())
            {
                context.Especialidades.AddRange(
                    new Especialidad { NombreEspecialidad = "Medicina General", DuracionCitaMin = 30, Estado = true },
                    new Especialidad { NombreEspecialidad = "Cirugía General", DuracionCitaMin = 45, Estado = true },
                    new Especialidad { NombreEspecialidad = "Ginecología", DuracionCitaMin = 30, Estado = true }
                );
            }

            if (!await context.Consultorios.AnyAsync())
            {
                context.Consultorios.AddRange(
                    new Consultorio { CodigoSala = "CONS-A1", NombreSala = "Consultorio A1", Piso = 1, Estado = true },
                    new Consultorio { CodigoSala = "CONS-B2", NombreSala = "Consultorio B2", Piso = 2, Estado = true },
                    new Consultorio { CodigoSala = "CONS-C1", NombreSala = "Consultorio C1", Piso = 1, Estado = true }
                );
            }

            if (!await context.CatalogoDiagnosticosCIE10.AnyAsync())
            {
                context.CatalogoDiagnosticosCIE10.AddRange(
                    new CatalogoDiagnosticoCIE10 { CodigoCIE10 = "J02.9", DescripcionCIE10 = "Faringitis aguda, no especificada" },
                    new CatalogoDiagnosticoCIE10 { CodigoCIE10 = "I10", DescripcionCIE10 = "Hipertensión esencial (primaria)" },
                    new CatalogoDiagnosticoCIE10 { CodigoCIE10 = "E11.9", DescripcionCIE10 = "Diabetes mellitus tipo 2 sin complicaciones" },
                    new CatalogoDiagnosticoCIE10 { CodigoCIE10 = "A09", DescripcionCIE10 = "Diarrea y gastroenteritis de presunto origen infeccioso" }
                );
            }

            if (!await context.CatalogoMedicamentos.AnyAsync())
            {
                context.CatalogoMedicamentos.AddRange(
                    new CatalogoMedicamento { NombreGenerico = "Azitromicina", Concentracion = "500 mg", Presentacion = "Tableta" },
                    new CatalogoMedicamento { NombreGenerico = "Amoxicilina (Penicilina)", Concentracion = "500 mg", Presentacion = "Cápsula" },
                    new CatalogoMedicamento { NombreGenerico = "Paracetamol", Concentracion = "500 mg", Presentacion = "Tableta" },
                    new CatalogoMedicamento { NombreGenerico = "Losartán", Concentracion = "50 mg", Presentacion = "Tableta" }
                );
            }

            if (!await context.ServiciosClinicos.AnyAsync())
            {
                context.ServiciosClinicos.AddRange(
                    new ServicioClinico { CodigoServicio = "SRV-01", NombreServicio = "Consulta Médica General", PrecioUnitario = 25.00m, Activo = true },
                    new ServicioClinico { CodigoServicio = "SRV-02", NombreServicio = "Consulta Especializada", PrecioUnitario = 45.00m, Activo = true },
                    new ServicioClinico { CodigoServicio = "SRV-05", NombreServicio = "Medicamento básico", PrecioUnitario = 30.00m, Activo = true }
                );
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedDatosOperativosDemoAsync(AppDBContext context)
        {
            if (await context.Medicos.AnyAsync()) return;

            var draEmelyUser = await context.Usuarios.FirstAsync(u => u.UserName == "espinoza");
            var draYurikaUser = await context.Usuarios.FirstAsync(u => u.UserName == "nelson");
            var espGeneral = await context.Especialidades.FirstAsync(e => e.NombreEspecialidad == "Medicina General");
            var espCirugia = await context.Especialidades.FirstAsync(e => e.NombreEspecialidad == "Cirugía General");

            context.Medicos.AddRange(
                new Medico
                {
                    CodigoMINSA = "MINSA-2026-01",
                    Nombres = "Emely Silena",
                    Apellidos = "Espinoza Weekly",
                    Telefono = "8888-1111",
                    TurnoHorario = "09:00-14:00",
                    Estado = true,
                    Usuarios_UsuarioID = draEmelyUser.UsuarioID,
                    Especialidades_EspecialidadID = espGeneral.EspecialidadID
                },
                new Medico
                {
                    CodigoMINSA = "MINSA-2026-02",
                    Nombres = "Yurika Dorisel",
                    Apellidos = "Nelson Hodgson",
                    Telefono = "8888-2222",
                    TurnoHorario = "08:00-16:00",
                    Estado = true,
                    Usuarios_UsuarioID = draYurikaUser.UsuarioID,
                    Especialidades_EspecialidadID = espCirugia.EspecialidadID
                }
            );

            var pacMaria = new Paciente
            {
                NumeroExpediente = "EXP-2026-014",
                Cedula = "601-150599-1000A",
                Nombres = "María",
                Apellidos = "López",
                FechaNacimiento = new DateTime(1999, 5, 15),
                Sexo = "F",
                TipoSangre = "O+",
                Telefono = "8888-9999",
                Direccion = "Barrio Central, Bluefields",
                Estado = true
            };
            context.Pacientes.Add(pacMaria);
            await context.SaveChangesAsync();

            var penicilina = await context.CatalogoAlergias.FirstAsync(a => a.NombreAlergeno == "Penicilina");
            context.DetallePaciente_Alergias.Add(new DetallePaciente_Alergia
            {
                Pacientes_PacienteID = pacMaria.PacienteID,
                CatalogoAlergias_AlergiaID = penicilina.AlergiaID,
                NivelSeveridad = "Severa",
                NotasReaccion = "Erupción Cutánea"
            });

            await context.SaveChangesAsync();
        }
    }
}