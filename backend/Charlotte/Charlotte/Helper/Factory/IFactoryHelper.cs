using Charlotte.VModel.Factory;

namespace Charlotte.Helper.Factory
{
    public interface IFactoryHelper
    {
        Task<FactoryVModel> GetFactory(int factoryId);
        Task<List<FactoryVModel>> GetFactorys();
        Task CreateFactory(string factoryName);
        Task ModifyFactory(int factoryId, string factoryName);
        Task DeleteFactory(int factoryId);
        Task BatchDeleteFactory(List<int> factorysId);
    }
}
