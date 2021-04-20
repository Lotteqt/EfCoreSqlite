using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EfCoreSqlite.DBModels
{
    public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
            => context is EfContext dynamicContext
                ? (context.GetType(), dynamicContext.CreateDateTime)
                : (object)context.GetType();
    }

    public class EfContext : DbContext
    {
        public DateTime CreateDateTime { get; set; }

        public static string connectionString
        {
            get
            {
                return @"Data Source=C:\Users\ltchen\Desktop\" + DateTime.Now.ToString("yyyy-MM")+".db";
            }
        }
        public DbSet<MdData> MdDatas { get; set; }

        public EfContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString).ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MdData>(entity =>
            {
                entity.ToTable(CreateDateTime.ToString("yyyyMMdd"));


                entity.Property(e => e.Barcode) // Column name is 'Full Name'
                    .IsRequired()
                    .HasColumnName("Barcode")
                    .HasColumnType("TEXT")
                    ;

                entity.Property(e => e.Id) // Column name is 'Full Name'
                    .IsRequired()
                    .HasColumnName("Id")
                    .HasColumnType("INTEGER")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Res) // Column name is 'Full Name'
                    .IsRequired()
                    .HasColumnName("res")
                    .HasColumnType("INTEGER");

                entity.Property(e => e.AaDateTime)
                    .IsRequired()
                    .HasColumnName("AaDateTime")
                    .HasColumnType("DATETIME")
    ;

                entity.HasKey("Id");
            });

        }
    }

}
