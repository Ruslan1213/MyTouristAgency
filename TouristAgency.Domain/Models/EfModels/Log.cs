using System.Data.Linq.Mapping;

namespace TouristAgency.Domain.Models.EfModels
{
    public class Log
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long LogID { get; set; }

        [Column()]
        public string UserName { get; set; }

        [Column()]
        public string IP { get; set; }

        [Column()]
        public string Controller { get; set; }

        [Column()]
        public string Action { get; set; }

        [Column()]
        public int Number { get; set; }

        [Column()]
        public string Field { get; set; }

        [Column()]
        public string Value { get; set; }

        [Column()]
        public string GUID { get; set; }

    }
}
