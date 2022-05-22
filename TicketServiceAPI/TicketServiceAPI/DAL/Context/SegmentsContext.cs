using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TicketServiceAPI
{
    public partial class SegmentsContext : DbContext
    {
        private static IConfiguration _configuration;   
        public static void InitSegment(IConfiguration conf)
        {
            _configuration = conf;
        }
        public SegmentsContext()
        {
            //Database.EnsureCreated();
        }

        public SegmentsContext(DbContextOptions<SegmentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Segment> Segments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql(_configuration["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Segment>(entity =>
            {
                entity.ToTable("segment");

                entity.HasIndex(e => new { e.SerialId, e.TicketNumber }, "segment_data_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.AirlineCode)
                    .HasMaxLength(40)
                    .HasColumnName("airline_code");

                entity.Property(e => e.ArriveDatetime)
                    .HasMaxLength(40)
                    .HasColumnName("arrive_datetime");

                entity.Property(e => e.ArrivePlace)
                    .HasMaxLength(40)
                    .HasColumnName("arrive_place");

                entity.Property(e => e.Birthdate)
                    .HasMaxLength(40)
                    .HasColumnName("birth_date");

                entity.Property(e => e.DepartDatetime)
                    .HasMaxLength(40)
                    .HasColumnName("depart_datetime");

                entity.Property(e => e.DepartPlace)
                    .HasMaxLength(40)
                    .HasColumnName("depart_place");

                entity.Property(e => e.DocNumber)
                    .HasMaxLength(40)
                    .HasColumnName("doc_number");

                entity.Property(e => e.DocType)
                    .HasMaxLength(40)
                    .HasColumnName("doc_type");

                entity.Property(e => e.FlightNum).HasColumnName("flight_num");

                entity.Property(e => e.Gender)
                    .HasMaxLength(40)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.OperationPlace)
                    .HasMaxLength(40)
                    .HasColumnName("operation_place");

                entity.Property(e => e.OperationTime)
                    .HasMaxLength(40)
                    .HasColumnName("operation_time");

                entity.Property(e => e.OperationType)
                    .HasMaxLength(40)
                    .HasColumnName("operation_type");

                entity.Property(e => e.PassengerType)
                    .HasMaxLength(40)
                    .HasColumnName("passenger_type");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(40)
                    .HasColumnName("patronymic");

                entity.Property(e => e.PnrId)
                    .HasMaxLength(40)
                    .HasColumnName("pnr_id");

                entity.Property(e => e.SerialId).HasColumnName("serialid");

                entity.Property(e => e.Surname)
                    .HasMaxLength(40)
                    .HasColumnName("surname");

                entity.Property(e => e.TicketNumber)
                    .HasMaxLength(40)
                    .HasColumnName("ticket_number");

                entity.Property(e => e.TicketType).HasColumnName("ticket_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
