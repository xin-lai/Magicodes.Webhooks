// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : WebHookInfo.cs
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

namespace Magicodes.Webhooks.Models
{
    public class WebHookInfo
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///     处理程序
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        ///     处理程序的webHook地址
        /// </summary>
        public string WebHook { get; set; }

        /// <summary>
        ///     自定义数据，例如： {\"at\":{\"isAtAll\":true}}
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     默认数据（为空则发送默认数据）
        /// </summary>
        public string DefaultMessage { get; set; }

        /// <summary>
        ///     返回数据（不填写则返回默认消息）
        /// </summary>
        public string ReturnData { get; set; }
    }
}