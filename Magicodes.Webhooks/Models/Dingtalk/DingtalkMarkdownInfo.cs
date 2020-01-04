// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : DingtalkMarkdownInfo.cs
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

namespace Magicodes.Webhooks.Models.Dingtalk
{
    public class DingtalkMarkdownInfo
    {
        /// <summary>
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}