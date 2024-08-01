using GalaSoft.MvvmLight;
using HandyControl.Data;
using HandyControl.Tools.Command;

namespace eraSandBoxWpf.Controls;

public class DataViewModel : DemoViewModelBase<DemoDataModel>
{
    /// <summary>
    ///     所有数据
    /// </summary>
    private readonly List<DemoDataModel> _totalDataList;

    /// <summary>
    ///     页码
    /// </summary>
    private int _pageIndex = 1;

    /// <summary>
    ///     页码
    /// </summary>
    public int PageIndex
    {
        get => this._pageIndex;
        set => this.Set(ref this._pageIndex, value);
    }

    public DataViewModel()
    {
        this._totalDataList = GetDemoDataList(100);
        this.DataList = this._totalDataList.Take(10).ToList();
    }

    public static List<DemoDataModel> GetDemoDataList(int count)
    {
        var list = new List<DemoDataModel>();
        for (var i = 1; i <= count; i++)
        {
            var index = i % 6 + 1;
            var model = new DemoDataModel
            {
                Index = i,
                IsSelected = i % 2 == 0,
                Name = $"Name{i}",
                Type = (DemoType)index,
                ImgPath = $"/HandyControlDemo;component/Resources/Img/Avatar/avatar{index}.png",
                Remark = new string(i.ToString()[0], 10)
            };
            list.Add(model);
        }

        return list;
    }

    public List<DemoDataModel> DataList { get; set; }

    /// <summary>
    ///     页码改变命令
    /// </summary>
    public RelayCommand<FunctionEventArgs<int>> PageUpdatedCmd => new(this.PageUpdated);

    /// <summary>
    ///     页码改变
    /// </summary>
    private void PageUpdated(FunctionEventArgs<int> info)
    {
        this.DataList = this._totalDataList.Skip((info.Info - 1) * 10).Take(10).ToList();
    }
}

public class DemoViewModelBase<T> : ViewModelBase
{
    /// <summary>
    ///     数据列表
    /// </summary>
    private IList<T> _dataList;

    /// <summary>
    ///     数据列表
    /// </summary>
    public IList<T> DataList
    {
        get => this._dataList;
        set => this.Set(ref this._dataList, value);
    }
}

public class DemoDataModel
{
    public int Index { get; set; }

    public string Name { get; set; }

    public bool IsSelected { get; set; }

    public string Remark { get; set; }

    public DemoType Type { get; set; }

    public string ImgPath { get; set; }

    public List<DemoDataModel> DataList { get; set; }
}

public enum DemoType
{
    Type1 = 1,
    Type2,
    Type3,
    Type4,
    Type5,
    Type6
}