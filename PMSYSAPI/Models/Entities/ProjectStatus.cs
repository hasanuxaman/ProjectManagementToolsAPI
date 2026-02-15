using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSYSAPI.Models.Entities
{
    public class tbStatus
    {
        [Key]
        [Column("Proj_StatusCod")]
        public int ProjStatusCod { get; set; }

        [Column("Proj_Status")]
        public string ProjStatus { get; set; } = null!; // Required

        [Column("Proj_StsDtl")]
        public string? ProjStsDtl { get; set; } // Nullable



    }
}
