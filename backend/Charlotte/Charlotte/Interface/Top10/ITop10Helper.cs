using Charlotte.Interface.Shared;
using Charlotte.VModel.Top10;

namespace Charlotte.Interface.Top10
{
    public interface ITop10Helper
    {
        Task<List<Top10Model>> GetAsync();
    }
}
