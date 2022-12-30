# Cryptography (密码学)

[参考 微软文档 - Encrypting data](https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data)

## 对称加密

托管对称加密类与名为 `CryptoStream` 的特殊流类一起使用，后者用于加密读入流中的数据。 使用托管流类、实现 接口（从实现加密算法的类创建）的类以及用于描述对 CryptoStream ICryptoTransform 授予的访问权限的类型的 `CryptoStreamMode` 枚举，来初始化 `CryptoStream`类。 可以使用派生自 类的任何类来初始化 CryptoStream Stream 类，其中包括 `FileStream`、 `MemoryStream` 和 `NetworkStream`。 使用这些类，可以对多个流对象执行对称加密。

以下示例说明了如何为 `Aes` 算法创建默认实现类的新实例。 该实例用于对 CryptoStream 类执行加密。 在此示例中，使用名为 的流对象初始化 `CryptoStream` ，该流对象可以是任何类型的托管流。 向 Aes 类中的 CreateEncryptor 方法传递用于加密的 `密钥(Key)` 和 `偏移量(IV)`。 在此例中，使用了由 `aes` 生成的默认密钥和 IV。

> 英文原文
> The managed symmetric cryptography classes are used with a special stream class called a CryptoStream that encrypts data read into the stream. The CryptoStream class is initialized with a managed stream class, a class that implements the ICryptoTransform interface (created from a class that implements a cryptographic algorithm), and a CryptoStreamMode enumeration that describes the type of access permitted to the CryptoStream. The CryptoStream class can be initialized using any class that derives from the Stream class, including FileStream, MemoryStream, and NetworkStream. Using these classes, you can perform symmetric encryption on a variety of stream objects.
>  
> The following example illustrates how to create a new instance of the default implementation class for the Aes algorithm. The instance is used to perform encryption on a CryptoStream class. In this example, the CryptoStream is initialized with a stream object called fileStream that can be any type of managed stream. The CreateEncryptor method from the Aes class is passed the key and IV that are used for encryption. In this case, the default key and IV generated from aes are used.

```Csharp
{
    Aes aes = Aes.Create();
    CryptoStream cryptStream = new CryptoStream(
    fileStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
}
```

执行此代码后，使用 AES 算法对写入到 CryptoStream 对象的任何数据进行加密。

下面的示例演示创建流、加密流、写入流和关闭流的整个过程。 此示例创建使用 CryptoStream 类和 Aes 类加密的文件流。 生成的 IV 将写入 到 的 FileStream开头，因此可以读取并用于解密。 然后使用 StreamWriter 类将消息写入到加密流。 虽然可以多次使用同一个密钥来加密和解密数据，但建议每次生成一个新的随机 IV。 这样加密的数据总是不同的，即使纯文本是相同的。

> 英文原文
> After this code is executed, any data written to the CryptoStream object is encrypted using the AES algorithm.
>
> The following example shows the entire process of creating a stream, encrypting the stream, writing to the stream, and closing the stream. This example creates a file stream that is encrypted using the CryptoStream class and the Aes class. Generated IV is written to the beginning of FileStream, so it can be read and used for decryption. Then a message is written to the encrypted stream with the StreamWriter class. While the same key can be used multiple times to encrypt and decrypt data, it is recommended to generate a new random IV each time. This way the encrypted data is always different, even when plain text is the same.

```Csharp
{
using System.Security.Cryptography;

try
{
    using (FileStream fileStream = new("TestData.txt", FileMode.OpenOrCreate))
    {
        using (Aes aes = Aes.Create())
        {
            byte[] key =
            {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };
            aes.Key = key;

            byte[] iv = aes.IV;
            fileStream.Write(iv, 0, iv.Length);

            using (CryptoStream cryptoStream = new(
                fileStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write))
            {
                // By default, the StreamWriter uses UTF-8 encoding.
                // To change the text encoding, pass the desired encoding as the second parameter.
                // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
                using (StreamWriter encryptWriter = new(cryptoStream))
                {
                    encryptWriter.WriteLine("Hello World!");
                }
            }
        }
    }

    Console.WriteLine("The file was encrypted.");
}
catch (Exception ex)
{
    Console.WriteLine($"The encryption failed. {ex}");
}
}
```

代码使用 AES 对称算法加密流，写入 IV，然后将加密的“Hello World！”写入到流中。 如果代码成功，则会创建一个名为 TestData.txt 的加密文件，并向控制台显示以下文本：

> 英文原文
> The code encrypts the stream using the AES symmetric algorithm, and writes IV and then encrypted "Hello World!" to the stream. If the code is successful, it creates an encrypted file named TestData.txt and displays the following text to the console:

`The file was encrypted.`

你可以使用[解密数据](https://learn.microsoft.com/zh-cn/dotnet/standard/security/decrypting-data)中的对称解密示例来解密文件。 该示例和此示例指定了相同的密钥。

但是，如果引发异常，代码会在控制台中显示以下文本：

> You can decrypt the file by using the symmetric decryption example in Decrypting Data. That example and this example specify the same key.
>
> However, if an exception is raised, the code displays the following text to the console:

`The encryption failed.`

## 对称解密

解密用对称算法加密的数据类似于用对称算法加密数据的过程。 若要对从任何托管流对象中读取的数据进行解密，应将 CryptoStream 与 .NET 提供的对称加密类一起使用。

以下示例说明了如何为 Aes 算法创建默认实现类的新实例。 该实例用于对 CryptoStream 对象执行解密。 此示例首先创建 Aes 实现类的一个新实例。 它从托管流变量 fileStream 中读取初始化向量(IV) 值。 接下来它实例化一个 CryptoStream 对象并将其初始化为 fileStream 实例的值。 Aes 实例中的 SymmetricAlgorithm.CreateDecryptor 方法传递 `IV` 值和用于`加密的相同密钥`。

> 英文原文：
> The following example illustrates how to create a new instance of the default implementation class for the [Aes](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes) algorithm. The instance is used to perform decryption on a [CryptoStream](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cryptostream) object. This example first creates a new instance of the Aes implementation class. It reads the initialization vector (IV) value from a managed stream variable, `fileStream`. Next it instantiates a CryptoStream object and initializes it to the value of the `fileStream` instance. The [SymmetricAlgorithm.CreateDecryptor](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.symmetricalgorithm.createdecryptor) method from the Aes instance is passed the IV value and the same key that was used for encryption.

```Csharp
{
    Aes aes = Aes.Create();
    CryptoStream cryptStream = new CryptoStream(
    fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
}
```

下面的示例显示创建流、解密流、从流中读取和关闭流的整个过程。 创建一个文件流对象，该对象读取名为 TestData.txt 的文件。 然后将使用 CryptoStream 类和 Aes 类对文件流进行解密。 此示例指定用于[加密数据](https://learn.microsoft.com/zh-cn/dotnet/standard/security/encrypting-data)的对称加密示例中的密钥值。 它不会显示加密和传输这些值所需的代码。

> 英文原文
> The following example shows the entire process of creating a stream, decrypting the stream, reading from the stream, and closing the streams. A file stream object is created that reads a file named TestData.txt. The file stream is then decrypted using the `CryptoStream` class and the `Aes` class. This example specifies key value that is used in the symmetric encryption example for [Encrypting Data](https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data). It does not show the code needed to encrypt and transfer these values.

```Csharp
{
using System.Security.Cryptography;

try
{
    using (FileStream fileStream = new("TestData.txt", FileMode.Open))
    {
        using (Aes aes = Aes.Create())
        {
            byte[] iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                if (n == 0) break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            byte[] key =
            {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };

            using (CryptoStream cryptoStream = new(
               fileStream,
               aes.CreateDecryptor(key, iv),
               CryptoStreamMode.Read))
            {
                // By default, the StreamReader uses UTF-8 encoding.
                // To change the text encoding, pass the desired encoding as the second parameter.
                // For example, new StreamReader(cryptoStream, Encoding.Unicode).
                using (StreamReader decryptReader = new(cryptoStream))
                {
                    string decryptedMessage = await decryptReader.ReadToEndAsync();
                    Console.WriteLine($"The decrypted original message: {decryptedMessage}");
                }
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"The decryption failed. {ex}");
}
}
```

前面的示例使用对称加密示例中用于加密数据的相同密钥和算法。 它解密由该示例创建的 TestData.txt 文件并在控制台上显示原始文本。
