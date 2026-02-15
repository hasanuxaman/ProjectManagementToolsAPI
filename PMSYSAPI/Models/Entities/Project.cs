using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSYSAPI.Models.Entities
{
    public class tbProjList
    {
         [Key]
        [Column("ProjCod")]
        public int ProjCod { get; set; }

        [Column("Proj_Name")]
        public string ProjName { get; set; } = null!;

        [Column("Proj_Shortname")]
        public string? ProjShortname { get; set; }

        [Column("Proj_Desc")]
        public string? ProjDesc { get; set; }

        [Column("Proj_compcod")]
        public int ProjCompCod { get; set; }

        [Column("Proj_Phasecod")]
        public int ProjPhaseCod { get; set; }

        [Column("Proj_StsCod")]
        public int ProjStsCod { get; set; }

        [Column("Proj_initDate")]
        public DateTime? ProjInitDate { get; set; }

        [Column("Proj_StrtPlndt")]
        public DateTime? ProjStrtPlndt { get; set; }

        [Column("Proj_EndPlandt")]
        public DateTime? ProjEndPlandt { get; set; }

        [Column("Proj_EstAmount")]
        public decimal? ProjEstAmount { get; set; }

        // Navigation properties (optional, for joins)
        [ForeignKey("ProjCompCod")]
        public tbComp? Company { get; set; }

        [ForeignKey("ProjPhaseCod")]
        public tbProjPhase? Phase { get; set; }

        [ForeignKey("ProjStsCod")]
        public tbStatus? Status { get; set; }
    }
}
