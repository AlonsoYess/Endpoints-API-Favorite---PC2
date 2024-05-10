using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;
using UE.STOREDB.DOMAIN.Infrastructure.Data;

namespace UE.STOREDB.DOMAIN.Infrastructure.Repositories
{
    
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly StoreDbueContext _dbContext;

        public FavoriteRepository(StoreDbueContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Delete(int id)
        {
            var findFavorite = await _dbContext
                .Favorite
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (findFavorite == null) return false;

            _dbContext.Favorite.Remove(findFavorite);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Favorite>> GetAll()
        {
            return await _dbContext
                    .Favorite
                    .Include(x => x.User)
                    .Include(x => x.Product)
                    .ToListAsync();
        }

        public async Task<Favorite> GetById(int id)
        {
            return await _dbContext
                    .Favorite
                    .Include(x => x.User)
                    .Include(x => x.Product)
                    .Where (x => x.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(Favorite favorite)
        {

            var findUser = await _dbContext
                   .User
                   .Where(x => x.IsActive == true && x.Id == favorite.UserId)
                   .FirstOrDefaultAsync();

            if (findUser == null) return false;

            var findProduct = await _dbContext.Product.Where(x => x.IsActive == true && x.Id == favorite.ProductId).FirstOrDefaultAsync();
            if (findProduct == null) return false;

            var findFavorire = await _dbContext.Favorite.Where(x => x.UserId == favorite.UserId && x.ProductId == favorite.ProductId).FirstOrDefaultAsync();
            if (findFavorire != null) return false;



            favorite.CreatedAt = DateTime.Now;
            await _dbContext.Favorite.AddAsync(favorite);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        
        
        }
    }
}
