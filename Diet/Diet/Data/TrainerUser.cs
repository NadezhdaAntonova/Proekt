namespace Diet.Data
{
    public class TrainerUser
    {
        public int TrainerUserId { get; set; }
        public int TrainerInfoId { get; set; }
        public TrainerInfo TrainerInfos { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
