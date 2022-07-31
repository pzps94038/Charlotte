using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Charlotte.Controllers
{
    public class DashbordHub : Hub
    {
        public async Task WeekSaleRefresh()
        {
            await Clients.All
                .SendAsync("WeekSaleRefresh");
        }

        public async Task MonthSaleRefresh()
        {
            await Clients.All
                .SendAsync("MonthSaleRefresh");
        }

        public async Task RegisteredMemberRefresh()
        {
            await Clients.All
                .SendAsync("RegisteredMemberRefresh");
        }
    }
}
