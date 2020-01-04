// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : ProcessMessageLogInfo.cs
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

using System;
using Newtonsoft.Json.Linq;

namespace Magicodes.Webhooks.Models
{
    public class ProcessMessageLogInfo
    {
        public ProcessMessageLogInfo()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     源消息
        /// </summary>

        public JObject SourceMessage { get; set; }

        /// <summary>
        ///     Web Hook配置信息
        /// </summary>

        public WebHookInfo WebHookInfo { get; set; }

        /// <summary>
        ///     Web Hook消息
        /// </summary>

        public IWebhookMessage WebhookMessage { get; set; }

        public string ErrorMesage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}