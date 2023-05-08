namespace Diet.Data
{
    public class MealPlan
    {
        public int MealPlanId { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
        public int TrainerInfoId { get; set; }
        public TrainerInfo TrainerInfos { get; set; }
        public int MealDayId { get; set; }
        public MealDay MealDays { get; set; }
    }
}
