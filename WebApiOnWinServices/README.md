## WebApi宿主在Windows服务上

1. 程序包管理器控制台下输入命令
	Update-Package -reinstall
	指定项目名： Update-Package -ProjectName 项目名称 -reinstall
2. 删除App_Start目录
3. WinInstall.bat 中修改exe文件名和服务名 binpath
4. 修改App.config中的端口

## Publish 2021/07/10

`WebApiOnWindowsServices.zip` 为导出的模板文件，放到`C:\Users\litdev\Documents\Visual Studio 2019\Templates\ProjectTemplates` 这个目录下
