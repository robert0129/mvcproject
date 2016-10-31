using System;
using System.Linq;
using System.Collections.Generic;
	
namespace mvcproject.Models
{   
	public  class vw_customerlistsRepository : EFRepository<vw_customerlists>, Ivw_customerlistsRepository
	{
    }

	public  interface Ivw_customerlistsRepository : IRepository<vw_customerlists>
	{

	}
}