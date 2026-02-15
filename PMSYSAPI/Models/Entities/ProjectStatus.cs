using System.ComponentModel.DataAnnotations;

namespace PMSYSAPI.Models.Entities
{
    public class tbStatus
    {
        [Key]
        public int ProjStatusCod { get; set; }
        public string ProjStatus { get; set; } = string.Empty;
        public string ProjStsDtl { get; set; } = string.Empty;

       
        public virtual ICollection<tbProjList>? Projects { get; set; }
    }
}
