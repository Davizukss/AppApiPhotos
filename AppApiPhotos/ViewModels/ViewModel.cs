using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppApiPhotos.Models;

namespace AppApiPhotos.ViewModels
{
    public class ViewModel : ObservableObject
    {
        private RestService _restService;

        public ObservableCollection<Post> Posts { get; } = new ObservableCollection<Post>();

        public ViewModel()
        {
            _restService = new RestService();
            LoadPhotosCommand = new AsyncRelayCommand(CarregarImgs);
            CarregarImgs();
        }

        public IAsyncRelayCommand LoadPhotosCommand { get; }

        private async Task CarregarImgs()
        {
            var posts = await _restService.GetPostsAsync();
            Posts.Clear();
            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }
    }
}
