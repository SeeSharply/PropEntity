## VSIXPROP Plugin vs插件-PROP
## 作者-康宇

### 目的

当我们编写实体Entity的时候，往往没有很好的办法直接生成，就比较麻烦，这也是编写这个插件的初衷

easily Entity 

**使用方式1**

type string，use  ‘,’ comma  split

string prop 》》shortcut  alt+p

安装插件之后，选中你需要生成的字符串，快捷键Alt+P可以按照字符串直接生成实体

比如以下字符串,like this

```  
Vchcode,dlyorder,Createtime,Qty
```
alt+p

命令会按照 逗号，回车，Tab制表符来分割每个属性，然后生成属性，生成效果如下

```
    /// <summary>
    ///
    /// <summary>
    public string  Vchcode  {get;set;}
    /// <summary>
    ///
    /// <summary>
    public ulong  Dlyorder  {get;set;}
    /// <summary>
    ///
    /// <summary>
    public DateTime  Createtime  {get;set;}
    /// <summary>
    ///
    /// <summary>
    public decimal  Qty  {get;set;}
```

**### 使用方式2**

type table name

databse prop 》》alt+s


程序可提供配置sql数据库，只要输入表名字，按Alt+S,可直接生成表的实体，效果如下

表名 

```
Atype
```


生成效果

```
    /// <summary>
    ///typeId，五五制，包含层级关系，主要用于查询统计
    /// <summary>
    public string  TypeId  {get;set;}
    /// <summary>
    ///父typeId
    /// <summary>
    public string  ParTypeId  {get;set;}
    /// <summary>
    ///层级
    /// <summary>
    public short  Leveal  {get;set;}
```
currently support mysql


数据库配置方式：

点击工具-》选项

找到PropConfig，数据库类型选择MySql，编写好连接字符串


database config method：

click tools-》options

find propconig，click

select MySql and edit connect string

现在，你可以在vs2017 扩展更新里面找到它了，搜索VsixProp

-----

## **versions**

----
1.1.2.1

1.support mysql and string to create entity;

    I recommand to select the string you typed before   using PropEntity Command!

----
1.2.3.1

1.support sqlite3;

    I recommand to use  absolute file path to build the connection string;

2.Add Summary Config

    You have an opportunity to choose if there is a need to add summary to your entity property through config!








