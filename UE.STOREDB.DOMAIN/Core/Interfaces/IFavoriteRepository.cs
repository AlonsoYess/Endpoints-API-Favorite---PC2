using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;

namespace UE.STOREDB.DOMAIN.Core.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetAll();
        Task<Favorite> GetById(int id);
        Task<bool> Insert(Favorite favorite);
        Task<bool> Delete(int id);
    }
}
