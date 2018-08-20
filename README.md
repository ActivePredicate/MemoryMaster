# MemoryMaster
A local filesystem Database by C#/C#的本地文件系统数据库

Codes are written by VS17

English：
MemoryMaster is a local filesystem database.This is a lightweight storage container ONLY FOR STRING.
If your program want to save another types of data, just conver then into STRING.For example,byte array,you can conver them into STRING with Encoding.
This Database is DIFFERENT from others.This DB is only as a table to normal DB.
This Database does not support SQL.To edit the data in Database, you can only use the internal methods.
If you want more functions,you can put the codes into your codes,edit them.

中文：
MemoryMaster是一个本地文件系统数据库。这是一个轻量级存储容器，仅用于字符串。
如果你的程序想要保存其他类型的数据，那么只需转换到STRING。例如，字节数组，你可以使用System.Encoding将它们转换为STRING。
此数据库与其他数据库不同。此数据库仅作为普通数据库的表。
此数据库不支持SQL。要编辑数据库中的数据，您只能使用DLL提供的内部方法。
如果您想要更多功能，可以自行编辑。

另外，老夫在MemoryMaster的类库项目中写了一个可视化数据管理器。调用方法：MemoryMaster.MemoryMaster.RunWindow(string dbpath)或MemoryMaster.MemoryMaster.RunWindow()

By the way,To open the Visual Database Viewer,Use MemoryMaster.MemoryMaster.RunWindow(string dbpath) or MemoryMaster.MemoryMaster.RunWindow()
