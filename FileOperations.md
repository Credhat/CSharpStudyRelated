> &emsp;<font color=#594ee7> CurrentTime: 2022-10-14-13:18:28 </font>
> &emsp;<font color=DarkBlue> Created by Yogi. </font>

## 强制刪除文件

#### **1**   &emsp;&emsp; CMD 命令行刪除

&emsp;&emsp; - `del /s/f/q PATH_FileUWannaDelete`

#### **2**   &emsp;&emsp; .bat文件拖拽刪除

&emsp;&emsp; - 新建TXT
&emsp;&emsp; - `DEL /F /A /Q \\?\%1`     <font color=Green> <----> 第一行内容 </font>
&emsp;&emsp; - `RD/S/Q\\\%1` &emsp;&emsp;&emsp;&emsp;    <font color=Green> <----> 第二行内容 </font>
&emsp;&emsp; - 更改新建TXT的文件名為`DeleteFile.bat`
&emsp;&emsp; - 拖拽想要刪除的文件至`DeleteFile.bat` ---> 即可刪除

#### **3**   &emsp;&emsp; 資源管理器刪除

&emsp;&emsp; 1. <font size=2> 按下`Win`鍵 </font>
&emsp;&emsp; 2. <font size=2> 搜索 ---> `資源管理器` </font>
&emsp;&emsp; 3. <font size=2> 選擇 `CPU` </font>
&emsp;&emsp; 4. <font size=2> 輸入`待刪除文件名` </font>
&emsp;&emsp; 5. <font size=2> 鼠標右擊`待刪除文件` ---> 結束進程 </font>
