using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diet.Data
{
    public class User:IdentityUser
    {
        [DisplayName("First name")]
        [MaxLength(25)]
        public string FisrtName { get; set; }

        [DisplayName("Last name")]
        [MaxLength(25)]
        public string LastName { get; set; }

        public int TrainerInfoId { get; set; }
        public TrainerInfo TrainerInfos { get;set; }

    }
}
