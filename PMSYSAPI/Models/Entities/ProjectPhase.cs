using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSYSAPI.Models.Entities
{
    public class tbProjPhase
    {
        [Key]
        [Column("proj_PhaseCod")]
        public int PhaseCod { get; set; }

        [Column("proj_Phase")]
        [MaxLength(120)]
        public string? PhaseName { get; set; } 

        [Column("proj_phasedesc")]
        [MaxLength(300)]
        public string? PhaseDesc { get; set; } 



    }
}
