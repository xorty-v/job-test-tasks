using backend.Entities;
using backend.Infrastructure;
using backend.Infrastructure.Auth;
using Bogus;

namespace backend.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        if (dbContext.Clients.Any() || dbContext.Payments.Any() || dbContext.Users.Any() || dbContext.Rates.Any())
            return;

        var faker = new Faker("ru");
        var clients = faker.GenerateClients(3);
        var payments = faker.GeneratePayments(clients, 5);

        var rate = new Rate
        {
            Id = Guid.NewGuid(),
            Value = 10,
            UpdatedAtOnUtc = DateTime.UtcNow,
        };

        var adminUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = "admin@mirra.dev",
            PasswordHash = passwordHasher.Generate("admin123")
        };

        dbContext.Clients.AddRange(clients);
        dbContext.Payments.AddRange(payments);
        dbContext.Rates.Add(rate);
        dbContext.Users.Add(adminUser);

        dbContext.SaveChanges();
    }

    private static List<Client> GenerateClients(this Faker faker, int count) 
    {
        return faker.Make(count, _ =>
        {
            var person = new Person("ru");

            return new Client
            {
                Id = Guid.NewGuid(),
                Name = person.FullName,
                Email = faker.Internet.Email(person.FirstName, person.LastName),
                BalanceT = faker.Finance.Amount(50, 5000)
            };
        }).ToList();
    }

    private static List<Payment> GeneratePayments(this Faker faker, List<Client> clients, int count)
    {
        return faker.Make(count, _ => new Payment
        {
            Id = Guid.NewGuid(),
            ClientId = faker.PickRandom(clients).Id,
            Amount = faker.Finance.Amount(10, 500),
            TimestampOnUtc = DateTime.SpecifyKind(faker.Date.Past(), DateTimeKind.Utc)
        }).ToList();
    }
}