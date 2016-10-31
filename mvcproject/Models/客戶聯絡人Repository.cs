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
            return base.All().Where(bk => bk.isDeleted != true).Include(c => c.客戶資料);
        }

        public 客戶聯絡人 Find(int Id)
        {
            return this.All().FirstOrDefault(c => c.Id == Id);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.isDeleted = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}