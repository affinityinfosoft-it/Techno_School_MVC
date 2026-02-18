using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SchoolMVC.Models;

namespace SchoolMVC.GlobalClass
{
    public static class ExtensionMethods
    {
        public static void CopyPropertiesTo(this Object source, object destination)
        {
            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();
            var destinationType = destination.GetType();
            var destinationProperties = destinationType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {

                var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null)
                {
                    var sourceValue = sourceProperty.GetValue(source, null);
                    var sourcePropertyType = sourceProperty.PropertyType;
                    if (sourcePropertyType == typeof(int?))
                    {
                        if (sourceValue == null)
                        {
                            sourceValue = 0;
                        }
                    }
                    destinationProperty.SetValue(destination, sourceValue, null);
                }

            }
        }

        public static string AddServerVarriablesToDatatable(this HtmlHelper htmlHelper, NameValueCollection serverVarriables)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("function(  aoData ) {");
            foreach (var key in serverVarriables.AllKeys)
            {
                stringBuilder.Append("aoData.push({name:'" + key + "', value:'" + serverVarriables[key] + "'}); ");
            }
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public static MvcHtmlString CalenderTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object additionalViewdata = null)
        {
            //var mvcHtmlString = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes ?? new { @class = "text-box single-line displayDate" });
            var mvcHtmlString = EditorExtensions.EditorFor(htmlHelper, expression, additionalViewdata);
            var xDoc = XDocument.Parse(mvcHtmlString.ToHtmlString().Replace("class=\"text-box single-line\"", "class=\"text-box single-line displayDate\"").Replace("class=\"input-validation-error text-box single-line\"", "class=\"input-validation-error text-box single-line displayDate\""));
            return new MvcHtmlString(xDoc.ToString());
        }

      public static string AddDrawCallbackWtaxDatatable(this HtmlHelper htmlHelper)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("function(  oSettings ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("var accNoIds = $('#hidComaseparatedAccId').val().split(',');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$(accNoIds).each(function( index ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$('#table-id_wrapper').find($('#chkDenad_' + accNoIds[index])).attr('checked','checked');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("});");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public static string AddDrawCallbackTradeRenewalDatatable(this HtmlHelper htmlHelper)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("function(  oSettings ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("var licNos = $('#hidComaseparatedLicNo').val().split(',');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$(licNos).each(function( index ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$('#table-id_wrapper').find($('#chkRenew_' + licNos[index])).attr('checked','checked');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("});");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public static string AddDrawCallbackBPSSendSMSDatatable(this HtmlHelper htmlHelper)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("function(  oSettings ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("var licNos = $('#hidSMSBPS1List').val().split(',');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$(licNos).each(function( index ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$('#table-id_wrapper').find(\"input[type='checkbox'][premisesno='\" + licNos[index] + \"']\").attr('checked','checked');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("});");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public static string AddDrawCallbackWorksEPaymentDatatable(this HtmlHelper htmlHelper)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("function(  oSettings ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("var billIds = $('#hidComaseparatedBillId').val().split(',');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$(billIds).each(function( index ) {");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("$('#table-id_wrapper').find($('#chkEpayment_' + billIds[index])).attr('checked','checked');");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("});");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public static DataSet ToDataset<T>(this List<T> list)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Table1";
            if (list.Count != 0)
                foreach (PropertyInfo property in list[0].GetType().GetProperties())
                {
                    var type = property.PropertyType;
                    var underlyingType = Nullable.GetUnderlyingType(type);
                    var returnType = underlyingType ?? type;

                    dt.Columns.Add(new DataColumn(property.Name, returnType));
                }

            foreach (var obj in list)
            {
                DataRow newRow = dt.NewRow();
                foreach (PropertyInfo property in obj.GetType().GetProperties())
                {
                    newRow[property.Name] = obj.GetType().GetProperty(property.Name).GetValue(obj, null) ?? DBNull.Value;
                }
                dt.Rows.Add(newRow);
            }
            ds.Tables.Add(dt);
            return ds;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static void RemoveModelStateError(this ModelStateDictionary modelStateDictionary, string entityKey, object entityValue)
        {
            modelStateDictionary.Remove(entityKey);
            modelStateDictionary.Add(entityKey, new ModelState());
            modelStateDictionary.SetModelValue(entityKey, new ValueProviderResult(entityValue, Convert.ToString(entityValue), null));
        }

        public static string ToXml(this object modelObject)
        {
            var value = modelObject;
            if (value == null)
            {
                return "";
            }
            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(modelObject.GetType());
                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);

                xmlserializer.Serialize(writer, value);

                string serializeXml = stringWriter.ToString();

                writer.Close();
                return serializeXml;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static T FromXml<T>(this T modelObject, string xmlString)
        {
            var value = modelObject;
            if (value == null)
            {
                return modelObject;
            }
            if (xmlString == null) return modelObject;
            if (xmlString.Trim() == "") return modelObject;
            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));

                TextReader reader = new StringReader(xmlString);
                //XmlReader xmlReader = new XmlTextReader(reader);
                var model = (T)xmlserializer.Deserialize(reader);
                reader.Close();
                return model;
            }
            catch (Exception ex)
            {
                return modelObject;
            }
        }

        public static object ToType<T>(this object obj, T type)
        {

            //create instance of T type object:
            var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {

                    //get the value of property and try 
                    //to assign it to the property of T type object:
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                              pi.GetValue(obj, null), null);
                }
                catch { }
            }

            //return the T type object:         
            return tmp;
        }

        public static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }

    }
}