using Tripeace.Domain.Enums;

namespace Tripeace.Application.ViewModels.Account
{
    public class IndexPlayerViewModel
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
    }
}
