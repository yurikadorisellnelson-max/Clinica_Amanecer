using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaAmanecer.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicialClinicaAmanecer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogoAlergias",
                columns: table => new
                {
                    AlergiaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreAlergeno = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CategoriaAlergia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoAlergias", x => x.AlergiaID);
                });

            migrationBuilder.CreateTable(
                name: "CatalogoDiagnosticosCIE10",
                columns: table => new
                {
                    DiagnosticoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCIE10 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescripcionCIE10 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoDiagnosticosCIE10", x => x.DiagnosticoID);
                });

            migrationBuilder.CreateTable(
                name: "CatalogoMedicamentos",
                columns: table => new
                {
                    MedicamentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGenerico = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Concentracion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Presentacion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoMedicamentos", x => x.MedicamentoID);
                });

            migrationBuilder.CreateTable(
                name: "Consultorios",
                columns: table => new
                {
                    ConsultorioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoSala = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NombreSala = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorios", x => x.ConsultorioID);
                });

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    EspecialidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DuracionCitaMin = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.EspecialidadID);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroExpediente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TipoSangre = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteID);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPermiso = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Modulo = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    NombreVisible = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosClinicos",
                columns: table => new
                {
                    ServicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoServicio = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    NombreServicio = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosClinicos", x => x.ServicioID);
                });

            migrationBuilder.CreateTable(
                name: "DetallePaciente_Alergias",
                columns: table => new
                {
                    DetalleAlergiaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NivelSeveridad = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    NotasReaccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CatalogoAlergias_AlergiaID = table.Column<int>(type: "int", nullable: false),
                    Pacientes_PacienteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePaciente_Alergias", x => x.DetalleAlergiaID);
                    table.ForeignKey(
                        name: "FK_DetallePaciente_Alergias_CatalogoAlergias_CatalogoAlergias_AlergiaID",
                        column: x => x.CatalogoAlergias_AlergiaID,
                        principalTable: "CatalogoAlergias",
                        principalColumn: "AlergiaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallePaciente_Alergias_Pacientes_Pacientes_PacienteID",
                        column: x => x.Pacientes_PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    RolPermisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Roles_RoleID = table.Column<int>(type: "int", nullable: false),
                    Permisos_PermisoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => x.RolPermisoID);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_Permisos_PermisoID",
                        column: x => x.Permisos_PermisoID,
                        principalTable: "Permisos",
                        principalColumn: "PermisoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_Roles_RoleID",
                        column: x => x.Roles_RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Roles_RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Roles_RoleID",
                        column: x => x.Roles_RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    MedicoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoMINSA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TurnoHorario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Usuarios_UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Especialidades_EspecialidadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoID);
                    table.ForeignKey(
                        name: "FK_Medicos_Especialidades_Especialidades_EspecialidadID",
                        column: x => x.Especialidades_EspecialidadID,
                        principalTable: "Especialidades",
                        principalColumn: "EspecialidadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicos_Usuarios_Usuarios_UsuarioID",
                        column: x => x.Usuarios_UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermisos",
                columns: table => new
                {
                    UsuarioPermisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsPermitido = table.Column<bool>(type: "bit", nullable: false),
                    MotivoCambio = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Permisos_PermisoID = table.Column<int>(type: "int", nullable: false),
                    Usuarios_UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermisos", x => x.UsuarioPermisoID);
                    table.ForeignKey(
                        name: "FK_UsuarioPermisos_Permisos_Permisos_PermisoID",
                        column: x => x.Permisos_PermisoID,
                        principalTable: "Permisos",
                        principalColumn: "PermisoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPermisos_Usuarios_Usuarios_UsuarioID",
                        column: x => x.Usuarios_UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<TimeSpan>(type: "time", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EstadoCita = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Consultorios_ConsultorioID = table.Column<int>(type: "int", nullable: false),
                    Pacientes_PacienteID = table.Column<int>(type: "int", nullable: false),
                    Medicos_MedicoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitaID);
                    table.ForeignKey(
                        name: "FK_Citas_Consultorios_Consultorios_ConsultorioID",
                        column: x => x.Consultorios_ConsultorioID,
                        principalTable: "Consultorios",
                        principalColumn: "ConsultorioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Medicos_Medicos_MedicoID",
                        column: x => x.Medicos_MedicoID,
                        principalTable: "Medicos",
                        principalColumn: "MedicoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Pacientes_Pacientes_PacienteID",
                        column: x => x.Pacientes_PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultasMedicas",
                columns: table => new
                {
                    ConsultasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAtencion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PresionArterial = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    TemperaturaCorporal = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    PesoKG = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TallaCM = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Anamnesis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExploracionFisica = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PlanTratamiento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Citas_CitaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultasMedicas", x => x.ConsultasID);
                    table.ForeignKey(
                        name: "FK_ConsultasMedicas_Citas_Citas_CitaID",
                        column: x => x.Citas_CitaID,
                        principalTable: "Citas",
                        principalColumn: "CitaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroFactura = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    EstadoFactura = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Usuarios_UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Citas_CitaID = table.Column<int>(type: "int", nullable: false),
                    Pacientes_PacienteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaID);
                    table.ForeignKey(
                        name: "FK_Facturas_Citas_Citas_CitaID",
                        column: x => x.Citas_CitaID,
                        principalTable: "Citas",
                        principalColumn: "CitaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Pacientes_Pacientes_PacienteID",
                        column: x => x.Pacientes_PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_Usuarios_UsuarioID",
                        column: x => x.Usuarios_UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleConsulta_Diagnosticos",
                columns: table => new
                {
                    DetalleDiagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCriterio = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CatalogoDiagnostico_DiagnosticoID = table.Column<int>(type: "int", nullable: false),
                    ConsultasMedicas_ConsultasID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleConsulta_Diagnosticos", x => x.DetalleDiagID);
                    table.ForeignKey(
                        name: "FK_DetalleConsulta_Diagnosticos_CatalogoDiagnosticosCIE10_CatalogoDiagnostico_DiagnosticoID",
                        column: x => x.CatalogoDiagnostico_DiagnosticoID,
                        principalTable: "CatalogoDiagnosticosCIE10",
                        principalColumn: "DiagnosticoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleConsulta_Diagnosticos_ConsultasMedicas_ConsultasMedicas_ConsultasID",
                        column: x => x.ConsultasMedicas_ConsultasID,
                        principalTable: "ConsultasMedicas",
                        principalColumn: "ConsultasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleReceta_Medicamentos",
                columns: table => new
                {
                    DetalleRecetaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosis = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Frecuencia = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DuracionDias = table.Column<int>(type: "int", nullable: false),
                    ViaAdministracion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CatalogoMedicamentos_MedicamentoID = table.Column<int>(type: "int", nullable: false),
                    ConsultasMedicas_ConsultasID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleReceta_Medicamentos", x => x.DetalleRecetaID);
                    table.ForeignKey(
                        name: "FK_DetalleReceta_Medicamentos_CatalogoMedicamentos_CatalogoMedicamentos_MedicamentoID",
                        column: x => x.CatalogoMedicamentos_MedicamentoID,
                        principalTable: "CatalogoMedicamentos",
                        principalColumn: "MedicamentoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleReceta_Medicamentos_ConsultasMedicas_ConsultasMedicas_ConsultasID",
                        column: x => x.ConsultasMedicas_ConsultasID,
                        principalTable: "ConsultasMedicas",
                        principalColumn: "ConsultasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura_Servicios",
                columns: table => new
                {
                    DetalleFacturaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotalLinea = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Facturas_FacturaID = table.Column<int>(type: "int", nullable: false),
                    ServicioClinico_ServicioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura_Servicios", x => x.DetalleFacturaID);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Servicios_Facturas_Facturas_FacturaID",
                        column: x => x.Facturas_FacturaID,
                        principalTable: "Facturas",
                        principalColumn: "FacturaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Servicios_ServiciosClinicos_ServicioClinico_ServicioID",
                        column: x => x.ServicioClinico_ServicioID,
                        principalTable: "ServiciosClinicos",
                        principalColumn: "ServicioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoDiagnosticosCIE10_CodigoCIE10",
                table: "CatalogoDiagnosticosCIE10",
                column: "CodigoCIE10",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_Consultorios_ConsultorioID",
                table: "Citas",
                column: "Consultorios_ConsultorioID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_Medicos_MedicoID",
                table: "Citas",
                column: "Medicos_MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_Pacientes_PacienteID",
                table: "Citas",
                column: "Pacientes_PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultasMedicas_Citas_CitaID",
                table: "ConsultasMedicas",
                column: "Citas_CitaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultorios_CodigoSala",
                table: "Consultorios",
                column: "CodigoSala",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleConsulta_Diagnosticos_CatalogoDiagnostico_DiagnosticoID",
                table: "DetalleConsulta_Diagnosticos",
                column: "CatalogoDiagnostico_DiagnosticoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleConsulta_Diagnosticos_ConsultasMedicas_ConsultasID",
                table: "DetalleConsulta_Diagnosticos",
                column: "ConsultasMedicas_ConsultasID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_Servicios_Facturas_FacturaID",
                table: "DetalleFactura_Servicios",
                column: "Facturas_FacturaID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_Servicios_ServicioClinico_ServicioID",
                table: "DetalleFactura_Servicios",
                column: "ServicioClinico_ServicioID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePaciente_Alergias_CatalogoAlergias_AlergiaID",
                table: "DetallePaciente_Alergias",
                column: "CatalogoAlergias_AlergiaID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePaciente_Alergias_Pacientes_PacienteID",
                table: "DetallePaciente_Alergias",
                column: "Pacientes_PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReceta_Medicamentos_CatalogoMedicamentos_MedicamentoID",
                table: "DetalleReceta_Medicamentos",
                column: "CatalogoMedicamentos_MedicamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReceta_Medicamentos_ConsultasMedicas_ConsultasID",
                table: "DetalleReceta_Medicamentos",
                column: "ConsultasMedicas_ConsultasID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Citas_CitaID",
                table: "Facturas",
                column: "Citas_CitaID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_NumeroFactura",
                table: "Facturas",
                column: "NumeroFactura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Pacientes_PacienteID",
                table: "Facturas",
                column: "Pacientes_PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Usuarios_UsuarioID",
                table: "Facturas",
                column: "Usuarios_UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_CodigoMINSA",
                table: "Medicos",
                column: "CodigoMINSA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_Especialidades_EspecialidadID",
                table: "Medicos",
                column: "Especialidades_EspecialidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_Usuarios_UsuarioID",
                table: "Medicos",
                column: "Usuarios_UsuarioID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Cedula",
                table: "Pacientes",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_NumeroExpediente",
                table: "Pacientes",
                column: "NumeroExpediente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_CodigoPermiso",
                table: "Permisos",
                column: "CodigoPermiso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NombreRol",
                table: "Roles",
                column: "NombreRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_Permisos_PermisoID",
                table: "RolPermisos",
                column: "Permisos_PermisoID");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_Roles_RoleID_Permisos_PermisoID",
                table: "RolPermisos",
                columns: new[] { "Roles_RoleID", "Permisos_PermisoID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosClinicos_CodigoServicio",
                table: "ServiciosClinicos",
                column: "CodigoServicio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermisos_Permisos_PermisoID",
                table: "UsuarioPermisos",
                column: "Permisos_PermisoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermisos_Usuarios_UsuarioID_Permisos_PermisoID",
                table: "UsuarioPermisos",
                columns: new[] { "Usuarios_UsuarioID", "Permisos_PermisoID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Roles_RoleID",
                table: "Usuarios",
                column: "Roles_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UserName",
                table: "Usuarios",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleConsulta_Diagnosticos");

            migrationBuilder.DropTable(
                name: "DetalleFactura_Servicios");

            migrationBuilder.DropTable(
                name: "DetallePaciente_Alergias");

            migrationBuilder.DropTable(
                name: "DetalleReceta_Medicamentos");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "UsuarioPermisos");

            migrationBuilder.DropTable(
                name: "CatalogoDiagnosticosCIE10");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "ServiciosClinicos");

            migrationBuilder.DropTable(
                name: "CatalogoAlergias");

            migrationBuilder.DropTable(
                name: "CatalogoMedicamentos");

            migrationBuilder.DropTable(
                name: "ConsultasMedicas");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Consultorios");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
