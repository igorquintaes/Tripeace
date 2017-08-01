using cloudscribe.Web.Pagination;
using Tripeace.Service.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Areas.Admin.ViewModels.Account
{
    public class ListModel
    {
        public ListModel()
        {
            Paging = new PaginationSettings();
        }

        public string Query { get; set; } = string.Empty;
        public AccountListDTO Data { get; set; }
        public PaginationSettings Paging { get; set; }
    }
}
