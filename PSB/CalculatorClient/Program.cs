using System.Net;

Console.Write("Введите первое число: ");
double x = Convert.ToDouble(Console.ReadLine());

Console.Write("Введите второе число: ");
double y = Convert.ToDouble(Console.ReadLine());

Console.Write("1. Сложение" +
              "\n2. Вычитание" +
              "\n3. Умножение" +
              "\n4. Деление" +
              "\nВведите число желаемой операции: ");

int operationNumber = int.Parse(Console.ReadLine());

string operation = operationNumber switch
{
    1 => "add",
    2 => "subtract",
    3 => "multiply",
    4 => "divide",
    _ => throw new InvalidOperationException("Invalid operation number")
};

string url = $"https://localhost:5001/api/calculator/{operation}?x={x}&y={y}";

try
{
    using var httpClient = new HttpClient();
    HttpResponseMessage response = await httpClient.GetAsync(url);

    if (response.StatusCode != HttpStatusCode.OK)
    {
        string error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[ServerError]: {error}");
        return;
    }

    response.EnsureSuccessStatusCode();
    string result = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"Ответ: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error]: {ex.Message}");
}