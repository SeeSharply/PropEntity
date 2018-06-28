## VSIXPROP Plugin vs插件-PROP
## 作者-康宇

### 目的

当我们编写实体Entity的时候，往往没有很好的办法直接生成，就比较麻烦，这也是编写这个插件的初衷

**### 使用方式1**

安装插件之后，选中你需要生成的字符串，快捷键Alt+P可以按照字符串直接生成实体

比如以下字符串

```  
Vchcode,dlyorder,Createtime,Qty
```

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

目前仅支持mysql的数据库连接，请确保正确的数据库连接


