using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Diet.Data
{
    public class TrainerInfo
    {
        public int TrainerInfoId { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        ICollection<User> Users { get; set; }
    }
}
