using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAppClient.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isLoaded;

        public bool IsValid<T>(T instance, out List<ValidationResult> errors)
            where T : class
        {
            errors = new();
            var validationContext = new ValidationContext(instance);
            return Validator.TryValidateObject(instance, validationContext, errors, true);
        }

        public async Task ExecuteWithLoadingIndicator(Func<Task> operation)
        {
            try
            {
                IsLoading = true;
                await operation();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task<T> ExecuteWithLoadingIndicator<T>(Func<Task<T>> operation)
        {
            try
            {
                IsLoading = true;
                return await operation();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
