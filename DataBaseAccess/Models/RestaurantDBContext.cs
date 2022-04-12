using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseAccess.Models
{
    public partial class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext()
        {
        }

        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Dishes> Dishes { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<ManagerAssignment> ManagerAssignment { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-2S65Q3J;Database=pizza_restaurant_ver_6;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount)
                    .HasName("account_PK");

                entity.ToTable("account");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.AccountCreationDate)
                    .HasColumnName("account_creation_date")
                    .HasColumnType("date");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e_mail")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("client_PK");

                entity.ToTable("client");

                entity.HasIndex(e => e.AccountIdAccount)
                    .HasName("client__IDX")
                    .IsUnique();

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.AccountIdAccount).HasColumnName("account_id_account");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Points).HasColumnName("points");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Client)
                    .HasForeignKey<Client>(d => d.AccountIdAccount)
                    .HasConstraintName("client_account_FK");
            });

            modelBuilder.Entity<Dishes>(entity =>
            {
                entity.HasKey(e => new { e.OrderIdOrder, e.PizzaIdPizza })
                    .HasName("dishes_PK");

                entity.ToTable("dishes");

                entity.Property(e => e.OrderIdOrder).HasColumnName("order_id_order");

                entity.Property(e => e.PizzaIdPizza).HasColumnName("pizza_id_pizza");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.HistoricalCost).HasColumnName("historical_cost");

                entity.Property(e => e.HistoricalPrice).HasColumnName("historical_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DishesCollection)
                    .HasForeignKey(d => d.OrderIdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASS_3");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.DishesCollection)
                    .HasForeignKey(d => d.PizzaIdPizza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ASS_4");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.IdManager)
                    .HasName("manager_PK");

                entity.ToTable("manager");

                entity.HasIndex(e => e.AccountIdAccount)
                    .HasName("manager__IDX")
                    .IsUnique();

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.Property(e => e.AccountIdAccount).HasColumnName("account_id_account");

                entity.Property(e => e.SalaryBrutto).HasColumnName("salary_brutto");

                entity.Property(e => e.SalaryNetto).HasColumnName("salary_netto");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Manager)
                    .HasForeignKey<Manager>(d => d.AccountIdAccount)
                    .HasConstraintName("manager_account_FK");
            });

            modelBuilder.Entity<ManagerAssignment>(entity =>
            {
                entity.HasKey(e => new { e.ManagerIdManager, e.RestaurantIdRestaurant })
                    .HasName("manager_assignment_PK");

                entity.ToTable("manager_assignment");

                entity.Property(e => e.ManagerIdManager).HasColumnName("manager_id_manager");

                entity.Property(e => e.RestaurantIdRestaurant).HasColumnName("restaurant_id_restaurant");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ManagerAssignments)
                    .HasForeignKey(d => d.ManagerIdManager)
                    .HasConstraintName("FK_ASS_7");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.ManagerAssignments)
                    .HasForeignKey(d => d.RestaurantIdRestaurant)
                    .HasConstraintName("FK_ASS_8");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("order_PK");

                entity.ToTable("order");

                entity.HasIndex(e => e.RestaurantIdRestaurant)
                    .HasName("order__IDX")
                    .IsUnique();

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.ClientIdClient).HasColumnName("client_id_client");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date");

                entity.Property(e => e.DeliveryAdress)
                    .IsRequired()
                    .HasColumnName("delivery_adress")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantIdRestaurant).HasColumnName("restaurant_id_restaurant");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientIdClient)
                    .HasConstraintName("order_client_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Order>(d => d.RestaurantIdRestaurant)
                    .HasConstraintName("order_restaurant_FK");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasKey(e => e.IdPizza)
                    .HasName("pizza_PK");

                entity.ToTable("pizza");

                entity.Property(e => e.IdPizza).HasColumnName("id_pizza");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation)
                    .HasName("reservation_PK");

                entity.ToTable("reservation");

                entity.Property(e => e.IdReservation).HasColumnName("id_reservation");

                entity.Property(e => e.ClientIdClient).HasColumnName("client_id_client");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date");

                entity.Property(e => e.EndOfReservation).HasColumnName("end_of_reservation");

                entity.Property(e => e.ManagerIdManager).HasColumnName("manager_id_manager");

                entity.Property(e => e.StartOfReservation).HasColumnName("start_of_reservation");

                entity.Property(e => e.TableIdTable).HasColumnName("table_id_table");

                entity.Property(e => e.TableRestaurantIdRestaurant).HasColumnName("table_restaurant_id_restaurant");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ClientIdClient)
                    .HasConstraintName("reservation_client_FK");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ManagerIdManager)
                    .HasConstraintName("reservation_manager_FK");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => new { d.TableIdTable, d.TableRestaurantIdRestaurant })
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("reservation_table_FK");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.IdRestaurant)
                    .HasName("restaurant_PK");

                entity.ToTable("restaurant");

                entity.Property(e => e.IdRestaurant).HasColumnName("id_restaurant");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.EMail)
                    .HasColumnName("e_mail")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => new { e.IdTable, e.RestaurantIdRestaurant })
                    .HasName("table_PK");

                entity.ToTable("table");

                entity.Property(e => e.IdTable)
                    .HasColumnName("id_table")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RestaurantIdRestaurant).HasColumnName("restaurant_id_restaurant");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(d => d.RestaurantIdRestaurant)
                    .HasConstraintName("table_restaurant_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
