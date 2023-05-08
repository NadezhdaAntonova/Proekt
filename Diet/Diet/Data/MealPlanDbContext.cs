using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diet.Data
{
    public class MealPlanDbContext : IdentityDbContext<User>
    {
        public MealPlanDbContext(DbContextOptions<MealPlanDbContext> options)
            : base(options)
        {
        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealDay> MealDays { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<PendingRequest> PendingRequests { get; set; }
        public DbSet<TrainerInfo> TrainerInfos { get; set; }
        public DbSet<TrainerUser> TrainerUsers { get; set; }

    }
}