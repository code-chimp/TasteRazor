using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository.Contracts
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem entity);
    }
}
