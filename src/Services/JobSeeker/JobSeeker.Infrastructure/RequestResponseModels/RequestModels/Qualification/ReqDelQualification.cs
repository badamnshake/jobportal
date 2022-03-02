using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification
{
    public class ReqDelQualification
    {
        [Required] public int Id { get; set; }
    }
}