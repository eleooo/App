using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Eleooo.Web.Controls
{
    [ToolboxData("<{0}:DatetimePicker runat=server></{0}:DatetimePicker>")]
    public class DatetimePicker : WebControl
    {
        private DateTime? _selectedValue;
        private DropDownList _ddlYear;
        private DropDownList _ddlMonth;
        private DropDownList _ddlDay;
        private bool _createdControl;

        public DatetimePicker( ) : base(HtmlTextWriterTag.Div) { }
        protected override void EnsureChildControls( )
        {
            if (!_createdControl)
            {
                _ddlYear = new DropDownList( ) { CssClass = YearCssClass, ID = "ddlYear" };
                _ddlMonth = new DropDownList( ) { CssClass = MonthCssClass, ID = "ddlMonth" };
                _ddlDay = new DropDownList( ) { CssClass = DayCssClass, ID = "ddlDay" };
                //this.Controls.Add(_ddlYear);
                //this.Controls.Add(_ddlMonth);
                //this.Controls.Add(_ddlDay);
                BindYear( );
                BindDay( );
                BindMonth( );
                _createdControl = true;
            }
            base.EnsureChildControls( );
        }
        protected override void CreateChildControls( )
        {
            EnsureChildControls( );
            base.CreateChildControls( );
        }
        protected override void OnPreRender(EventArgs e)
        {
            _ddlYear.Enabled = this.Enabled;
            _ddlDay.Enabled = this.Enabled;
            _ddlDay.Enabled = this.Enabled;
            base.OnPreRender(e);
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (SelectedDate.HasValue)
            {
                string yearVal = SelectedDate.Value.Year.ToString( );
                string monthVal = SelectedDate.Value.Month.ToString( );
                string dayVal = SelectedDate.Value.Day.ToString( );
                var year = _ddlYear.Items.OfType<ListItem>( ).FirstOrDefault(item => item.Value == yearVal);
                if (year != null)
                    year.Selected = true;
                var month = _ddlMonth.Items.OfType<ListItem>( ).FirstOrDefault(item => item.Value == monthVal);
                if (month != null)
                    month.Selected = true;
                var day = _ddlDay.Items.OfType<ListItem>( ).FirstOrDefault(item => item.Value == dayVal);
                if (day != null)
                    day.Selected = true;
            }
            else
            {
                _ddlYear.ClearSelection( );
                _ddlMonth.ClearSelection( );
                _ddlDay.ClearSelection( );
            }
            _ddlYear.RenderControl(writer);
            writer.Write("&nbsp;");
            _ddlMonth.RenderControl(writer);
            writer.Write("&nbsp;");
            _ddlDay.RenderControl(writer);
            base.RenderContents(writer);
        }
        private string SelectedYear
        {
            get
            {
                EnsureChildControls( );
                return HttpContext.Current.Request[_ddlYear.UniqueID];
            }
        }
        private string SelectedMonth
        {
            get
            {
                EnsureChildControls( );
                return HttpContext.Current.Request[_ddlMonth.UniqueID];
            }
        }
        private string SelectedDay
        {
            get
            {
                EnsureChildControls( );
                return HttpContext.Current.Request[_ddlDay.UniqueID];
            }
        }
        public DateTime? GetSelectedDate( )
        {
            DateTime val;
            if (DateTime.TryParse(string.Format("{0}-{1}-{2}", SelectedYear, SelectedMonth, SelectedDay), out val))
                return val;
            else
                return null;
        }
        public DateTime? SelectedDate
        {
            get
            {
                if (!_selectedValue.HasValue)
                    _selectedValue = GetSelectedDate( );
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
            }
        }

        private void BindYear( )
        {
            int span = DateSpan > 0 ? DateSpan : 100;
            _ddlYear.Items.Clear( );
            _ddlYear.Items.Add("请选择");
            int year = DateTime.Now.Year;
            string val;
            for (int i = 0; i < span; i++)
            {
                val = (year - i).ToString( );
                _ddlYear.Items.Add(new ListItem { Text = val, Value = val });
            }
        }
        private void BindMonth( )
        {
            _ddlMonth.Items.Clear( );
            _ddlMonth.Items.Add("请选择");
            string val;
            for (int i = 1; i < 13; i++)
            {
                val = i.ToString( );
                _ddlMonth.Items.Add(new ListItem { Text = val, Value = val });
            }
        }
        private void BindDay( )
        {
            _ddlDay.Items.Clear( );
            _ddlDay.Items.Add("请选择");
            string val;
            for (int i = 1; i < 32; i++)
            {
                val = i.ToString( );
                _ddlDay.Items.Add(new ListItem { Text = val, Value = val });
            }
        }

        public int DateSpan { get; set; }

        [CssClassProperty]
        public string YearCssClass { get; set; }

        [CssClassProperty]
        public string MonthCssClass { get; set; }

        [CssClassProperty]
        public string DayCssClass { get; set; }
    }
}
