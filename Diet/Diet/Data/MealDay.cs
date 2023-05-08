namespace Diet.Data
{
    public class MealDay
    {
        public int MealDayId { get; set; }
        public int MealTypeId { get; set; }
        public MealType MealTypes { get; set; }
        public int MealId { get; set; }
        public Meal Meals { get; set; }
        public DateTime Date { get; set; }
    }
}
