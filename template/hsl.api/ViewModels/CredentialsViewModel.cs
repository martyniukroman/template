using hsl.api.ViewModels.Interfaces;

namespace hsl.api.ViewModels
{
    public class CredentialsViewModel : IViewModelInterface
    {
        public string UserName { set; get; }
        public string Password { get; set; }
        
        public bool isValid() => !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password);
    }
}