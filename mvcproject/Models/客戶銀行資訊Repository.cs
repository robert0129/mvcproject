using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace mvcproject.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(bk => bk.isDeleted != true).Include(c => c.客戶資料);
        }

        public 客戶銀行資訊 Find(int Id)
        {
            return this.All().FirstOrDefault(bank => bank.Id == Id);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.isDeleted = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}