using System;
using System.Web;
using System.Xml;
using System.Data;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://Storm.test.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{

    public class TestHeader : SoapHeader
    {
        public string name="test";
        public string lastname="test";
      //  public TestClass testClass;
    }

    public enum TestEnum
    {
        One,
        Two,
        Three
    }

    public class NestedClass
    {
        private TestClass tc;
        public TestClass NC
        {
            get { return this.tc; }
            set { this.tc = value; }
        }
        public List<TestClass> myTcList;
        public NestedClass()
        { }


    }

    public class TestClass
    {
        public Guid myGuid = new Guid();
        public bool myBool;
        public int myInt;
        public long myLong;
        public double myDouble;
        public float myFloat;
        public string myString;
        public DateTime myDate;
        public TimeSpan myTimeSpan;
        //public XmlDocument myXdoc;
        public XmlElement myXmlElem;
        //public XmlAttribute myXmLAttr;
        //public XmlNode myXmlNode;
        public byte myByte;
        public byte[] mybyteArray;
        public sbyte mySbyte;
        public short myShort;
        public object myObj;
        public object[] myObjArray;
        public TestEnum myEnum;
        DataSet _ds = new DataSet();
        public DataSet DataS
        {
            get { return this._ds; }
            set {this._ds = value;}
        }
        public TestClass()
        {
        }
    }





    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
   // public void Test(TestClass tc) {
        
    //}

    public class SmallClass
    {
        public int num;
        public string sss = "ere";
        public SmallClass()
        { }
    }

    [WebMethod]
    public InterfaceImpl TestInterface(InterfaceImpl inter)
    {
        return inter;

    }

    [WebMethod]
    public MorphBase TestPolymorph(MorphBase morpheus)
    {
        return morpheus;

    }
    [WebMethod]
    public MorphBase TestPolymorph1(MorphConcreteLevel1 morpheus)
    {
        return morpheus;


    }

    [WebMethod]
    public MorphBase[] TestPolymorphArray(MorphBase[] morpheusArray)
    {
        return morpheusArray;


    }


    [WebMethod]
    public MorphBase TestPolymorph1a(MorphConcreteLevel1a morpheus)
    {
        return morpheus;


    }
    [WebMethod]
    public MorphBase TestPolymorph2(MorphConcreteLevel2 morpheus)
    {
        return morpheus;

    }


    [WebMethod]
    public string TestNullableType(int? intNUllablr, DateTime? dtNullable)
    {
        string ret = "sadly nobody is donating to the project.";
        if (intNUllablr.HasValue)
           ret = String.Format("int has value of {0}", intNUllablr) ;
        if (dtNullable.HasValue)
            ret += String.Format("date has value of {0}", dtNullable);

        return ret;
    }

    [WebMethod]
    public NestedClass TestComplexClass(NestedClass elem)
    {
        return elem;
    }


    [WebMethod]
    public XmlElement TestXmlElement(XmlElement elem)
    {
        string temp = @"<Root>
                           <child>a</child>
                           <child>
                              <grandkid>hohohoho</grandkid>
                           </child>
                                    
                        </Root>";
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(temp);
        return xdoc.DocumentElement;
    }

    public TestHeader head = new TestHeader();
    [SoapHeader("head", Direction = SoapHeaderDirection.InOut)]
    [WebMethod]
    public string[] TestSoapHeader()
    {
        if (head == null)
        {
            head = new TestHeader();
            head.name = "storm";
            head.lastname = "cyclone";
        }
        else
        {
            head.name = head.name.ToUpper();
            head.lastname = head.lastname.ToUpper();
        }
        return new string[] { head.lastname, head.name };
    }

    [WebMethod]
    public int Test1Simple(TestEnum te, bool go)
    {
        if (go)
        {
            return 96;
        }
        return 69;
    }
    [WebMethod]
    public SmallClass TestSmallClass(SmallClass sc, int num, bool go)
    {
        return sc;
    }

    [WebMethod]
    public SmallClass TestException(SmallClass sc, int num, bool go)
    {
        throw new Exception("test");
    }

    [WebMethod]
    public void TestVoid()
    {
        
    }
    [WebMethod]
    public SmallClass[] TestArray(SmallClass[] scArr, int num, bool go)
    {
        return scArr;
    }
    [WebMethod]
    public System.Data.DataSet TestDS()
    {
        return new System.Data.DataSet();
    }
//    [WebMethod]
  //  public TestClass TestNested(NestedClass nestedclass)
    //{
   //     return new TestClass();
   // }
    
}
