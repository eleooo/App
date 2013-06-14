using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UcDateCombox : UserControl
    {
        public UcDateCombox( )
        {
            InitializeComponent( );
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!_beginDate.HasValue)
                _beginDate = DateTime.Now.AddYears(-100);
            if (!_endDate.HasValue)
                _endDate = DateTime.Now;
            base.OnLoad(e);
        }
        private DateTime? _beginDate;
        private DateTime? _endDate;
        private DateTime? _value;

        public DateTime? BeginDate
        {
            get { return _beginDate; }
            set
            {
                if (value.HasValue && value != _beginDate)
                {
                    _beginDate = value;
                    InitCombox( );
                }
            }
        }
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (value.HasValue && value != _endDate)
                {
                    _endDate = value;
                    InitCombox( );
                }
            }
        }

        [Bindable(BindableSupport.Yes)]
        public DateTime? Value
        {
            get { _value = GetValue( ); return _value; }
            set
            {
                if (value.HasValue && value != _value)
                {
                    _value = value;
                    ValueChanged( );
                }
            }
        }
        void InitCombox( )
        {
            if (!_beginDate.HasValue || !_endDate.HasValue)
                return;
            for (int i = _endDate.Value.Year; i >= _beginDate.Value.Year; i--)
                cbYear.Items.Add(i.ToString( ));
            for (int i = 1; i <= 31; i++)
            {
                if (i <= 12)
                    cbMonth.Items.Add(i.ToString("00"));
                cbDate.Items.Add(i.ToString("00"));
            }
        }
        void ValueChanged( )
        {
            if (_value.HasValue)
            {
                cbYear.Text = _value.Value.Year.ToString( );
                cbMonth.Text = _value.Value.Month.ToString("00");
                cbDate.Text = _value.Value.Day.ToString("00");
            }
        }
        DateTime? GetValue( )
        {
            string date = string.Format("{0}-{1}-{2}", cbYear.Text, cbMonth.Text, cbDate.Text);
            DateTime dt;
            if (DateTime.TryParse(date, out dt))
                return dt;
            else
                return null;
        }
        private void cbYear_TextChanged(object sender, EventArgs e)
        {
            //_value = GetValue( );
        }

        private void cbMonth_TextChanged(object sender, EventArgs e)
        {
            //_value = GetValue( );
        }

        private void cbDate_TextChanged(object sender, EventArgs e)
        {
            //_value = GetValue( );
        }
    }
}
