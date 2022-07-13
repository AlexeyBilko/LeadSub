using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;

namespace LeadSub.Models
{
    public class SubscriptionChecker
    {
        public async void GetFollowers()
        {
            var userSession = new UserSessionData
            {
                UserName = "lead.sub",
                Password = "LeadSub123"
            };
            var _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Exceptions))
                .Build();

            if (!_instaApi.IsUserAuthenticated)
            {
                // login
                Console.WriteLine($"Logging in as {userSession.UserName}");
                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                    return;
                }
            }
            var user = await _instaApi.UserProcessor.GetUserAsync("bilkkoo");
            long id = user.Value.Pk;
            Console.WriteLine(id);
            var fullUserInfo = await _instaApi.UserProcessor.GetFullUserInfoAsync(id);
            long amountFollowers = fullUserInfo.Value.UserDetail.FollowerCount;
            Console.WriteLine(amountFollowers);
            int pagesToLoad = Convert.ToInt32(amountFollowers / 100) + 1;
            Console.WriteLine(pagesToLoad);
            var userFollowers = await _instaApi.UserProcessor.GetUserFollowersAsync("bilkkoo", PaginationParameters.MaxPagesToLoad(pagesToLoad));
            var list = userFollowers.Value.ToList();
            Console.WriteLine(list.Count);
            foreach (var item in list)
            {
                Console.WriteLine(item.UserName);
            }
        }
    }
}
