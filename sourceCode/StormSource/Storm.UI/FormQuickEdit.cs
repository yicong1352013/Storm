//Copyright (c) 2008, Erik Araojo
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and 
//the following disclaimer in the documentation and/or other materials provided with the distribution.
//* Neither the name of Erik Araojo nor the names of its contributors may be used to endorse or 
//promote products derived from this software without specific prior written permission.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
//IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
//FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
//LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
//THE POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Storm.UI
{
    
    public partial class FormQuickEdit : Form
    {
        public FormQuickEdit()
        {
            InitializeComponent();
        }

        public void ShowExtraControls( Type t)
        {
            if (typeof(byte[]) == t)
            {
                this.splitContainer1.Panel1Collapsed = false;
                this.cbPolymorphTypes.Visible = false;
                this.btnLoadFile.Visible = true;
                this.btnLoadFile.Dock = this.cbPolymorphTypes.Dock;
                this.lblLoadFile.Dock = this.cbPolymorphTypes.Dock;
                this.lblPoly.Visible = false;
                this.lblLoadFile.Visible = true;
                this.lblLoadFile.Text = "";
               // this.lblPoly.Text = "Load a file";
            }
            else
                this.splitContainer1.Panel1Collapsed = true;
        }
        public void InitSubTypes(Type[] subtypes)
        {
            if (subtypes.Length > 1)
            {

                this.cbPolymorphTypes.Visible = true;
                #region loadfile
                this.lblLoadFile.Visible = false;
                this.btnLoadFile.Visible = false;
                #endregion
                this.splitContainer1.Panel1Collapsed = false;
                this.cbPolymorphTypes.Items.Clear();
                this.cbPolymorphTypes.Items.AddRange(subtypes);
                this.cbPolymorphTypes.SelectedIndex = 0;
                this.lblPoly.Visible = true;
                this.lblPoly.Text = "Select a new Type";
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = true;
            }
        }

        private void gradientPanel1_Click(object sender, EventArgs e)
        {

        }

        
    }



    public class DynamicConverter : TypeConverter
    {

        private static Hashtable converterTable = null;
        //private static string typeNotFoundMessage = "ProxyPropertiesType {0} specified in SoapBits.exe.options is not found";

        static DynamicConverter()
        {
            converterTable = new Hashtable();
            converterTable[typeof(bool)] = Activator.CreateInstance(typeof(BooleanConverter));
            converterTable[typeof(byte)] = Activator.CreateInstance(typeof(ByteConverter));
            converterTable[typeof(sbyte)] = Activator.CreateInstance(typeof(SByteConverter));
            converterTable[typeof(char)] = Activator.CreateInstance(typeof(CharConverter));
            converterTable[typeof(double)] = Activator.CreateInstance(typeof(DoubleConverter));
            converterTable[typeof(string)] = Activator.CreateInstance(typeof(StringConverter));
            converterTable[typeof(int)] = Activator.CreateInstance(typeof(Int32Converter));
            converterTable[typeof(short)] = Activator.CreateInstance(typeof(Int16Converter));
            converterTable[typeof(long)] = Activator.CreateInstance(typeof(Int64Converter));
            converterTable[typeof(float)] = Activator.CreateInstance(typeof(SingleConverter));
            converterTable[typeof(ushort)] = Activator.CreateInstance(typeof(UInt16Converter));
            converterTable[typeof(uint)] = Activator.CreateInstance(typeof(UInt32Converter));
            converterTable[typeof(ulong)] = Activator.CreateInstance(typeof(UInt64Converter));
            converterTable[typeof(object)] = Activator.CreateInstance(typeof(ExpandableObjectConverter));
            converterTable[typeof(void)] = Activator.CreateInstance(typeof(TypeConverter));
            converterTable[typeof(CultureInfo)] = Activator.CreateInstance(typeof(CultureInfoConverter));
            converterTable[typeof(DateTime)] = Activator.CreateInstance(typeof(DateTimeConverter));
            converterTable[typeof(decimal)] = Activator.CreateInstance(typeof(DecimalConverter));
            converterTable[typeof(TimeSpan)] = Activator.CreateInstance(typeof(TimeSpanConverter));
            converterTable[typeof(Guid)] = Activator.CreateInstance(typeof(GuidConverter));


        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return GetConverter(context).CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(string));

            return GetConverter(context).CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return GetConverter(context).ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return GetConverter(context).ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return GetConverter(context).CreateInstance(context, propertyValues);
        }

        private static Type GetContainedType(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                Type t = context.Instance.GetType();
                
                QuickEditWrapper<object> td = context.Instance as QuickEditWrapper<object>;
                if (td != null)
                    return td.Value.GetType();
                


            }
            return null;
        }

        private static TypeConverter GetConverter(ITypeDescriptorContext context)
        {
            object obj2 = null;
            Type containedType = GetContainedType(context);
            if (containedType != null)
            {
                obj2 = converterTable[containedType];
            }
            if (obj2 == null)
            {
                if ((containedType != null) && containedType.IsEnum)
                {
                    obj2 = converterTable[containedType] = new EnumConverter(containedType);
                }
                else
                {
                    obj2 = converterTable[typeof(object)];
                }
            }
            return (obj2 as TypeConverter);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetCreateInstanceSupported(context);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return GetConverter(context).GetProperties(context, value, attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetPropertiesSupported(context);
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValues(context);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValuesExclusive(context);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValuesSupported(context);
        }

        public static bool IsConverterDefined(Type type)
        {
            return (converterTable[type] != null);
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return GetConverter(context).IsValid(context, value);
        }
    }
    //public class QuickEditWrapper
    //{
    //    object val = null;

    //    public object Value
    //    {
    //        get
    //        {
    //            return this.val;
    //        }
    //        set
    //        {
    //            this.val = value;
    //        }
    //    }
    //    // [RefreshProperties(RefreshProperties.All)]
    //    public string Type
    //    {
    //        get
    //        {
    //            return this.val.GetType().ToString();
    //        }

    //    }
    //}
    public class QuickEditWrapper<T>
    {
        T val = default(T);
        bool isNull = false;

        public QuickEditWrapper()
        { }

        public QuickEditWrapper(T v)
        {
            this.val = v;
        }

        [Browsable(true)]
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        [RefreshProperties(RefreshProperties.All),
        TypeConverter(typeof(DynamicConverter))]
        [Editor("System.ComponentModel.Design.DateTimeEditor", typeof(UITypeEditor))]
        public new T Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }

        public string Type
        {
            get
            {
                return this.val.GetType().ToString();
            }

        }

        [Browsable(false)]
        public string ParentType
        {
            get
            {
                return this.val.GetType().BaseType.ToString();
            }

        }

        [RefreshProperties(RefreshProperties.All)]
        public bool IsNull
        {
            get { return this.isNull; }
            set { this.isNull = value; }
        }
    }
    


}