## Net Core下的控制台应用程序模板

#### 为什么要建这个项目

VS自带的控制台模板创建后没有依赖注入，没有配置文件，所以这里集成了依赖注入和配置文件，还集成了线程池下多线程的使用方法，这个项目适合做一些主项目的辅助性工作，比如爬虫



#### Windows计划任务执行该程序

如果使用定时任务来启动这个项目，注意配置完该程序的目录后，下面有个  `“起始于(可选)T”` ，填写程序的根目录，不然会报错，而且，程序中获取程序目录要使用拓展方法：`"".GetProgramDirectory()`



## 此为项目的模板文件，直接导出项目模板就行了

## Publish 2021/07/10

`NetCoreConsoleAppPrefect.zip` 为导出的模板文件，放到`C:\Users\litdev\Documents\Visual Studio 2019\Templates\ProjectTemplates` 这个目录下
