using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCoreSqlite.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EfCoreSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtStart = new DateTime(2021, 3, 20);
            DateTime dtEnd = new DateTime(2021, 3, 22);
            List<MdData> results = null;
            for (; dtStart <= dtEnd; dtStart = dtStart.AddDays(1))
            {
                using (EfContext db = new EfContext { CreateDateTime = dtStart })
                {
                    db.Database.EnsureCreated();
                    try
                    {
                        db.Database.EnsureCreated();
                        RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)db.Database.GetService<IDatabaseCreator>();
                        databaseCreator.CreateTables();
                    }
                    catch (Microsoft.Data.Sqlite.SqliteException)
                    {

                    }
                    if (dtStart == new DateTime(2021, 3, 20)) results = db.MdDatas.Where(md => md.Res == 0).ToList();
                    results = results.Union(db.MdDatas.Where(md => md.Res == 0)).ToList();
                }
            }
            var tableList = results.ToList();


            //    using (EfContext db = new EfContext { CreateDateTime = DateTime.Now })
            //    {
            //        db.Database.EnsureCreated();
            //        try
            //        {
            //            db.Database.EnsureCreated();
            //            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)db.Database.GetService<IDatabaseCreator>();
            //            databaseCreator.CreateTables();
            //        }
            //        catch (Microsoft.Data.Sqlite.SqliteException)
            //        {

            //        }
            //        var md = new MdData
            //        {
            //            Barcode = "asd",
            //            Res = 0
            //        };
            //        db.MdDatas.Add(md);
            //        db.SaveChangesAsync();

            //    }
            //    using (EfContext db = new EfContext { CreateDateTime = DateTime.Now.AddDays(-1) })
            //    {

            //        db.Database.EnsureCreated();
            //        try
            //        {
            //            db.Database.EnsureCreated();
            //            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)db.Database.GetService<IDatabaseCreator>();
            //            databaseCreator.CreateTables();
            //        }
            //        catch (Microsoft.Data.Sqlite.SqliteException)
            //        {

            //        }
            //        var mdList = new List<MdData> { new MdData { Res = 0, Barcode = "qqwe" }, new MdData { Res = 1, Barcode = "vbn" } };
            //        db.MdDatas.AddRange(mdList);
            //        db.SaveChangesAsync();



            //    }
            //}
            Console.Read();
        }
    }
}
