using System.Linq;

namespace mvcproject.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static sysdiagramsRepository GetsysdiagramsRepository()
		{
			var repository = new sysdiagramsRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static sysdiagramsRepository GetsysdiagramsRepository(IUnitOfWork unitOfWork)
		{
			var repository = new sysdiagramsRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static vw_customerlistsRepository Getvw_customerlistsRepository()
		{
			var repository = new vw_customerlistsRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static vw_customerlistsRepository Getvw_customerlistsRepository(IUnitOfWork unitOfWork)
		{
			var repository = new vw_customerlistsRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶資料Repository Get客戶資料Repository()
		{
			var repository = new 客戶資料Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶資料Repository Get客戶資料Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶資料Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶銀行資訊Repository Get客戶銀行資訊Repository()
		{
			var repository = new 客戶銀行資訊Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶銀行資訊Repository Get客戶銀行資訊Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶銀行資訊Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static 客戶聯絡人Repository Get客戶聯絡人Repository()
		{
			var repository = new 客戶聯絡人Repository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static 客戶聯絡人Repository Get客戶聯絡人Repository(IUnitOfWork unitOfWork)
		{
			var repository = new 客戶聯絡人Repository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}
        
        public static IQueryable<客戶資料> Sort(this IQueryable<客戶資料> entity, Customer cust, string asc)
        {
            IQueryable<客戶資料> new_entity;
            
            switch (cust)
            {
                case Customer.客戶名稱:
                    new_entity = string.Equals(asc, "up") 
                        ? entity.OrderBy(s => s.客戶名稱)
                        : entity.OrderByDescending(s => s.客戶名稱);
                    break;
                case Customer.統一編號:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.統一編號)
                        : entity.OrderByDescending(s => s.統一編號);
                    break;
                case Customer.電話:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.電話)
                        : entity.OrderByDescending(s => s.電話);
                    break;
                case Customer.傳真:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.傳真)
                        : entity.OrderByDescending(s => s.傳真);
                    break;
                case Customer.地址:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.地址)
                        : entity.OrderByDescending(s => s.地址);
                    break;
                case Customer.電子郵件:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.Email)
                        : entity.OrderByDescending(s => s.Email);
                    break;
                default:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.客戶名稱)
                        : entity.OrderByDescending(s => s.客戶名稱);
                    break;
            }
            return new_entity;
        }

        public enum Customer : int {
            客戶名稱 = 0,
            統一編號,
            電話,
            傳真,
            地址,
            電子郵件
        }
        
        public static IQueryable<客戶聯絡人> Sort(this IQueryable<客戶聯絡人> entity, Contactor ctor, string asc)
        {
            IQueryable<客戶聯絡人> new_entity;

            switch (ctor)
            {
                case Contactor.職稱:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.職稱)
                        : entity.OrderByDescending(s => s.職稱);
                    break;
                case Contactor.姓名:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.姓名)
                        : entity.OrderByDescending(s => s.姓名);
                    break;
                case Contactor.電子郵件:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.Email)
                        : entity.OrderByDescending(s => s.Email);
                    break;
                case Contactor.手機:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.手機)
                        : entity.OrderByDescending(s => s.手機);
                    break;
                case Contactor.電話:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.電話)
                        : entity.OrderByDescending(s => s.電話);
                    break;
                case Contactor.客戶名稱:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.客戶資料.客戶名稱)
                        : entity.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    new_entity = string.Equals(asc, "up")
                        ? entity.OrderBy(s => s.職稱)
                        : entity.OrderByDescending(s => s.職稱);
                    break;
            }

            return new_entity;
        }
        public enum Contactor : int {
            職稱 = 0,
            姓名,
            電子郵件,
            手機,
            電話,
            客戶名稱
        }
    }
}