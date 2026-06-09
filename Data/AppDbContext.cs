using ProjetoHELIOS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoHELIOS_API.Data
{
public class AppDbContext : DbContext
{
public AppDbContext(
DbContextOptions<AppDbContext> options
) : base(options)
{
}

    // DbSets

    public DbSet<Habitat> Habitats { get; set; }

    public DbSet<ModuloHabitacional> Modulos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Ocupante> Ocupantes { get; set; }

    public DbSet<Reserva> Reservas { get; set; }

    public DbSet<Sensor> Sensores { get; set; }

    public DbSet<LeituraSensor> Leituras { get; set; }

    public DbSet<Alerta> Alertas { get; set; }

    public DbSet<RegraAlerta> RegrasAlerta { get; set; }

    public DbSet<AcaoAutomatica> AcoesAutomaticas { get; set; }

    public DbSet<LogEvento> LogsEvento { get; set; }



    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        base.OnModelCreating(modelBuilder);

        // TABELAS

        modelBuilder.Entity<Habitat>()
            .ToTable("API_HELIOS_HABITAT");

        modelBuilder.Entity<ModuloHabitacional>()
            .ToTable("API_HELIOS_MODULO_HABITACIONAL");

        modelBuilder.Entity<Usuario>()
            .ToTable("API_HELIOS_USUARIO");

        modelBuilder.Entity<Ocupante>()
            .ToTable("API_HELIOS_OCUPANTE");

        modelBuilder.Entity<Reserva>()
            .ToTable("API_HELIOS_RESERVA");

        modelBuilder.Entity<Sensor>()
            .ToTable("API_HELIOS_SENSOR");

        modelBuilder.Entity<LeituraSensor>()
            .ToTable("API_HELIOS_LEITURA_SENSOR");

        modelBuilder.Entity<Alerta>()
            .ToTable("API_HELIOS_ALERTA");

        modelBuilder.Entity<RegraAlerta>()
            .ToTable("API_HELIOS_REGRA_ALERTA");

        modelBuilder.Entity<AcaoAutomatica>()
            .ToTable("API_HELIOS_ACAO_AUTOMATICA");

        modelBuilder.Entity<LogEvento>()
            .ToTable("API_HELIOS_LOG_EVENTO");


        // CONSTRAINTS

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();


        // PRECISION

        modelBuilder.Entity<ModuloHabitacional>()
            .Property(m => m.IndiceRisco)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Sensor>()
            .Property(s => s.LimiteMinimo)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Sensor>()
            .Property(s => s.LimiteMaximo)
            .HasPrecision(10, 2);

        modelBuilder.Entity<LeituraSensor>()
            .Property(l => l.ValorLeitura)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RegraAlerta>()
            .Property(r => r.ValorMinimo)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RegraAlerta>()
            .Property(r => r.ValorMaximo)
            .HasPrecision(10, 2);


        // RELACIONAMENTOS


        // Habitat -> Modulo

        modelBuilder.Entity<ModuloHabitacional>()
            .HasOne(m => m.Habitat)
            .WithMany(h => h.Modulos)
            .HasForeignKey(m => m.HabitatId);



        // Ocupante -> Reserva

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Ocupante)
            .WithMany(o => o.Reservas)
            .HasForeignKey(r => r.OcupanteId);



        // Modulo -> Reserva

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Modulo)
            .WithMany(m => m.Reservas)
            .HasForeignKey(r => r.ModuloId);



        // Modulo -> Sensor

        modelBuilder.Entity<Sensor>()
            .HasOne(s => s.Modulo)
            .WithMany(m => m.Sensores)
            .HasForeignKey(s => s.ModuloId);



        // Sensor -> Leitura

        modelBuilder.Entity<LeituraSensor>()
            .HasOne(l => l.Sensor)
            .WithMany(s => s.Leituras)
            .HasForeignKey(l => l.SensorId);



        // Sensor -> Alerta

        modelBuilder.Entity<Alerta>()
            .HasOne(a => a.Sensor)
            .WithMany(s => s.Alertas)
            .HasForeignKey(a => a.SensorId)
            .OnDelete(DeleteBehavior.Restrict);



        // Modulo -> Alerta

        modelBuilder.Entity<Alerta>()
            .HasOne(a => a.Modulo)
            .WithMany(m => m.Alertas)
            .HasForeignKey(a => a.ModuloId)
            .OnDelete(DeleteBehavior.Restrict);



        // Alerta -> Acao

        modelBuilder.Entity<AcaoAutomatica>()
            .HasOne(a => a.Alerta)
            .WithMany(a => a.Acoes)
            .HasForeignKey(a => a.AlertaId);
    }
}

}
