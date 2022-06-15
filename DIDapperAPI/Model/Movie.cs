using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIDapperAPI.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        [DataType(DataType.DateTime)]
        public DateTime CreateDateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }

    public class Conn
    {
        public string ConnectionString { get; set; } = String.Empty;
    }
}

