using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Like
{
    public class LikeViewComponent(ILikeStorage likeStorage, UserManager<User> usersManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var like = await likeStorage.GetAsync(user?.Id ?? new());

            var likeCount = like?.Products.Count ?? 0;

            return View("Like", likeCount);
        }
    }
}
