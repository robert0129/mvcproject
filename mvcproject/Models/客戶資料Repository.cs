using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace mvcproject.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => c.是否已刪除 != true);
        }

        public IQueryable<客戶資料> All(int? Filter)
        {
            return base.All().Where(c => c.是否已刪除 != true && c.客戶分類 >= Filter);
        }

        public 客戶資料 Find(int Id)
        {
            return this.All().FirstOrDefault(c => c.Id == Id);
        }

        public IQueryable<客戶資料> All(string search, int classification)
        {
            if (String.IsNullOrEmpty(search) && classification == 0)
            {
                return this.All();
            }
            else if (!String.IsNullOrEmpty(search) && classification == 0)
            {
                return this.All().Where(c => c.客戶名稱 == search);
            }
            else if (String.IsNullOrEmpty(search) && classification != 0)
            {
                return this.All().Where(c => classification > c.客戶分類);
            }
            else
            {
                return this.All().Where(c => c.客戶名稱 == search && classification > c.客戶分類);
            }
                //throw new NotImplementedException();
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}