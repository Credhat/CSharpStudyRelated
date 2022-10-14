# Csharp Git .etc

##### 1. 获取源码

`git clone https://git.xxx.com/test.git`

##### 2. 进入代码目录

`cd test\src`

##### 3. 添加自定义nuget，如果不使用，则不需要；已经存在可略过

`dotnet nuget add source <http://nuget.mynuget.cn/nuget> --name=mynuget`

##### 4. 还原库文件

`dotnet restore .  #有点号`

##### 5. 生成测试,没问题后，可发布

`dotnet build .   #有点号`

##### 6. 发布输出到指定到文件夹，Debug模式【可选Release】

`dotnet publish --configuration=Debug  --output=D:\TempFIles  --framework=netcoreapp3.1`
`dotnet publish --configuration=Debug  --output=D:\TempFIles  --framework=net6.0`
`dotnet publish -r win-x86 -c release /p:PublishSingleFile=true /p:publishtrimmed=true`
`dotnet publish -r win-x64 -c release /p:PublishSingleFile=true /p:publishtrimmed=true`

##### 7. 进入发布后的目录查看是否发布成功

`explorer   D:\publish\mycoreapi`

##### 8. 压缩EXE发布(Csharp)

```Xml
    <Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <PublishSingleFile>true</PublishSingleFile>
    <SelfContained>true</SelfContained>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PublishReadyToRun>true</PublishReadyToRun>
  </PropertyGroup>
  <PropertyGroup>
    <PublishTrimmed>true</PublishTrimmed>
  </PropertyGroup>
</Project>
//old
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

> 使用软件Wrap压缩C#可执行文件

- ##### USAGE

- `:windows-x64.warp-packer.exe --arch <arch> --exec <exec> --input_dir <input_dir> --output <output>`
- <font size=2 color=#4da463>ForExample:</font>
- `windows-x64.warp-packer.exe --arch windows-x64 --exec OrganizePLNfiles.exe --input_dir F:\VSCodeDepository\Dotnet6\Garbage\OrganizePLNfiles\bin\Debug\net6.0\win-x64\publish  --output OrganizePLNfiles.exe`

> 不使用Runtime库，手动实现基本函数

- 参见：<https://github.com/MichalStrehovsky/zerosharp>

#### 9. **.GitIgnore 规则等**

![gitIgnoreRules](/CSharpLearning/img/Csharppublish/E-2022-09-13-10-50-42.png)

 当.gitignore 文件失效，不起作用时：👇👇👇
<https://blog.csdn.net/ThinkWon/article/details/101447866>
![WorkAround](/CSharpLearning/img/Csharppublish/E-2022-09-13-10-56-02.png)

- <font color=Pink>  git rm -r --cached .</font>
- <font color=Pink>  git add .</font>
- <font color=Pink>git commit -m 'update .gitignore'</font>
