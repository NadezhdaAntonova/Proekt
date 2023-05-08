namespace Diet.Data
{
    public class MealType
    {
        public int MealTypeId { get; set; }
        public string TypeName { get; set;}
        ICollection<MealDay> MealDays  { get; set; }
    }
}
