using cloudscribe.Web.Pagination;
using Tripeace.Service.DTO.Account;

namespace Tripeace.Application.Areas.Admin.ViewModels.Account
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            Paging = new PaginationSettings();
        }

        public string Query { get; set; } = string.Empty;
        public AccountListDTO Data { get; set; }
        public PaginationSettings Paging { get; set; }
    }
}
