using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public interface IMyInterface
{
    string Name { get; set; }
}


public class InterfaceImpl : IMyInterface
{
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            this._name = value;
        }
    }


}

/// <summary>
/// Summary description for Morph
/// </summary>
public abstract class MorphBase
{
    private int _baseProp;

    public int BaseProp
    {
        get { return _baseProp; }
        set { _baseProp = value; }
    }
    
}

public class MorphConcreteLevel1 : MorphBase
{
    private string _baseProp;

    public string ConcretePropLevel1
    {
        get { return _baseProp; }
        set { _baseProp = value; }
    }



}


public class MorphConcreteLevel1a : MorphBase
{
    private string _baseProp;

    public string ConcretePropLevel1a
    {
        get { return _baseProp; }
        set { _baseProp = value; }
    }



}


public class MorphConcreteLevel2 : MorphConcreteLevel1
{
    private string _baseProp;

    public string ConcretePropLevel2
    {
        get { return _baseProp; }
        set { _baseProp = value; }
    }

}
