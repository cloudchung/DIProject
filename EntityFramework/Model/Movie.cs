using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Model
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
}
