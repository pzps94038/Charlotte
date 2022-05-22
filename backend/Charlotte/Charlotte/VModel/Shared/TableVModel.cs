namespace Charlotte.VModel.Shared
{
    public class TableVModel<T>
    {
        public List<T> TableDataList { get; set; }
        public int TableTotalCount { get; set; }
        public TableVModel(List<T> data, int totalCount)
        {
            this.TableDataList = data;
            this.TableTotalCount = totalCount;
        }
    }
}
