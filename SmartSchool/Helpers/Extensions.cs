using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Helpers
{
	public static class Extensions
	{
		public static void AddPagination(this HttpResponse response,
			int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

			response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
			response.Headers.Add("Acess-Control-Expose-Header", "Pagination");
		}
	}
}
