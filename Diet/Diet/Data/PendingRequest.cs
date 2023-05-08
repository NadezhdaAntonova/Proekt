namespace Diet.Data
{
    public class PendingRequest
    {
        public int PendingRequestId { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
        public int TrainerInfoId { get; set; }
        public TrainerUser TrainerInfos { get; set; }
    }
}
