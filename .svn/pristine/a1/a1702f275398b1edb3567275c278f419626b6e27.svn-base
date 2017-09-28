using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace YY.Edu.Sys.Api.Providers
{

    /// <summary>
    /// 自定义上传文件名provider
    /// </summary>
    public class MultipartFileWithExtensionStreamProvider : MultipartFormDataStreamProvider
    {

        // 摘要:   
        //     初始化 System.Net.Http.MultipartFileStreamProvider 类的新实例。  
        //  
        // 参数:   
        //   rootPath:  
        //     MIME 多部分正文部分的内容写入到的根路径。  
        public MultipartFileWithExtensionStreamProvider(string rootPath)
            : this(rootPath, 4096)
        {
        }

        //  
        // 摘要:   
        //     初始化 System.Net.Http.MultipartFileStreamProvider 类的新实例。  
        //  
        // 参数:   
        //   rootPath:  
        //     MIME 多部分正文部分的内容写入到的根路径。  
        //  
        //   bufferSize:  
        //     为写入到文件而缓冲的字节数。  
        public MultipartFileWithExtensionStreamProvider(string rootPath, int bufferSize)
            : base(rootPath, bufferSize)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return base.GetLocalFileName(headers) + Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"",""));
        }

    }

}