using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Fiver.EF.Crud;

namespace Fiver.EF.Crud.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20170717134706_'setup'")]
    partial class setup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fiver.EF.Crud.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Fiver.EF.Crud.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("Fiver.EF.Crud.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DirectorId");

                    b.Property<int>("ReleaseYear");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Fiver.EF.Crud.Entities.MovieActor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<int>("MovieId");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("Fiver.EF.Crud.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Dated");

                    b.Property<int>("MovieId");

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });
        }
    }
}
