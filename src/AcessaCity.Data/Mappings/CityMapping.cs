using System;
using System.Linq;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessaCity.Data.Mappings
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(c => c.Latitude)
                .HasColumnType("decimal(11, 8)");

            builder.Property(c => c.Longitude)
                .HasColumnType("decimal(11, 8)");     

            builder.HasOne(c => c.CityState)
                .WithMany(s => s.Cities)
                .IsRequired();   



            builder.HasData
            (
                new City 
                {
                    Id = Guid.Parse("7ae590f1-c6a4-4bb3-91bf-1e82ea45bb4b"),
                    Name = "Campinas",
                    IBGECode = 3509502,
                    StateId = Guid.Parse("b545ceb9-fbde-43c9-bbcc-de62a49e1661"),
                    Latitude = -22.9064,
                    Longitude = -47.0616
                },
                new City
                {
                    Id = Guid.Parse("d9805d6e-4048-4783-8497-b8d4a237ef50"),
                    Name = "Sumaré",
                    IBGECode = 3552403,
                    StateId = Guid.Parse("b545ceb9-fbde-43c9-bbcc-de62a49e1661"),
                    Latitude = -22.8216,
                    Longitude = -47.2664
                },
                new City
                {
                    Id = Guid.Parse("1c3ca2cf-1e8e-4320-9868-65dc8d447315"),
                    Name = "Hortolândia",
                    IBGECode = 3519071,
                    StateId = Guid.Parse("b545ceb9-fbde-43c9-bbcc-de62a49e1661"),
                    Latitude = -22.8577,
                    Longitude = -47.2203
                },
                new City
                {
                    Id = Guid.Parse("6b4faa2d-22ff-47c9-9023-cf9c45bb3184"),
                    Name = "Paulínia",
                    IBGECode = 3536505,
                    StateId = Guid.Parse("b545ceb9-fbde-43c9-bbcc-de62a49e1661"),
                    Latitude = -22.7617,
                    Longitude = -47.1541
                },
                new City
                {
                    Id = Guid.Parse("b79016eb-3a9d-4f67-ac79-6c6539d99327"),
                    Name = "Valinhos",
                    IBGECode = 3556206,
                    StateId = Guid.Parse("b545ceb9-fbde-43c9-bbcc-de62a49e1661"),
                    Latitude = -22.9712,
                    Longitude = -46.9964
                }
            );            
        }
    }
}