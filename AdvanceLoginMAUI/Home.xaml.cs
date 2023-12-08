using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Formats.Tar;
using System.Reflection.Metadata;
using System.Text;

namespace AdvanceLoginMAUI;

public partial class Home 
{
    User userDetails = new User();
    public Home(User user)
    {
        InitializeComponent();
        userDetails = user;
    }

    async void GetAllUsers()
    {
        try
        {
            string url = $"http://127.0.0.1/api/MAUILogin/GetUser?UserId={userDetails.UserId}&Password={userDetails.Password}";

            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(url);
            var FileList = JsonConvert.DeserializeObject<List<User>>(result);
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = new ObservableCollection<User>(FileList);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Input Error", ex.Message, "OK");
            return;
        }
    }
    protected override void OnAppearing()
    {
        GetAllUsers();
    }
    
    private void BtnEdit_Clicked(object sender, EventArgs e)
    {
        User user = (((Button)sender).CommandParameter) as User;
        if (user != null)
        {
            Navigation.PushModalAsync(new RegistrationPage(user));

            MessagingCenter.Unsubscribe<User>(this, "ReciveData");
            MessagingCenter.Subscribe<User>(this, "ReciveData", (value) =>
            {
                userDetails = value;    
            });
        }
    }

    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        User user = (((Button)sender).CommandParameter) as User;
        if (user != null)
        {
            var msgResult = await DisplayActionSheet("Alert", "Cancel", "Ok", "Are you sure to delete?");
            switch (msgResult)
            {
                case "Ok":
                    string url = $"http://127.0.0.1/api/MAUILogin/DeleteUser?Uid={user.id}";
                    HttpClient client = new HttpClient();

                    HttpResponseMessage response1 = await client.DeleteAsync(url);
                    string result = await response1.Content.ReadAsStringAsync();
                    Response responseData = JsonConvert.DeserializeObject<Response>(result);
                    if (responseData.Status == 1)
                    {
                        await DisplayAlert("Delete", "Delete Successfully.", "OK");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Deletion Failed.", "OK");
                        return;
                    }
                    break;
            }
        }
    }

    private void EmployeeList_Refreshing(object sender, EventArgs e)
    {
        GetAllUsers(); 
        EmployeeList.IsRefreshing = false;
    }
}