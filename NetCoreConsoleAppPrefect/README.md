## Net Core下的控制台应用程序模板

#### 为什么要建这个项目

VS自带的控制台模板创建后没有依赖注入，没有配置文件，所以这里集成了依赖注入和配置文件，还集成了线程池下多线程的使用方法，这个项目适合做一些主项目的辅助性工作，比如爬虫

#### 代码生成

使用  [Mustachio]( https://github.com/wildbit/mustachio) 生成Service层代码

`Main`方法中添加下面代码

```c#
//Service层代码生成
var codeGenerateService = ActivatorUtilities.CreateInstance<CodeGenerateService>(host.Services);
codeGenerateService.GenerateBusinessServiceFile("User", "用户");//第一个参数类名/文件名,第二个参数备注
```

循环使用官方示例

```c#
//参数JSON，可使用model，然后Json序列化
{
    "company_name" : "ACME Rockets, Inc.",
    "employees" : [
         { "name" : "Wile E. Coyote"},
         { "name" : "Road Runner"}
    ]
}
//模板中使用
{{ company_name }} Employees:
<ul>
{{#each employees}}
<li>{{ name }}</li>
{{/each}}
</ul>
```

#### Windows计划任务执行该程序

如果使用定时任务来启动这个项目，注意配置完该程序的目录后，下面有个  `“起始于(可选)T”` ，填写程序的根目录，不然会报错，而且，程序中获取程序目录要使用拓展方法：`"".GetProgramDirectory()`

> 已知在Windows Server 2019下计划任务无法启动应用程序，Windows Server 2016下正常，应该是我装的Windows Server 2019这个有问题

## 此为项目的模板文件，直接导出项目模板就行了