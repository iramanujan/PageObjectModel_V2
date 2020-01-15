using Newtonsoft.Json;

namespace WebDriverHelper.Grid
{
    public class BrowsersStatusModel
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "browserSlotsCount")]
        public BrowserSlotsCountModel BrowserSlotsCount { get; set; }
    }
}
