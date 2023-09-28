# Invoke JavaScript methods in Blazor .Net Application

### 1. Create a Blazor .Net Application

    术语客户端/客户端侧和服务器/服务器侧用于区分应用代码执行的位置：

1. 客户端/客户端侧 (Client-Side)
    - 托管的 `Blazor WebAssembly` 应用的 `Client` 项目。
    - `Blazor WebAssembly` 应用。
    - `Blazor` 脚本启动配置位于 `wwwroot/index.html` 文件中。
    - `Program` 文件为 `Program.cs`。
    </br>
2. 服务器/服务器端 (Server-Side)
    - 托管的 `Blazor WebAssembly` 应用的 `Server` 项目。
    - `Blazor Server` 应用。 `Blazor` 脚本启动配置位于 `Pages/_Host.cshtml` 中。
    - `Program` 文件为 `Program.cs`。
    - `IJSRuntime` 由 `Blazor` 框架注册。若要从 `.NET` 调入 `JS`，请注入 `IJSRuntime` 抽象并调用以下方法之一：
    </br>
3. .Net 里的 `IJSRuntime` 接口

    ```csharp
    - IJSRuntime.InvokeAsync<TValue>(string methodName, object?[]? args)
    - JSRuntimeExtensions.InvokeAsync<TValue>(string methodName, object?[]? args)
    - JSRuntimeExtensions.InvokeVoidAsync(string methodName, object?[]? args)
    ```

    </br>

## Recommend JS inter-operation in .Net

### 1. 注入 `IJSRuntime` 的方式
