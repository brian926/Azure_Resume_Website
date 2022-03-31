# Azure Hosted Resume Website

 ## Summary
 Personal resume website being hosted completely on Azure. This is part of the Azure Resume Challenge, where the goal is to create a 100% Azure-hosted version of your resume.
 View it live [here](https://www.brianantunes.com)
 A summary of it is a static site deployed to Blob Storage. That static site also fetches a visitor count from a Azure Function API to show a visitor count.
 That Azure Function API retrieves data from a Azure Cosmos DB and passes the count of visitors in the database.
 Lastly, Github Actions is used to create a CI/CD to deploy all changes to the front-end and/or back-end in Azure automatically.

 ## Resources
 The front-end is a static site being hosted with Azure Blob Storage. Here are some of the resources I used.
 - HTML and CSS templated used from [html5up](https://html5up.net) and modified to fix my own design
 - How to [deploy a static site to Azure Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website-host)

 Then create a Azure CDN endpoint
 - Integrate a static website with [Azure CDN and create an endpoint](https://docs.microsoft.com/en-us/azure/storage/blobs/static-website-content-delivery-network)
 - Map a custom domain to your [Azure Blob Storage endpoint](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-custom-domain-name?tabs=azure-portal)

 The website then uses [Javascript to make a API call](https://www.digitalocean.com/community/tutorials/how-to-use-the-javascript-fetch-api-to-get-data)
 That API is a Azure Function, a HTTP triggered Azure Function. It has a Cosmos DB input and output binding to retrieve visitor count and update it to the new count.
 - Create a [HTTP triggered Azure Function in Visual Studio](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp)
 - [Azure Functions Cosmos DB bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2)
 - Example on how to [Retrieve a Cosmos DB item with Functions binding.](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-input?tabs=csharp)
 - Example on how to [Write to a Cosmos DB item with Functions binding.](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-output?tabs=csharp)
 - You'll have to enable [CORS with Azure Functions locally](https://github.com/Azure/azure-functions-host/issues/1012) and once it's [deployed to Azure](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings?tabs=portal#cors) for you website to be able to call it.
 ```
 func host start --cors *
 ```

 Lastly, the Github actions that create a CI/CD pipeline with Azure so any changes to the code is automatically updated in Azure
 - This is how you can deploy a blob storage static site with [GitHub actions.](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-static-site-github-actions)
 - This is how you can [deploy an Azure Function to Azure with GitHub Actions.](https://github.com/marketplace/actions/azure-functions-action)