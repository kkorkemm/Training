namespace Training.Base
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class TrainingDBEntities : DbContext
    {
        public TrainingDBEntities()
            : base("name=TrainingDBEntities")
        {
        }

        private static TrainingDBEntities context;
        /// <summary>
        /// Получение контекста данных
        /// </summary>
        /// <returns></returns>
        public static TrainingDBEntities GetContext()
        {
            if (context == null)
                context = new TrainingDBEntities();
            return context;
        }

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public static User currentUser;
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
