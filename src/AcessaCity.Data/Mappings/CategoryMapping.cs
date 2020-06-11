using System;
using AcessaCity.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessaCity.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(120)");

            builder.HasOne(c => c.ParentCategory)
                .WithMany(p => p.Categories)
                .IsRequired(false);

            builder.HasData(
                new Category()
                {
                    Id = Guid.Parse("2695bd36-45d4-4135-a1c5-488e592788e5"),
                    Name = "Pavimentação danificada",
                    Active = true
                },
                new Category()
                {
                    Id = Guid.Parse("5aa23f4c-e480-462b-b402-966ea8bab551"),
                    Name = "Sinalização de trânsito",
                    Active = true
                },
                new Category()
                {
                    Id = Guid.Parse("47632f40-852f-4957-9813-34f1464a1849"),
                    Name = "Mecânismos de mobilidade",
                    Active = true
                },
                new Category()
                {
                    Id = Guid.Parse("a255e104-4b55-4954-aee9-07513da17e44"),
                    Name = "Vandalismo",
                    Active = true
                },
                new Category()
                {
                    Id = Guid.Parse("6a47411f-eac3-460b-8ede-b2e1a03137cd"),
                    Name = "Riscos à integridade física",
                    Active = true
                }
            );
        }
    }
}