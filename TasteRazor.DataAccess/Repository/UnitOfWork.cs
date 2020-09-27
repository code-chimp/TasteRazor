using System.Threading.Tasks;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IApplicationUserRepository ApplicationUser { get; }
        public ICategoryRepository Category { get; private set; }
        public IFoodTypeRepository FoodType { get; }
        public IMenuItemRepository MenuItem { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            ApplicationUser = new ApplicationUserRepository(_context);
            Category = new CategoryRepository(_context);
            FoodType = new FoodTypeRepository(_context);
            MenuItem = new MenuItemRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
