using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Data.Entities;

namespace WebBase.Data.Configurations
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