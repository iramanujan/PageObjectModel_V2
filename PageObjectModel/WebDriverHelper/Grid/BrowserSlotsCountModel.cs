using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebDriverHelper.Grid
{
    public class BrowserSlotsCountModel
    {
        [JsonProperty(PropertyName = "CHROME")]
        public SlotCountModel Chrome { get; set; }

        [JsonProperty(PropertyName = "FIREFOX")]
        public SlotCountModel Firefox { get; set; }

        [JsonProperty(PropertyName = "INTERNET EXPLORER")]
        public SlotCountModel InternetExplorer { get; set; }

        [JsonProperty(PropertyName = "MICROSOFTEDGE")]
        public SlotCountModel Edge { get; set; }

        [JsonProperty(PropertyName = "PHANTOMJS")]
        public SlotCountModel PhantomJS { get; set; }
    }
}
