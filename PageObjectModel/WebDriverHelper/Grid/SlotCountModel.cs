using Newtonsoft.Json;

namespace WebDriverHelper.Grid
{
    public class SlotCountModel
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "free")]
        public int Free { get; set; }
    }
}
