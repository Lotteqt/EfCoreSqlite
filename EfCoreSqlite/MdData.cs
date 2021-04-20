using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreSqlite
{
    public enum MdRes
    {
        Ok,
        SensTiltNg,
        GlueNg
    }
    public class MdData
    {
        [Key]
        public int Id { get; set; }

        public string Barcode { get; set; }

        public int Res { get; set; }

        //时间
        public DateTime AaDateTime { get; set; }

    }

    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }
    }
}
