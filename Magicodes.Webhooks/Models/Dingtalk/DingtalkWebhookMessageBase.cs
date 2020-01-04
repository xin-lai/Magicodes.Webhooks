// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : DingtalkWebhookMessageBase.cs
//           description :
//   
//           created by 雪雁 at  2019-01-08 14:41
//           Mail: wenqiang.li@xin-lai.com
//           QQ群：85318032（技术交流）
//           Blog：http://www.cnblogs.com/codelove/
//           GitHub：https://github.com/xin-lai
//           Home：http://xin-lai.com
//   
// ======================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magicodes.Webhooks.Models.Dingtalk
{
    public abstract class DingtalkWebhookMessageBase : IWebhookMessage
    {
        /// <summary>
        ///     消息类型
        /// </summary>
        [JsonProperty("msgtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DingtalkMessageTypes Type { get; set; }

        /// <summary>
        ///     文本
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public DingtalkTextInfo Text { get; set; }

        /// <summary>
        ///     Markdown
        /// </summary>
        [JsonProperty("markdown", NullValueHandling = NullValueHandling.Ignore)]
        public DingtalkMarkdownInfo Markdown { get; set; }

        [JsonProperty("at", NullValueHandling = NullValueHandling.Ignore)]
        public DingtalkAtInfo At { get; set; }
    }
}