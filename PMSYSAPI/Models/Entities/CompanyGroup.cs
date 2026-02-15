using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSYSAPI.Models.Entities
{
    public class tbCompGrp
    {
        [Key]
        [Column("GroupCod")]
        public int GroupCod { get; set; }

        [Column("GroupName")]
        public string? GroupName { get; set; } 

        [Column("Group_Shortname")]
        public string? GroupShortname { get; set; } 
    }
}
