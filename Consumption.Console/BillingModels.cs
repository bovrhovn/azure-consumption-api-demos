using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Consumption.Console
{
    public class Tags    {
        [JsonProperty("web")]
        public string Web; 

        [JsonProperty("data")]
        public string Data; 

        [JsonProperty("ms-resource-usage")]
        public string MsResourceUsage; 
    }

    public class ResourceProperty   
    {
        [JsonProperty("billingPeriodId")]
        public string BillingPeriodId; 

        [JsonProperty("usageStart")]
        public DateTime UsageStart; 

        [JsonProperty("usageEnd")]
        public DateTime UsageEnd; 

        [JsonProperty("instanceId")]
        public string InstanceId; 

        [JsonProperty("instanceName")]
        public string InstanceName; 

        [JsonProperty("instanceLocation")]
        public string InstanceLocation; 

        [JsonProperty("meterId")]
        public string MeterId; 

        [JsonProperty("usageQuantity")]
        public double UsageQuantity; 

        [JsonProperty("pretaxCost")]
        public double PretaxCost; 

        [JsonProperty("currency")]
        public string Currency; 

        [JsonProperty("additionalProperties")]
        public string AdditionalProperties; 

        [JsonProperty("isEstimated")]
        public bool IsEstimated; 

        [JsonProperty("subscriptionGuid")]
        public string SubscriptionGuid; 

        [JsonProperty("subscriptionName")]
        public string SubscriptionName; 

        [JsonProperty("product")]
        public string Product; 

        [JsonProperty("consumedService")]
        public string ConsumedService; 

        [JsonProperty("partNumber")]
        public string PartNumber; 

        [JsonProperty("resourceGuid")]
        public string ResourceGuid; 

        [JsonProperty("offerId")]
        public string OfferId; 

        [JsonProperty("chargesBilledSeparately")]
        public bool ChargesBilledSeparately; 

        [JsonProperty("meterDetails")]
        public object MeterDetails; 
    }

    public class UsageResource    {
        [JsonProperty("id")]
        public string Id; 

        [JsonProperty("name")]
        public string Name; 

        [JsonProperty("type")]
        public string Type; 

        [JsonProperty("tags")]
        public Tags Tags; 

        [JsonProperty("properties")]
        public ResourceProperty Properties; 
    }

    public class BillingModel    {
        [JsonProperty("value")]
        public List<UsageResource> UsageList; 
    }
}