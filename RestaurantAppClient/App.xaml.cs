using epj.Expander.Maui;

namespace RestaurantAppClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Expander.EnableAnimations();
            MainPage = new AppShell();
        }
    }
}