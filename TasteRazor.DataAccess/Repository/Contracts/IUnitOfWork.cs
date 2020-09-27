using System;
using System.Threading.Tasks;

namespace TasteRazor.DataAccess.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        ICategoryRepository Category { get; }
        IFoodTypeRepository FoodType { get; }
        IMenuItemRepository MenuItem { get; }
        void Save();
        Task SaveAsync();
    }
}
