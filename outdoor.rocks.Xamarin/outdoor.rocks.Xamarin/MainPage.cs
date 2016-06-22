using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace outdoor.rocks.Xamarin
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var gridContainer = new Grid()
            {
                Padding = new Thickness(1, 1),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                }
            };

            var label = new Label
            {
                Text = GetDataFromRestTestCtrl().Result,
                BackgroundColor = Color.Aqua
            };

            gridContainer.Children.Add(label, 1, 1);
            
            Content = gridContainer;
        }

        private async Task<string> GetDataFromRestTestCtrl()
        {
            var client = new HttpClient();
            var res = await client.GetAsync("http://localhost:50374/api/Test");
            return await res.Content.ReadAsStringAsync();
        }
    }


}
