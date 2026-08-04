# Playwright Foundations in C# / .NET

## Getting Started

This is an Aspire-based solution, so you should be able to run it
as long as you have [the Aspire prerequisites](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling?tabs=windows&pivots=visual-studio#install-net-aspire)
installed.

If you're running the solution for the first time, there may be some
extra startup time pulling the container images being used:

- `postgres` (the database)
- `smtp4dev` (a fake / dev email server with a UI)

To make sure you can do everything with Playwright, check [its
setup requirements](https://playwright.dev/dotnet/docs/intro).

## Features

This is a simple e-commerce application that has a few features
that we want to explore automated end-to-end testing strategies for
with Playwright.

Here are the features:

- **API**
  - `GET` based on category (or "all") and by id allow anonymous requests
  - `POST` requires authentication and an `admin` role
  - Validation will be done with [FluentValidation](https://docs.fluentvalidation.net/en/latest/index.html) and errors returned as a `400 Bad Request` with `ProblemDetails`
  - A `GET` with a category of something other than "all", "boots", "equip", or "kayak" will return a `500 internal server error` with `ProblemDetails`
  - Data is refreshed to a known state as the app starts
- Authentication provided by OIDC via the [demo Duende Identity Server](https://demo.duendesoftware.com)
- A custom claims transformer will add the `admin` role to "Bob Smith" and any authentication via Google
- **WebApp**
  - The home page and listing pages will show a subset of products
  - There is a page at `/Admin` that will show a list of products that can be edited or added to
  - If you navigate to `/Admin` without the admin role, you should see an `AccessDenied` page
  - Any validation errors from the API should be displayed on the admin section edit pages
  - Can add items to cart and see a summary of the cart (shows when empty too)
  - Can submit an order or cancel the order and clear the cart
  - A submitted order will send a fake email

If you want to "catch" a breaking change with a refactor:

- change the "admin" role to be "administrator" and see what breaks
(maybe make it a different claim type than role and change to a "policy"??)

Ideas, if you want to add more features to test:

- Add edit and (soft) delete to the API and WebApp, then write tests
- More complex "cart edit" functionality
- Be able to apply a "promotion" on the Cart page

## VS Code Setup

The same instructions as above (Getting Started) apply here,
but the following extension should be installed
(it includes some other extensions):

- [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

Then just hit `F5`.

## Data and EF Core Migrations

The `dotnet ef` tool is used to manage EF Core migrations.  The following command is used to create migrations (from the `CarvedRock.Data` folder).

```bash
dotnet ef migrations add Initial -s ../CarvedRock.Api
```

The database used by the application is Postgres.

To browse / query the data, you can comment in the `.WithPgAmin()` line in the `AppHost`:

## Verifiying Emails

The very simple email functionality is done using a template
from [this GitHub repo](https://github.com/leemunroe/responsive-html-email-template)
and the [smtp4dev](https://github.com/rnwood/smtp4dev)
service that we're running in Aspire using its container image.

Click the link in the Aspire Dashboard for the smtpserver and you can
see any emails that have been sent by the application (they don't go
anywhere other than this UI).
