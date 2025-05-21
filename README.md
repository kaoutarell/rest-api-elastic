# 📚 Simple Book API with Elasticsearch Integration

This is a small RESTful API built with .NET 8 that connects to an Elasticsearch cluster using the official NEST client. It allows interaction with book data and demonstrates modern API development practices.

## 🚀 Features
✅ RESTful API endpoints (/api/books)
✅ Connected to a cloud-hosted Elasticsearch instance
✅ Swagger UI integration for testing
✅ Configurable via environment variables for secrets and URIs

## 🛠 Technologies Used
ASP.NET Core 8
Elasticsearch (with NEST .NET client) - on a 14-days-trial
Swagger (via Swashbuckle)
Environment variables for secure config injection

## ⚙️ How It Works
1. The IElasticClient is configured and injected as a singleton, ensuring a single shared connection configuration across the entire app.
2. Swagger is used to expose and interact with the API endpoints in development.
3. Environment variables are used to securely inject Elasticsearch credentials and URI without hardcoding sensitive data.

## 🧠 What I Learned through debugging ...
1. Singleton for Elasticsearch Config
Used builder.Services.AddSingleton<IElasticClient>(...) to ensure a single entry point to the ES connection settings and credentials. This improves performance and keeps the configuration centralized.
👉🏻 Program.cs
2. Swagger Integration for API Testing
Added Swagger for documentation and quick testing
3. Environment Variables in .NET & Rider
Environment variables must be set before running the app, and Rider does not inherit your shell’s exported variables.
✅ Working in Terminal:
```
export ELASTICSEARCH_URI="https://the-cloud-uri:9200"
export ELASTIC_API_KEY_ID="the-api-id"
export ELASTIC_API_KEY_SECRET="the-api-secret"
dotnet run
```
