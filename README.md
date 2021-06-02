# Demo about calling Azure Consumption API

Azure has a rich API ecosystem. You can get to control majority of resources in Azure via [Azure Resource Manager web Interface](https://resources.azure.com). 
In some cases you want to incorporate usage/consumption details of important services inside your application or workflows, either via other resource (Azure Functions, Azure Logic App, etc.)
or inside your own code.

This repository contains demo representation of how you can call [Azure Consumption API](https://docs.microsoft.com/en-us/rest/api/consumption/) via C# programming language in [.NET Core](https://dot.net).

## Settings

In order for demo to work, you need to configure environment variables inside [Program.cs](https://github.com/bovrhovn/azure-consumption-api-demos/blob/master/Consumption.Console/Program.cs). 

1. TenantId

TenantId you can get from Azure Active Directory blade inside [Azure Portal](https://portal.azure.com) in Overview tab.

![tenantid information](https://webeudatastorage.blob.core.windows.net/web/azure-consumption-api-demo-tenantid.png)

2. SubscriptionId

SubscriptionId can be found in Subscription blade inside [Azure Portal](https://portal.azure.com) in list subscriptions tab. You can search for *subscriptions* through search box.

![subscriptionId in settings](https://webeudatastorage.blob.core.windows.net/web/azure-consumption-api-demo-subscriptionid.png)

In order to full-fill the ClientId and Secret, you need to follow [this instructions](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal).
Don't forget to use RBAC to allow the newly created application to access subscription information.

## Additional information

if you have any additional questions / comments, feel free to open issue and I'll have a look.
