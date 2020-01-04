// ======================================================================
//   
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : DingtalkWebhookTextMessage.cs
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

namespace Magicodes.Webhooks.Models.Dingtalk
{
    /// <summary>
    ///     文本消息
    /// </summary>
    public class DingtalkWebhookTextMessage : DingtalkWebhookMessageBase
    {
        public DingtalkWebhookTextMessage(string text)
        {
            Type = DingtalkMessageTypes.text;
            Text = new DingtalkTextInfo
            {
                Content = text
            };
        }
    }
}