using FinanceManagementApp.Data.StockAPI;
using YahooQuotesApi;

namespace FinanceManagementApp.ViewModels;

public class StocksViewModel
{
    #region init
    public StocksViewModel() { }

    public void Init()
    {
        FetchStocks();
        isLoading = false;
    }

    public event Action OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
    #endregion

    #region properties
    private Dictionary<string, YahooQuotesApi.Security?> stocks;
    public Dictionary<string, YahooQuotesApi.Security?> Stocks
    {
        get => stocks;
        set
        {
            stocks = value;
            NotifyStateChanged();
        }
    }

    private bool isLoading = true;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            NotifyStateChanged();
        }
    }
    #endregion

    #region functions
    public async void FetchStocks()
    {
        Stocks = await new StockAPIHelper().FetchDataAsync();
    }
    #endregion
}
