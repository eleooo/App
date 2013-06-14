using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace Eleooo.Common
{
    public class GridViewHelper
    {
        // Fields
        public static int DIGITS_Price = 3;
        public static int DIGITS_Qty = 3;
        public static int DIGITS_Rate = 4;
        public static int DIGITS_Sum = 2;

        // Methods
        public static void AddGridExcelColumn(GridView gv, DataTable ColItem)
        {
            AddGridExcelColumn(gv, ColItem, string.Empty);
        }

        public static void AddGridExcelColumn(GridView gv, DataTable ColItem, string LangID)
        {
            gv.AllowSorting = false;
            gv.AutoGenerateColumns = false;
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            bool flag = false;
            foreach (DataRow row in ColItem.Rows)
            {
                str = row["ColName"].ToString( );
                str3 = row["ColCaption"].ToString( ).ToLower( );
                flag = Convert.ToBoolean(row["IsVisible"]);
                str2 = row["ColCaption_Cn"].ToString( );
                if (!flag)
                {
                    continue;
                }
                BoundField field = new BoundField
                {
                    HeaderText = str2,
                    DataField = str
                };
                field.ItemStyle.Wrap = false;
                field.HeaderStyle.Wrap = false;
                string str4 = str3;
                if (str4 != null)
                {
                    if (!(str4 == "datetime") && !(str4 == "smalldatetime"))
                    {
                        if (str4 == "money")
                        {
                            goto Label_0169;
                        }
                        if (str4 == "numeric")
                        {
                            goto Label_0198;
                        }
                        if (str4 == "int")
                        {
                            goto Label_01C7;
                        }
                    }
                    else
                    {
                        field.DataFormatString = "{0:d}";
                    }
                }
                goto Label_01D4;
            Label_0169:
                field.DataFormatString = "{0:c" + DIGITS_Sum.ToString( ) + "}";
                field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                goto Label_01D4;
            Label_0198:
                field.DataFormatString = "{0:f" + DIGITS_Qty.ToString( ) + "}";
                field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                goto Label_01D4;
            Label_01C7:
                field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            Label_01D4:
                gv.Columns.Add(field);
            }
        }

        public static void AddGridFindColumn(GridView gv, DataTable ColItem)
        {
            AddGridFindColumn(gv, ColItem, string.Empty);
        }

        public static void AddGridFindColumn(GridView gv, DataTable ColItem, string LangID)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            foreach (DataRow row in ColItem.Rows)
            {
                if (!Convert.ToBoolean(row["IsFind"]))
                {
                    continue;
                }
                str = row["ColName"].ToString( );
                str3 = row["ColDataType"].ToString( ).ToLower( );
                str2 = row["ColCaption_Cn"].ToString( );
                BoundField field = new BoundField
                {
                    HeaderText = str2,
                    DataField = str,
                    SortExpression = str
                };
                field.HeaderStyle.Wrap = false;
                field.ItemStyle.Wrap = false;
                string str4 = str3;
                if (str4 != null)
                {
                    if (!(str4 == "money"))
                    {
                        if (str4 == "numeric")
                        {
                            goto Label_0168;
                        }
                        if (str4 == "int")
                        {
                            goto Label_0197;
                        }
                    }
                    else
                    {
                        field.DataFormatString = "{0:c" + DIGITS_Sum.ToString( ) + "}";
                        field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }
                }
                goto Label_01A4;
            Label_0168:
                field.DataFormatString = "{0:f" + DIGITS_Qty.ToString( ) + "}";
                field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                goto Label_01A4;
            Label_0197:
                field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            Label_01A4:
                gv.Columns.Add(field);
            }
        }

        public static void AddGridListColumn(GridView gv, DataTable ColItem)
        {
            AddGridListColumn(gv, ColItem, string.Empty);
        }

        public static void AddGridListColumn(GridView gv, DataTable ColItem, string LangID)
        {
            string fieldname = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            foreach (DataRow row in ColItem.Rows)
            {
                BoundField field3;
                if (!Convert.ToBoolean(row["IsVisible"]))
                {
                    continue;
                }
                fieldname = row["ColName"].ToString( );
                str3 = row["ColDataType"].ToString( ).ToLower( );
                str2 = row["ColCaption_Cn"].ToString( );
                switch (str3)
                {
                    case "datetime":
                    case "smalldatetime":
                        {
                            TemplateField field = new TemplateField
                            {
                                SortExpression = fieldname
                            };
                            field.ItemStyle.Wrap = false;
                            field.HeaderStyle.Wrap = false;
                            //field.ItemTemplate = new iDateColumnTemplate(fieldname);
                            field.HeaderText = str2;
                            gv.Columns.Add(field);
                            continue;
                        }
                    default:
                        {
                            if (row["Link_Str"].ToString( ).Trim( ) != string.Empty)
                            {
                                HyperLinkField field2 = new HyperLinkField
                                {
                                    HeaderText = str2,
                                    DataNavigateUrlFormatString = row["Link_Str"].ToString( ),
                                    DataNavigateUrlFields = row["Link_Field"].ToString( ).Split(new char[] { ',' }),
                                    SortExpression = fieldname,
                                    DataTextField = fieldname
                                };
                                field2.HeaderStyle.Wrap = false;
                                field2.ItemStyle.Wrap = false;
                                gv.Columns.Add(field2);
                                continue;
                            }
                            field3 = new BoundField
                            {
                                HeaderText = str2,
                                DataField = fieldname,
                                SortExpression = fieldname
                            };
                            field3.HeaderStyle.Wrap = false;
                            field3.ItemStyle.Wrap = false;
                            int num = Utilities.ToInt(row["ColWidth"]);
                            field3.ItemStyle.Width = new Unit((num > 40) ? num : 40);
                            string str4 = str3;
                            if (str4 != null)
                            {
                                if (!(str4 == "money"))
                                {
                                    if (str4 == "numeric")
                                    {
                                        break;
                                    }
                                    if (str4 == "int")
                                    {
                                        goto Label_02E6;
                                    }
                                }
                                else
                                {
                                    field3.DataFormatString = "{0:c" + DIGITS_Sum.ToString( ) + "}";
                                    field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                }
                            }
                            goto Label_02F3;
                        }
                }
                field3.DataFormatString = "{0:f" + DIGITS_Qty.ToString( ) + "}";
                field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                goto Label_02F3;
            Label_02E6:
                field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            Label_02F3:
                gv.Columns.Add(field3);
            }
        }

        public static void InsertGridListColumn(GridView gv, int SeqNo, DataTable ColItem)
        {
            InsertGridListColumn(gv, SeqNo, ColItem, string.Empty);
        }

        public static void InsertGridListColumn(GridView gv, int SeqNo, DataTable ColItem, string LangID)
        {
            InsertGridListColumn(gv, SeqNo, ColItem, string.Empty, false);
        }

        public static void InsertGridListColumn(GridView gv, int SeqNo, DataTable ColItem, string LangID, bool IsFixWidth)
        {
            gv.Attributes.Add("style ", "table-layout:fixed ");
            int num = 100;
            string fieldname = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            foreach (DataRow row in ColItem.Rows)
            {
                TemplateField field;
                BoundField field3;
                if (Convert.ToBoolean(row["IsVisible"]))
                {
                    fieldname = row["ColName"].ToString( );
                    num = Utilities.ToInt(row["ColWidth"]);
                    str3 = row["ColDataType"].ToString( ).ToLower( );
                    str2 = row["ColCaption_Cn"].ToString( );
                    switch (str3)
                    {
                        case "datetime":
                        case "smalldatetime":
                            {
                                field = new TemplateField
                                {
                                    SortExpression = fieldname,
                                    //ItemTemplate = new iDateColumnTemplate(fieldname),
                                    HeaderText = str2
                                };
                                field.HeaderStyle.Width = new Unit(num);
                                field.ItemStyle.Width = new Unit(num);
                                gv.Columns.Insert(SeqNo, field);
                                continue;
                            }
                    }
                    if (row["Link_Str"].ToString( ).Trim( ) != string.Empty)
                    {
                        HyperLinkField field2 = new HyperLinkField
                        {
                            HeaderText = str2,
                            DataNavigateUrlFormatString = row["Link_Str"].ToString( ),
                            DataNavigateUrlFields = row["Link_Field"].ToString( ).Split(new char[] { ',' }),
                            SortExpression = fieldname,
                            DataTextField = fieldname
                        };
                        field2.HeaderStyle.Width = new Unit(num);
                        field2.ItemStyle.Width = new Unit(num);
                        gv.Columns.Insert(SeqNo, field2);
                    }
                    else
                    {
                        string str4 = str3;
                        if (str4 == null)
                        {
                            goto Label_03EA;
                        }
                        if (!(str4 == "money"))
                        {
                            if (str4 == "numeric")
                            {
                                goto Label_02FB;
                            }
                            if (str4 == "int")
                            {
                                goto Label_038B;
                            }
                            goto Label_03EA;
                        }
                        field3 = new BoundField
                        {
                            HeaderText = str2,
                            DataField = fieldname,
                            SortExpression = fieldname
                        };
                        field3.ItemStyle.Wrap = false;
                        field3.DataFormatString = "{0:c" + DIGITS_Sum.ToString( ) + "}";
                        field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        field3.HeaderStyle.Width = new Unit(num);
                        field3.ItemStyle.Width = new Unit(num);
                        gv.Columns.Insert(SeqNo, field3);
                    }
                }
                continue;
            Label_02FB:
                field3 = new BoundField( );
                field3.HeaderText = str2;
                field3.DataField = fieldname;
                field3.SortExpression = fieldname;
                field3.ItemStyle.Wrap = false;
                field3.DataFormatString = "{0:f" + DIGITS_Qty.ToString( ) + "}";
                field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                field3.HeaderStyle.Width = new Unit(num);
                field3.ItemStyle.Width = new Unit(num);
                gv.Columns.Insert(SeqNo, field3);
                continue;
            Label_038B:
                field3 = new BoundField( );
                field3.HeaderText = str2;
                field3.DataField = fieldname;
                field3.SortExpression = fieldname;
                field3.ItemStyle.Wrap = false;
                field3.HeaderStyle.Width = new Unit(num);
                field3.ItemStyle.Width = new Unit(num);
                field3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                continue;
            Label_03EA:
                field = new TemplateField( );
                field.SortExpression = fieldname;
                field.HeaderStyle.Width = new Unit(num);
                field.ItemStyle.Width = new Unit(num);
                //field.ItemTemplate = new iTextColumnTemplate(fieldname, num);
                field.HeaderText = str2;
                gv.Columns.Insert(SeqNo, field);
            }
        }
    }
}