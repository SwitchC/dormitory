using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dormitary
{
    public partial class dormitoryContext : DbContext
    {
        public dormitoryContext()
        {
        }

        public dormitoryContext(DbContextOptions<dormitoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bloсk> Bloсks { get; set; } = null!;
        public virtual DbSet<Dormitory> Dormitories { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Kitchen> Kitchens { get; set; } = null!;
        public virtual DbSet<LaundryRoom> LaundryRooms { get; set; } = null!;
        public virtual DbSet<LivingRoom> LivingRooms { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Strike> Strikes { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UGK304U\\MSSQLSERVER01; Database=dormitory; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bloсk>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.NameDormitory })
                    .HasName("PK_Bloсk_1");

                entity.ToTable("Bloсk");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.Electricity).HasColumnName("electricity");

                entity.Property(e => e.NumberFloor).HasColumnName("numberFloor");

                entity.HasOne(d => d.N)
                    .WithMany(p => p.Bloсks)
                    .HasForeignKey(d => new { d.NumberFloor, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bloсk_Floor");
            });

            modelBuilder.Entity<Dormitory>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_Dormitories");

                entity.ToTable("Dormitory");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .HasColumnName("job");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.Pasword)
                    .HasMaxLength(6)
                    .HasColumnName("pasword");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phoneNumber");

                entity.HasOne(d => d.NameDormitoryNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NameDormitory)
                    .HasConstraintName("FK_Employee_Dormitory");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.HasKey(e => new { e.NumberFlor, e.NameDormitory });

                entity.ToTable("Floor");

                entity.Property(e => e.NumberFlor).HasColumnName("numberFlor");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.HasOne(d => d.NameDormitoryNavigation)
                    .WithMany(p => p.Floors)
                    .HasForeignKey(d => d.NameDormitory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Floor_Dormitory");
            });

            modelBuilder.Entity<Kitchen>(entity =>
            {
                entity.HasKey(e => new { e.NumberRoom, e.NameDormitory })
                    .HasName("PK_Kitchen_1");

                entity.ToTable("Kitchen");

                entity.Property(e => e.NumberRoom).HasColumnName("numberRoom");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.NumberOfGasStoves).HasColumnName("numberOfGasStoves");

                entity.Property(e => e.NumberOfSinks).HasColumnName("numberOfSinks");

                entity.HasOne(d => d.N)
                    .WithOne(p => p.Kitchen)
                    .HasForeignKey<Kitchen>(d => new { d.NumberRoom, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kitchen_Room");
            });

            modelBuilder.Entity<LaundryRoom>(entity =>
            {
                entity.HasKey(e => new { e.NumberRoom, e.NameDormitory })
                    .HasName("PK_LaundryRoom_1");

                entity.ToTable("LaundryRoom");

                entity.Property(e => e.NumberRoom).HasColumnName("numberRoom");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.NumberOfDryer).HasColumnName("numberOfDryer");

                entity.Property(e => e.NumberOfWashingMachine).HasColumnName("numberOfWashingMachine");

                entity.HasOne(d => d.N)
                    .WithOne(p => p.LaundryRoom)
                    .HasForeignKey<LaundryRoom>(d => new { d.NumberRoom, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LaundryRoom_Room");
            });

            modelBuilder.Entity<LivingRoom>(entity =>
            {
                entity.HasKey(e => new { e.NumberRoom, e.NameDormitory })
                    .HasName("PK_LivingRoom_1");

                entity.ToTable("LivingRoom");

                entity.Property(e => e.NumberRoom).HasColumnName("numberRoom");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.NumberBlock).HasColumnName("numberBlock");

                entity.HasOne(d => d.N)
                    .WithMany(p => p.LivingRooms)
                    .HasForeignKey(d => new { d.NumberBlock, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LivingRoom_Bloсk");

                entity.HasOne(d => d.NNavigation)
                    .WithOne(p => p.LivingRoom)
                    .HasForeignKey<LivingRoom>(d => new { d.NumberRoom, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LivingRoom_Room");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.NameDormitory });

                entity.ToTable("Room");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Info).HasColumnName("info");

                entity.Property(e => e.NumberFloor).HasColumnName("numberFloor");

                entity.HasOne(d => d.N)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => new { d.NumberFloor, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Floor");
            });

            modelBuilder.Entity<Strike>(entity =>
            {
                entity.ToTable("Strike");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");

                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.Property(e => e.Type)
                    .HasMaxLength(150)
                    .HasColumnName("type");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Strikes)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Strike_Employee");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Strikes)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Strike_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.Course).HasColumnName("course");

                entity.Property(e => e.Faculty)
                    .HasMaxLength(150)
                    .HasColumnName("faculty");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.NameDormitory)
                    .HasMaxLength(150)
                    .HasColumnName("nameDormitory");

                entity.Property(e => e.NumberRoom).HasColumnName("numberRoom");

                entity.Property(e => e.Password)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phoneNumber");

                entity.HasOne(d => d.N)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => new { d.NumberRoom, d.NameDormitory })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_LivingRoom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
