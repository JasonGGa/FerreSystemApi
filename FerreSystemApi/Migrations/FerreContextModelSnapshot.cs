using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FerreSystemApi.Models;

namespace FerreSystemApi.Migrations
{
    [DbContext(typeof(FerreContext))]
    partial class FerreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FerreSystemApi.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double?>("PurchPriceDol");

                    b.Property<double?>("PurchPriceSol");

                    b.Property<double?>("RetailPrice");

                    b.Property<int?>("Stock");

                    b.Property<double?>("WholesalePrice");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });
        }
    }
}
