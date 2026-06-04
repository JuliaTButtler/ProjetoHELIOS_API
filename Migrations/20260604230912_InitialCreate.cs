using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHELIOS_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "API_HELIOS_HABITAT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoHabitat = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeTotal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StatusOperacional = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_HABITAT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_LOG_EVENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoEvento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataHoraEvento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    OrigemEvento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NivelEvento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_LOG_EVENTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_OCUPANTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Funcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    StatusOcupante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_OCUPANTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_REGRA_ALERTA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoSensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    ValorMaximo = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    NivelCriticidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PesoRisco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MensagemPadrao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ativo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_REGRA_ALERTA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_USUARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StatusUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NivelAcesso = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_MODULO_HABITACIONAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    HabitatId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomeModulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoModulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeOcupantes = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OcupacaoAtual = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StatusModulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NivelRisco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IndiceRisco = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_MODULO_HABITACIONAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_MODULO_HABITACIONAL_API_HELIOS_HABITAT_HabitatId",
                        column: x => x.HabitatId,
                        principalTable: "API_HELIOS_HABITAT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_RESERVA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OcupanteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ModuloId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    StatusReserva = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_RESERVA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_RESERVA_API_HELIOS_MODULO_HABITACIONAL_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "API_HELIOS_MODULO_HABITACIONAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_RESERVA_API_HELIOS_OCUPANTE_OcupanteId",
                        column: x => x.OcupanteId,
                        principalTable: "API_HELIOS_OCUPANTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_SENSOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ModuloId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomeSensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoSensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StatusSensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UnidadeMedida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LimiteMinimo = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    LimiteMaximo = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    IntervaloLeituraSegundos = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DataInstalacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_SENSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_SENSOR_API_HELIOS_MODULO_HABITACIONAL_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "API_HELIOS_MODULO_HABITACIONAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_ALERTA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ModuloId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TipoAlerta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NivelCriticidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataHoraAlerta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataHoraResolucao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    StatusAlerta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AcaoCorretiva = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_ALERTA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_ALERTA_API_HELIOS_MODULO_HABITACIONAL_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "API_HELIOS_MODULO_HABITACIONAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_ALERTA_API_HELIOS_SENSOR_SensorId",
                        column: x => x.SensorId,
                        principalTable: "API_HELIOS_SENSOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_LEITURA_SENSOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ValorLeitura = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    DataHoraLeitura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    StatusLeitura = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_LEITURA_SENSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_LEITURA_SENSOR_API_HELIOS_SENSOR_SensorId",
                        column: x => x.SensorId,
                        principalTable: "API_HELIOS_SENSOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "API_HELIOS_ACAO_AUTOMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AlertaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataHoraExecucao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    StatusAcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_HELIOS_ACAO_AUTOMATICA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_API_HELIOS_ACAO_AUTOMATICA_API_HELIOS_ALERTA_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "API_HELIOS_ALERTA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_ACAO_AUTOMATICA_AlertaId",
                table: "API_HELIOS_ACAO_AUTOMATICA",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_ALERTA_ModuloId",
                table: "API_HELIOS_ALERTA",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_ALERTA_SensorId",
                table: "API_HELIOS_ALERTA",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_LEITURA_SENSOR_SensorId",
                table: "API_HELIOS_LEITURA_SENSOR",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_MODULO_HABITACIONAL_HabitatId",
                table: "API_HELIOS_MODULO_HABITACIONAL",
                column: "HabitatId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_RESERVA_ModuloId",
                table: "API_HELIOS_RESERVA",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_RESERVA_OcupanteId",
                table: "API_HELIOS_RESERVA",
                column: "OcupanteId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_SENSOR_ModuloId",
                table: "API_HELIOS_SENSOR",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_API_HELIOS_USUARIO_Email",
                table: "API_HELIOS_USUARIO",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "API_HELIOS_ACAO_AUTOMATICA");

            migrationBuilder.DropTable(
                name: "API_HELIOS_LEITURA_SENSOR");

            migrationBuilder.DropTable(
                name: "API_HELIOS_LOG_EVENTO");

            migrationBuilder.DropTable(
                name: "API_HELIOS_REGRA_ALERTA");

            migrationBuilder.DropTable(
                name: "API_HELIOS_RESERVA");

            migrationBuilder.DropTable(
                name: "API_HELIOS_USUARIO");

            migrationBuilder.DropTable(
                name: "API_HELIOS_ALERTA");

            migrationBuilder.DropTable(
                name: "API_HELIOS_OCUPANTE");

            migrationBuilder.DropTable(
                name: "API_HELIOS_SENSOR");

            migrationBuilder.DropTable(
                name: "API_HELIOS_MODULO_HABITACIONAL");

            migrationBuilder.DropTable(
                name: "API_HELIOS_HABITAT");
        }
    }
}
