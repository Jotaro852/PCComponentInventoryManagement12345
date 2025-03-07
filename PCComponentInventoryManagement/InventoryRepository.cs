using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Data;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement.Data.Repositories
{
    public class InventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(InventoryTransaction transaction)
        {
            await _context.InventoryTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}