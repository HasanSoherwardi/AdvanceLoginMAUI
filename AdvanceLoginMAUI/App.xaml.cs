using SQLite;

namespace AdvanceLoginMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

          //  SQLiteConnection con = DependencyService.Get<SQLiteInterface>().GetConnectionWithDatabase();
            MainPage = new AppShell();
        }
    }
}
