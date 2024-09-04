using NemoTravel.FinanceCore.Entities;
using NemoTravel.FinanceCore.Infrastructure;
using NemoTravel.FinanceCore.Services;

HttpClient httpClient = new HttpClient();
IExchangeRateProvider exchangeRateProvider = new ExchangeRateApiProvider(httpClient);
ICurrencyConverterService currencyConverterService = new CurrencyConverterService(exchangeRateProvider);
IMoneyOperationService moneyOperationService = new MoneyOperationService(currencyConverterService);

var moneyUsd = new Money(150, "USD");
var moneyEur = new Money(100, "EUR");

var сonverterUsdToEur = await currencyConverterService.Convert(moneyEur, moneyUsd.Currency);
var UsdAddEur = await moneyOperationService.Add(moneyUsd, moneyEur);

Console.WriteLine(сonverterUsdToEur.ToString());
Console.WriteLine(UsdAddEur.ToString());