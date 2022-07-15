﻿using FinanceManagementApp.Data.StockAPI.Models;
using NodaTime;
using System.Diagnostics;
using YahooQuotesApi;

namespace FinanceManagementApp.Data.StockAPI
{
    public class StockAPIHelper
    {
        private readonly YahooQuotes yahooAPI;

        public StockAPIHelper(DateTime? startDate = null, Frequency frequency = Frequency.Monthly)
        {
            var date = startDate ?? new DateTime(2020, 1, 1);
            yahooAPI = new YahooQuotesBuilder()
                .WithHistoryStartDate(Instant.FromUtc(date.Year, date.Month, date.Day, date.Hour, date.Minute))
                .WithPriceHistoryFrequency(frequency)
                .Build();
        }

        /// <summary>
        /// array of all stocks we want to monitor
        /// TODO - make list dynamic (user selects and saves?)
        /// </summary>
        private static readonly string[] _stocks = new[]
        {
            "AAPL",
            "GOOG"
        };

        /// <summary>
        /// fetch latest data for all stocks
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, YahooQuotesApi.Security?>> FetchDataAsync()
        {
            var stock_response = await yahooAPI.GetAsync(_stocks, HistoryFlags.All);
            return stock_response;
        }
    }
}
