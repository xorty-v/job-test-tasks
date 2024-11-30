using System.Text;
using System.Xml;
using static System.Text.Encoding;

RegisterProvider(CodePagesEncodingProvider.Instance);
const string url = "https://www.cbr.ru/scripts/XML_daily.asp";

Console.Write("Введите код валюты: ");
string? currencyCode = Console.ReadLine()!.ToUpper();

if (string.IsNullOrEmpty(currencyCode))
    throw new ArgumentNullException("Код валюты не может быть null");

var xmlDocument = await GetXmlDocument(url);
string value = ParseCurrencyByCode(xmlDocument);

Console.WriteLine(value);


async Task<XmlDocument> GetXmlDocument(string url)
{
    using var client = new HttpClient();

    using var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    await using var stream = await response.Content.ReadAsStreamAsync();

    var xmlDocument = new XmlDocument();
    xmlDocument.Load(stream);

    return xmlDocument;
}

string ParseCurrencyByCode(XmlDocument xmlDocument)
{
    XmlNodeList valuteNodes = xmlDocument.GetElementsByTagName("Valute");

    foreach (XmlNode node in valuteNodes)
    {
        string code = node.SelectSingleNode("CharCode").InnerText;
        if (code == currencyCode)
        {
            string value = node.SelectSingleNode("Value").InnerText;

            return $"{currencyCode} — {value} руб";
        }
    }

    return "Код валюты не найден!";
}