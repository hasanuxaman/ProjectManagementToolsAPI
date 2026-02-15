using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSYSAPI.Models.Entities
{
    public class tbComp
    {
        [Key]
        [Column("CompCod")]
        public int CompCod { get; set; }

        [Column("Compname")]
        [MaxLength(120)]
        public string Compname { get; set; } = null!;

        [Column("CompShortname")]
        [MaxLength(50)]
        public string CompShortname { get; set; } = null!;

        [Column("cmp_groupcod")]
        public int CmpGroupCod { get; set; }





    }
}
