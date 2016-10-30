using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcproject.Models.Repository
{
    public interface ICustomersRepository : IDisposable
    {
        IEnumerable<客戶資料> GetAll();
        void Create(客戶資料 instance);
        void Update(客戶資料 instance);
        void Delete(客戶資料 instance);
        void SaveChanges();
        客戶資料 Get(int CustomerId);
    }
}
