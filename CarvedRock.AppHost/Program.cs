var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db")
        //.WithPgAdmin() // comment in if you want to explore the data with pgadmin
        .AddDatabase("CarvedRockPostgres", "carved_rock");

var api = builder.AddProject<Projects.CarvedRock_Api>("carvedrock-api")
    .WaitFor(db)
    .WithReference(db);

var email = builder.AddContainer("smptserver", "rnwood/smtp4dev")
    .WithHttpEndpoint(targetPort: 80, name: "http")
    .WithEndpoint(targetPort: 25, name: "smtp");

builder.AddProject<Projects.CarvedRock_WebApp>("carvedrock-webapp")
    .WithEnvironment("CarvedRock:ApiBaseUrl", api.GetEndpoint("https"))
    .WithEnvironment("CarvedRock:SmtpServer", email.GetEndpoint("smtp"));

builder.Build().Run();
