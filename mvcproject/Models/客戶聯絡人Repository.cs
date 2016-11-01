using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace mvcproject.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(ct => ct.是否已刪除 != true).Include(c => c.客戶資料);
        }

        public 客戶聯絡人 Find(int Id)
        {
            return this.All().FirstOrDefault(c => c.Id == Id);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }

        public IQueryable<客戶聯絡人> All(string search, string jobTitle)
        {
            if (String.IsNullOrEmpty(search) && String.IsNullOrEmpty(jobTitle))
            {
                return this.All();
            }
            else if (!String.IsNullOrEmpty(search) && String.IsNullOrEmpty(jobTitle))
            {
                return this.All().Where(ct => ct.姓名.Contains(search) || ct.客戶資料.客戶名稱.Contains(search));
            }
            else if (String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(jobTitle))
            {
                return this.All().Where(ct => ct.職稱.Contains(jobTitle));
            }
            else
            {
                return this.All().Where(ct => (ct.姓名.Contains(search) || ct.客戶資料.客戶名稱.Contains(search))  && ct.職稱.Contains(jobTitle));
            }
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}