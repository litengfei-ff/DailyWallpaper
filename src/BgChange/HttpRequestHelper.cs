using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BgChange
{
    /// <summary>
    /// HTTP请求静态类
    /// </summary>
    public class HttpRequestHelper
    {

        public static async Task<byte[]> GetResponse(string requestUrl)
        {
            var req = (HttpWebRequest)WebRequest.Create(requestUrl);
            var res = await req.GetResponseAsync();

            Stream resStream = res.GetResponseStream();

            int count = (int)res.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            {
                int n = resStream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }

            return buf;
        }
    }
}
