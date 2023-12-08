using Newtonsoft.Json;
using System.Text;

namespace AdvanceLoginMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UName.Text))
            {
                await DisplayAlert("Input Error", "Please enter user name!!!", "OK");
                UName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(Password.Text))
            {
                await DisplayAlert("Input Error", "Please enter password!!!", "OK");
                Password.Focus();
                return;
            }
            User UserDetails = new User();
            UserDetails.UserId = UName.Text;
            UserDetails.Password = Password.Text;

            string url = $"http://127.0.0.1/api/MAUILogin/GetUser?UserId={UserDetails.UserId}&Password={UserDetails.Password}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response1 = await client.GetAsync(url);
            string result = await response1.Content.ReadAsStringAsync();
            var EmpList = JsonConvert.DeserializeObject<List<User>>(result);

                UserDetails = new User();
                foreach (User item in EmpList)
                {
                    UserDetails.id = item.id;
                    UserDetails.Name = item.Name;
                    UserDetails.DOB = item.DOB;
                    UserDetails.POB = item.POB;
                    UserDetails.Email = item.Email;
                    UserDetails.UserId = item.UserId;
                    UserDetails.Password = item.Password;
                    UserDetails.myArray = item.myArray;
                }
                
            if (UserDetails.id != 0)
            {
                await DisplayAlert("Welcome", "Login Successfully.", "OK");
                await Navigation.PushModalAsync(new Home(UserDetails));
            }
            else
            {
                await DisplayAlert("Error", "Invalid Credentials", "OK");
                return;
            }
        }

        private void BtnRegistration_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegistrationPage(null));
        }
    }

}
