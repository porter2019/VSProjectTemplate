using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using $safeprojectname$.IServices;
using System.IO;

namespace $safeprojectname$.Services
{
    /// <summary>
    /// 代码生成
    /// </summary>
    public class CodeGenerateService : BaseBusinessService, ICodeGenerateService
    {
        public CodeGenerateService(ILogger<CodeGenerateService> logger, IConfiguration config) : base(logger, config)
        {

        }

        /// <summary>
        /// 生成通用业务服务文件
        /// </summary>
        /// <param name="serviceName">服务类名</param>
        /// <param name="serviceDesc">服务说明</param>
        /// <param name="serviceName">服务文件名称</param>
        public void GenerateBusinessServiceFile(string serviceName, string serviceDesc)
        {
            var iServiceTemplate = ReadTemplateFile("IBusinessService.txt");

            var template = Mustachio.Parser.Parse(iServiceTemplate);
            // 创建模板数据，也可以是 Dictionary<string,object> 类型的
            dynamic model = new ExpandoObject();
            model.ServiceName = serviceName;
            model.ServiceDesc = serviceDesc;

            // 解析获取最终数据
            var content = (string)template(model);
            //保存IService文件
            SaveCodeToFile(content, $"I{serviceName}Service.cs", "IServices");

            var serviceTemplate = ReadTemplateFile("BusinessService.txt");
            template = Mustachio.Parser.Parse(serviceTemplate);
            content = (string)template(model);
            //保存Service文件
            SaveCodeToFile(content, $"{serviceName}Service.cs", "Services");

            _logger.LogInformation($"{serviceName} Service代码生成完毕");

        }

        /// <summary>
        /// 保存代码到文件中
        /// </summary>
        /// <param name="content">代码内容</param>
        /// <param name="fileName">文件名</param>
        /// <param name="folderName">目标文件夹</param>
        void SaveCodeToFile(string content, string fileName, string folderName)
        {
            var targetFilePath = Path.Combine(GetProjectDirectory(), folderName, fileName);
            if (File.Exists(targetFilePath))
            {
                _logger.LogError($"{targetFilePath}文件已存在，跳过保存");
                return;
            }

            using (var targetFileInfo = File.Create(targetFilePath))
            {
                var writer = new StreamWriter(targetFileInfo, Encoding.UTF8);
                writer.Write(content);
                writer.Dispose();
            }

        }

        /// <summary>
        /// 读取模板内容
        /// </summary>
        /// <param name="templateFileName">模板文件名称</param>
        /// <returns></returns>
        string ReadTemplateFile(string templateFileName)
        {
            return File.ReadAllText(Path.Combine(GetProjectDirectory(), "Template", templateFileName));
        }

        /// <summary>
        /// 获取项目根目录
        /// </summary>
        /// <returns></returns>
        string GetProjectDirectory()
        {
            var currentDir = "".GetProgramDirectory();
            var binIndex = currentDir.IndexOf("\\bin\\");
            if (binIndex == -1)
                return currentDir;
            else
            {
                return currentDir.Substring(0, binIndex);
            }
        }

    }
}
