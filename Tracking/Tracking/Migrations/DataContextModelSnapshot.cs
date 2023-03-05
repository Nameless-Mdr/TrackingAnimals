﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tracking.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalTypeAnimal", b =>
                {
                    b.Property<int>("AnimalsId")
                        .HasColumnType("integer");

                    b.Property<int>("TypesId")
                        .HasColumnType("integer");

                    b.HasKey("AnimalsId", "TypesId");

                    b.HasIndex("TypesId");

                    b.ToTable("types_animals", "info");
                });

            modelBuilder.Entity("Domain.Entity.Animal.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChipperId")
                        .HasColumnType("integer")
                        .HasColumnName("chipper_id");

                    b.Property<DateTimeOffset>("ChippingDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("chipping_date_time");

                    b.Property<int>("ChippingLocationId")
                        .HasColumnType("integer")
                        .HasColumnName("chipping_location_id");

                    b.Property<DateTimeOffset?>("DeathDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("death_date_time");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<double>("Length")
                        .HasColumnType("double precision")
                        .HasColumnName("length");

                    b.Property<string>("LifeStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("ALIVE")
                        .HasColumnName("life_status");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.HasIndex("ChipperId");

                    b.HasIndex("ChippingLocationId");

                    b.ToTable("animals", "info", t =>
                        {
                            t.HasCheckConstraint("Gender", "gender IN ('MALE', 'FEMALE', 'OTHER')")
                                .HasName("CH_gender");

                            t.HasCheckConstraint("Height", "height > 0")
                                .HasName("CH_height");

                            t.HasCheckConstraint("Length", "length > 0")
                                .HasName("CH_length");

                            t.HasCheckConstraint("LifeStatus", "life_status IN ('ALIVE', 'DEAD')")
                                .HasName("CH_life_status");

                            t.HasCheckConstraint("Weight", "weight > 0")
                                .HasName("CH_weight");
                        });
                });

            modelBuilder.Entity("Domain.Entity.Animal.TypeAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("types", "info");
                });

            modelBuilder.Entity("Domain.Entity.Location.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision")
                        .HasColumnName("longitude");

                    b.HasKey("Id");

                    b.HasIndex("Latitude", "Longitude")
                        .IsUnique();

                    b.ToTable("locations", "info", t =>
                        {
                            t.HasCheckConstraint("Latitude", "latitude >= -90 AND latitude <= 90")
                                .HasName("CH_latitude");

                            t.HasCheckConstraint("Longitude", "longitude >= -180 AND longitude <= 180")
                                .HasName("CH_longitude");
                        });
                });

            modelBuilder.Entity("Domain.Entity.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", "auth");
                });

            modelBuilder.Entity("Domain.Entity.User.UserSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid>("RefreshToken")
                        .HasColumnType("uuid")
                        .HasColumnName("refresh_token");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("user_session", "auth");
                });

            modelBuilder.Entity("AnimalTypeAnimal", b =>
                {
                    b.HasOne("Domain.Entity.Animal.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Animal.TypeAnimal", null)
                        .WithMany()
                        .HasForeignKey("TypesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entity.Animal.Animal", b =>
                {
                    b.HasOne("Domain.Entity.User.User", "User")
                        .WithMany("Animals")
                        .HasForeignKey("ChipperId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Location.Location", "Location")
                        .WithMany("Animals")
                        .HasForeignKey("ChippingLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.User.UserSession", b =>
                {
                    b.HasOne("Domain.Entity.User.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Location.Location", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Domain.Entity.User.User", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
