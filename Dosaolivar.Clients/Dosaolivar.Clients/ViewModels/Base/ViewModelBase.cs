using System.Threading.Tasks;
using SmartHotel.Clients.Core.Services.Dialog;
using SmartHotel.Clients.Core.Services.Navigation;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
namespace SmartHotel.Clients.Core.ViewModels.Base
{
    public abstract class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }


        protected readonly IAdapter Adapter;
        protected const string DeviceIdKey = "DeviceIdNavigationKey";
        protected const string ServiceIdKey = "ServiceIdNavigationKey";
        protected const string CharacteristicIdKey = "CharacteristicIdNavigationKey";
        protected const string DescriptorIdKey = "DescriptorIdNavigationKey";


        public ViewModelBase(IAdapter adapter)
        {
            Adapter = adapter;
        }

      
       

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}