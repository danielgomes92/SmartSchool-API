﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Helpers
{
	public class PaginationHeader
	{
		public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			CurrentPage = currentPage;
			ItemsPerPage = itemsPerPage;
			TotalItems = totalItems;
			TotalPages = totalPages;
		}


		[JsonProperty("current-page")]
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
	}
}
