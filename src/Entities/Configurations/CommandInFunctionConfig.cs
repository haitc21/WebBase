using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public class CommandInFunctionConfig : IEntityTypeConfiguration<CommandInFunction>
    {
        public void Configure(EntityTypeBuilder<CommandInFunction> builder)
        {
            builder.ToTable("CommandInFunctions");

            builder.Property(x => x.CommandId).HasMaxLength(50).IsUnicode(true).IsRequired();

            builder.Property(x => x.FunctionId).HasMaxLength(50).IsUnicode(true).IsRequired();

            builder.HasKey(x => new { x.CommandId, x.FunctionId });

            builder.HasOne(cf => cf.Command).WithMany(c => c.CommandInFunctions)
                .HasForeignKey(cf => cf.CommandId);
            builder.HasOne(cf => cf.Function).WithMany(f => f.CommandInFunctions)
                .HasForeignKey(cf => cf.FunctionId);
        }
    }
}
