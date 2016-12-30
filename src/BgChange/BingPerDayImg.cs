using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BgChange
{
    /// <summary>
    /// Bing每日图片
    /// </summary>
    public class BingPerDayImg
    {

        private string bingUrl = "http://cn.bing.com/";
        private string bingAPIUrl = "HPImageArchive.aspx?format=js&idx=0&n=1";
        private string jsonToken = "images[0].url";

        private async Task<string> GetImgUrl()
        {
            var res = await HttpRequestHelper.GetResponse(bingUrl + bingAPIUrl);
            var resJsonString = Encoding.UTF8.GetString(res);
            var resData = JObject.Parse(resJsonString);
            var resUrl = resData.SelectToken(jsonToken).ToString();
            return resUrl;
        }

        /// <summary>
        /// 获取Bing每日图片 ，保存到指定路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="callBack">回掉方法</param> 
        /// <param name="ErrorHandler">异常处理</param> 
        public async void GetImg(string filePath, Action callBack, Action<Exception> ErrorHandler)
        {
            try
            {
                var imgUrl = await GetImgUrl();
                var resBytes = await HttpRequestHelper.GetResponse(bingUrl + imgUrl);

                Image img = Bitmap.FromStream(new MemoryStream(resBytes));
                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);

                // 调用回调方法
                callBack();
            }
            catch (Exception e)
            {
                ErrorHandler(e);
            }
        }

    }
}
