using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class ModelBuilderExtension
    {
        private readonly string AdminRoleName = "Admin";
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Command>().HasData(
                new Command() { Id = "VIEW", Name = "Xem" },
                    new Command() { Id = "CREATE", Name = "Thêm" },
                    new Command() { Id = "UPDATE", Name = "Sửa" },
                    new Command() { Id = "DELETE", Name = "Xoá" },
                    new Command() { Id = "APPROVE", Name = "Duyệt" });
        }
    }
}
