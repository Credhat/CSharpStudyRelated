> &emsp;<font color=#594ee7> CurrentTime: 2022-10-14-13:18:28 </font>
> &emsp;<font color=DarkBlue> Created by Yogi. </font>

### 强制刪除文件

- Force file deletion

#### **1**   &emsp;&emsp; CMD 命令行刪除 :pen:&emsp;

&emsp;&emsp; - `del /s/f/q PATH_FileUWannaDelete`

#### **2**   &emsp;&emsp; C# Console Application 支持中文编码 :page_with_curl:&emsp;

```Csharp
    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    Console.OutputEncoding = System.Text.Encoding.GetEncoding("GB2312");
    .//or Just use  Console.OutputEncoding = System.Text.Encoding.GetEncoding("GBK");
```

#### **3**   &emsp;&emsp; .bat文件拖拽刪除 :star:&emsp;

&emsp;&emsp; - 新建TXT
&emsp;&emsp; - `DEL /F /A /Q \\?\%1`     <font color=Green> <----> 第一行内容 </font>
&emsp;&emsp; - `RD/S/Q\\\%1` &emsp;&emsp;&emsp;&emsp;    <font color=Green> <----> 第二行内容 </font>
&emsp;&emsp; - 更改新建TXT的文件名為`DeleteFile.bat`
&emsp;&emsp; - 拖拽想要刪除的文件至`DeleteFile.bat` ---> 即可刪除

> Addition:
> ![Format Explanation](/img/FileOperations/E-2022-12-06-17-10-19.png)

#### **4**   &emsp;&emsp; 資源管理器刪除

&emsp;&emsp; 1. <font size=2> 按下`Win`鍵 </font>
&emsp;&emsp; 2. <font size=2> 搜索 ---> `資源管理器` </font>
&emsp;&emsp; 3. <font size=2> 選擇 `CPU` </font>
&emsp;&emsp; 4. <font size=2> 輸入`待刪除文件名` </font>
&emsp;&emsp; 5. <font size=2> 鼠標右擊`待刪除文件` ---> 結束進程 </font>

### 微软工具箱

- [PowerToys](https://github.com/microsoft/PowerToys/releases/) <font color=Green size=1> 👈 Go to Download. </font> :star:

PowerToys Awake 还可以直接从 PowerToys 文件夹作为独立的应用程序执行。 通过 *终端* 或 *.lnk 快捷方式* 文件运行 `PowerToys.Awake.exe` 时，可以使用以下命令行参数：

| 参数              | 说明 |
|:------------------|:-----|
| `--use-pt-config` |1. 使用 PowerToys 配置文件来管理设置。 这假设有一个由 PowerToys 生成的用于 Awake 的 `settings.json` 文件，其中包含所有必需的运行时信息。 <br><br>这包括“行为模式”(不限时或计时): <br><br>&emsp; - 是否应让屏幕保持打开状态 <br><br>&emsp; - 让屏幕暂时保持唤醒状态的小时值和分钟值。<br><br>2.使用此参数时，会忽略所有其他参数。 Awake 会在 `settings.json` 文件中查找更改以更新其状态。|
| `--display-on`    | 确定在计算机保持 唤醒状态 时，屏幕应保持*打开状态*还是*关闭状态*。 预期值为 `true` 或 `false`。     |
|`--time-limit`|Awake 使计算机保持唤醒状态的持续时间（以**秒**为单位）。 可以与 `--display-on` 结合使用。|
|`--pid`|将 Awake 的执行附加到进程`ID (PID)`。 当具有给定 `PID` 的进程*终止*时，Awake 也将终止。|
`PS:在缺少命令行参数的情况下，PowerToys Awake 会使计算机处于无限期唤醒状态。`:star::star::star:

### Install .Net 3.5 (sp 1) by using command in CMD

![.NET 35 Installation](/img/FileOperations/E-2022-12-06-17-07-20.png)

```Csharp
//Recommend
DISM /Online /Enable-Feature /FeatureName:NetFx3 /All /LimitAccess /Source:H:\sources\sxs
//or
DISM /Online /Enable-Feature /FeatureName:NetFx3 /Source:H:\sources\sxs
```

___

1. [跳转回*强制刪除文件*](#强制刪除文件)
2. [跳转回*微软工具箱*](#微软工具箱)
