namespace Diet.Data
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealName { get; set;}
        public string Description { get; set;}
        public double Calories { get; set;}
        public string Picture { get; set;}

        ICollection<MealType> MealTypes { get; set;}
        ICollection<MealDay> MealDays { get; set; }
    }
}
