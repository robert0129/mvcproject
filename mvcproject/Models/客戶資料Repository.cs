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
            return base.All().Where(c => c.isDeleted != true);
        }

        public 客戶資料 Find(int Id)
        {
            return this.All().FirstOrDefault(bank => bank.Id == Id);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.isDeleted = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}