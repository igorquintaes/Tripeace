using Tripeace.Domain.Enums;

namespace Tripeace.Application.ViewModels.Account
{
    public class EditCharacterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vocation Vocation { get; set; }
        public Sex Sex { get; set; }
        public bool IsVisible { get; set; }
    }
}
