namespace Charlotte.Interface.Dashbord
{
    public interface IDashbordHelper
    {
        Task<object> WeekSale();
        Task<object> MonthSale();

        Task<object> RegisteredMember();
    }
}
