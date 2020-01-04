// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : DingtalkModel.cs
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
    /// <summary>
    /// </summary>
    public class DingtalkAtInfo
    {
        /// <summary>
        ///     被@人的手机号
        /// </summary>
        [JsonProperty("atMobiles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] AtMobiles { get; set; }

        /// <summary>
        ///     @所有人时:true,否则为:false
        /// </summary>
        [JsonProperty("isAtAll")]
        public bool IsAtAll { get; set; }
    }
}