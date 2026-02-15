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
        [MaxLength(200)]
        public string ProjName { get; set; } = null!;

        [Column("Proj_Shortname")]
        [MaxLength(50)]
        public string ProjShortname { get; set; } = null!;

        [Column("Proj_Desc")]
        [MaxLength(500)]
        public string ProjDesc { get; set; } = null!;

        [Column("Proj_compcod")]
        public int CompCod { get; set; }

        [Column("Proj_Phasecod")]
        public int PhaseCod { get; set; }

        [Column("Proj_StsCod")]
        public int StatusCod { get; set; }

        [Column("Proj_initDate")]
        public DateTime? InitDate { get; set; }

        [Column("Proj_StrtPlndt")]
        public DateTime? StartPlannedDate { get; set; }

        [Column("Proj_EndPlandt")]
        public DateTime? EndPlannedDate { get; set; }

        [Column("Proj_EstAmount")]
        public decimal? EstimatedAmount { get; set; }
    }
}
