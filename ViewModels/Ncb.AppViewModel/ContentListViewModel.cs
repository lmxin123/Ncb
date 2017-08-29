using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Framework.Common;

namespace Ncb.AppViewModels
{
    public class ContentListViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "createTime")]
        public string CreateTime { get; set; }
        [JsonProperty(PropertyName = "accessType")]
        public AccessTypes AccessType { get; set; }
        [JsonProperty(PropertyName = "isFree")]
        public bool IsFree { get; set; }
        [JsonProperty(PropertyName = "freeDate")]
        public string FreeDate { get; set; }
    }
}