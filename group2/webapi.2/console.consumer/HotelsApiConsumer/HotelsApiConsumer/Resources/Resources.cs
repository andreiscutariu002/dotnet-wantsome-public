namespace HotelsApiConsumer.Resources
{
    namespace HotelsApiConsumer.Resources
    {
        using System.CodeDom.Compiler;
        using System.Collections.Generic;
        using Newtonsoft.Json;

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class HotelResource
        {
            [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public int Id { get; set; }

            [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("city", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string City { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static HotelResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<HotelResource>(data);
            }
        }

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class UpdateHotelResource
        {
            [JsonProperty("city", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string City { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static UpdateHotelResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<UpdateHotelResource>(data);
            }
        }

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class CreateHotelResource
        {
            [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty("city", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string City { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static CreateHotelResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<CreateHotelResource>(data);
            }
        }

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class RoomResource
        {
            [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public int Id { get; set; }

            [JsonProperty("number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public string Number { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static RoomResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<RoomResource>(data);
            }
        }

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class CreateRoomResource
        {
            [JsonProperty("number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public string Number { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static CreateRoomResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<CreateRoomResource>(data);
            }
        }

        [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
        public class UpdateRoomResource
        {
            [JsonProperty("number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
            public string Number { get; set; }

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static UpdateRoomResource FromJson(string data)
            {
                return JsonConvert.DeserializeObject<UpdateRoomResource>(data);
            }
        }
    }
}
