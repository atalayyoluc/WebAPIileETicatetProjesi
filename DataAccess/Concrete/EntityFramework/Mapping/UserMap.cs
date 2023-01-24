using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.ToTable("Users",@"dbo");
           builder.HasKey(t => t.Id);

           builder.Property(t=>t.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(50)
                .IsRequired();
            
           builder.Property(t=>t.FirstName)
                .HasColumnName("FirsName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.LastName)
                 .HasColumnName("LastName")
                 .HasMaxLength(50)
                 .IsRequired();

            builder.Property(t => t.Password)
                 .HasColumnName("Password")
                 .HasMaxLength(20)
                 .IsRequired();

            builder.Property(t => t.Gender)
                 .HasColumnName("Gender")
                 .IsRequired();

            builder.Property(t => t.DateOfBirth)
                 .HasColumnName("DateOfBirth")
                 .IsRequired();
            builder.Property(t => t.CreatedDate)
                .HasDefaultValue(DateTime.Now);

            builder.HasData(new User
            {
                Id = 1,
                FirstName = "Ahmet",
                LastName = "Doğan",
                Password = "123456",
                Gender = true,
                DateOfBirth = Convert.ToDateTime("01-01-1980"),
                CreatedDate = DateTime.Now,
                Address = "Kütahya",
                CreatedUserId = 1,
                Email = "ali.dogan@gmail.com",
                UserName = "ahmetdogan"
            }) ;


        }
    }
}
