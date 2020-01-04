using Html2Markdown;
using Magicodes.Webhooks.Models;
using Magicodes.Webhooks.Models.Dingtalk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Magicodes.Webhooks.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        public static List<WebHookInfo> WebHookInfos = new List<WebHookInfo>();
        private static readonly HttpClient Client = new HttpClient();
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger) => _logger = logger;

        [HttpGet("")]
        public IActionResult Index() => Ok("start...");

        [HttpPost("{appId}")]
        public async Task<IActionResult> Index(string appid, [FromBody]JObject data)
        {
            //Request.Headers

            var logInfo = new ProcessMessageLogInfo()
            {
                SourceMessage = data
            };
            var webhookInfo = WebHookInfos.FirstOrDefault(p => p.AppId.Equals(appid, System.StringComparison.CurrentCultureIgnoreCase));
            if (webhookInfo == null)
            {
                return ReturnError($"找不到对应的配置!AppId:{appid}", logInfo);
            }

            logInfo.WebHookInfo = webhookInfo;

            string key = null;

            if (!string.IsNullOrWhiteSpace(data["publisherId"]?.ToString()))
            {
                key = data["publisherId"].ToString();
            }
            else if (!string.IsNullOrEmpty(data["@type"]?.ToString()))
            {
                key = data["@type"].ToString();
            }
            else
            {
                var agent = Request.Headers["User-Agent"];
                if (agent == "git-oschina-hook")
                {
                    key = agent;
                }
                else
                {
                    return ReturnError($"没有适配的处理程序，请联系xinlai@xin-lai.com!", logInfo);
                }
            }

            switch (key)
            {
                case "rm":
                case "tfs":
                    {
                        //支持text\markdown\html
                        switch (webhookInfo.Provider)
                        {
                            case "dingtalk":
                                {
                                    var markDown = data["detailedMessage"]["markdown"]?.ToString() ?? data["message"]["markdown"]?.ToString();
                                    if (!string.IsNullOrWhiteSpace(markDown))
                                    {
                                        return await PostMessage(webhookInfo, new DingtalkWebhookMarkdownMessage(markDown, markDown), logInfo);
                                    }

                                    var text = data["detailedMessage"]["text"]?.ToString() ?? data["message"]["text"]?.ToString();
                                    if (!string.IsNullOrWhiteSpace(text))
                                    {
                                        return await PostMessage(webhookInfo, new DingtalkWebhookTextMessage(text), logInfo);
                                    }
                                    //TODO：html转MD

                                    break;
                                }
                        }
                        break;
                    }
                //码云
                case "git-oschina-hook":
                //自定义
                case "magicodes":
                    {
                        var msgtype = data["msgtype"]?.ToString();
                        switch (msgtype)
                        {
                            case "text":
                                {
                                    return await PostMessage(webhookInfo, new DingtalkWebhookTextMessage(data["content"]?.ToString()), logInfo);
                                }
                            case "markdown":
                                {
                                    var title = data["title"]?.ToString();
                                    var content = data["content"]?.ToString();
                                    return await PostMessage(webhookInfo, new DingtalkWebhookMarkdownMessage(title, content), logInfo);
                                }
                        }
                        break;
                    }
                case "MessageCard":
                    {
                        if (data["sections"] != null)
                        {
                            var section = data["sections"][0];
                            var title = section["title"]?.ToString();
                            var activityImage = section["activityImage"]?.ToString();
                            var activityTitle = section["activityTitle"]?.ToString();
                            var activitySubtitle = section["activitySubtitle"]?.ToString();
                            //var themeColor = data["themeColor"]?.ToString();

                            var dTitle = data["summary"]?.ToString();
                            var dMarkdown = $"{title}<img src=\"{activityImage}\" alt=\"{activityTitle}\"><br>{activitySubtitle}";
                            var converter = new Converter();
                            dMarkdown = converter.Convert(dMarkdown);
                            return await PostMessage(webhookInfo, new DingtalkWebhookMarkdownMessage(dTitle, dMarkdown), logInfo);
                        }
                        break;
                    }
            }
            //if (data["message"] != null)
            //{
            //    var markdown = $"### 【阿里云监控】{data["metricName"]} \n\r";
            //}
            return ReturnError($"没有适配的处理程序!AppId:{appid}", logInfo);
        }

        private ActionResult ReturnError(string message, ProcessMessageLogInfo logInfo)
        {
            logInfo.ErrorMesage = message;
            _logger.LogError(message);
            //await ProduceLoginfo(logInfo);
            return BadRequest(message);
        }

        /// <summary>
        /// 提交消息
        /// </summary>
        /// <param name="webhookInfo"></param>
        /// <param name="message"></param>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        private async Task<ActionResult> PostMessage(WebHookInfo webhookInfo, IWebhookMessage message, ProcessMessageLogInfo logInfo)
        {
            logInfo.WebhookMessage = message;
            if (logInfo.SourceMessage["at"] != null)
            {
                if (message is DingtalkWebhookMessageBase dingtalkMessage)
                {
                    dingtalkMessage.At = logInfo.SourceMessage["at"].ToObject<DingtalkAtInfo>();
                }
            }
            else if (!string.IsNullOrWhiteSpace(webhookInfo.Data))
            {
                var data = JsonConvert.DeserializeObject<JObject>(webhookInfo.Data);
                if (message is DingtalkWebhookMessageBase dingtalkMessage)
                {
                    if (data["at"] != null)
                    {
                        dingtalkMessage.At = JsonConvert.DeserializeObject<DingtalkAtInfo>(data["at"].ToString());
                        logInfo.WebhookMessage = dingtalkMessage;
                    }
                }
            }

            var response = await Client.PostAsJsonAsync(
                webhookInfo.WebHook, message);
            if (response.IsSuccessStatusCode)
            {
                logInfo.IsSuccess = true;

                _logger.LogDebug(JsonConvert.SerializeObject(logInfo));

                //TODO:存盘
                //await ProduceLoginfo(logInfo);

                //如果返回数据不为空，则返回默认的配置返回数据
                return !string.IsNullOrWhiteSpace(webhookInfo.ReturnData) ? Ok(webhookInfo.ReturnData) : Ok(new { success = true });
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                return ReturnError(result, logInfo);
            }
        }
    }
}