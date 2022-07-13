using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;

namespace LeadSub.Models
{
    public class SubscriptionChecker
    {
        public async Task<bool> GetFollowers(string subscribeTo, string username)
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
                    return false;
                }
                else Console.WriteLine($"Logged In successfully");
            }

            //var user = await _instaApi.UserProcessor.GetUserAsync(subscribeTo);
            //long id = user.Value.Pk;
            //Console.WriteLine(id);
            //var fullUserInfo = await _instaApi.UserProcessor.GetFullUserInfoAsync(id);
            //long amountFollowers = fullUserInfo.Value.UserDetail.FollowerCount;
            //Console.WriteLine(amountFollowers);
            //int pagesToLoad = Convert.ToInt32(amountFollowers / 100) + 1;
            //Console.WriteLine(pagesToLoad);

            var userFollowers = await _instaApi.UserProcessor.GetUserFollowersAsync(subscribeTo, PaginationParameters.MaxPagesToLoad(1));
            foreach (var item in userFollowers.Value)
            {
                if (item.UserName == username) return true;
            }
            return false;
        }
    }
}
