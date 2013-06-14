using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace SubSonic.Utilities
{
    [Serializable]
    internal class DataColumnSurrogate
    {
        // Fields
        private bool _allowNull;
        private bool _autoIncrement;
        private long _autoIncrementSeed;
        private long _autoIncrementStep;
        private string _caption;
        private MappingType _columnMapping;
        private string _columnName;
        private Type _dataType;
        private object _defaultValue;
        private string _expression;
        private Hashtable _extendedProperties;
        private int _maxLength;
        private string _namespace;
        private string _prefix;
        private bool _readOnly;

        // Methods
        public DataColumnSurrogate(DataColumn dc)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("The datacolumn parameter is null");
            }
            this._columnName = dc.ColumnName;
            this._namespace = dc.Namespace;
            this._dataType = dc.DataType;
            this._prefix = dc.Prefix;
            this._columnMapping = dc.ColumnMapping;
            this._allowNull = dc.AllowDBNull;
            this._autoIncrement = dc.AutoIncrement;
            this._autoIncrementStep = dc.AutoIncrementStep;
            this._autoIncrementSeed = dc.AutoIncrementSeed;
            this._caption = dc.Caption;
            this._defaultValue = dc.DefaultValue;
            this._readOnly = dc.ReadOnly;
            this._maxLength = dc.MaxLength;
            this._expression = dc.Expression;
            this._extendedProperties = new Hashtable( );
            if (dc.ExtendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in dc.ExtendedProperties.Keys)
                {
                    this._extendedProperties.Add(obj2, dc.ExtendedProperties[obj2]);
                }
            }
        }

        internal static bool AreDefaultValuesEqual(object o1, object o2)
        {
            if ((o1 == null) && (o2 == null))
            {
                return true;
            }
            if ((o1 == null) || (o2 == null))
            {
                return false;
            }
            return o1.Equals(o2);
        }

        public DataColumn ConvertToDataColumn( )
        {
            DataColumn column = new DataColumn
            {
                ColumnName = this._columnName,
                Namespace = this._namespace,
                DataType = this._dataType,
                Prefix = this._prefix,
                ColumnMapping = this._columnMapping,
                AllowDBNull = this._allowNull,
                AutoIncrement = this._autoIncrement,
                AutoIncrementStep = this._autoIncrementStep,
                AutoIncrementSeed = this._autoIncrementSeed,
                Caption = this._caption,
                DefaultValue = this._defaultValue,
                ReadOnly = this._readOnly,
                MaxLength = this._maxLength
            };
            Debug.Assert(this._extendedProperties != null);
            if (this._extendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in this._extendedProperties.Keys)
                {
                    column.ExtendedProperties.Add(obj2, this._extendedProperties[obj2]);
                }
            }
            return column;
        }

        internal bool IsSchemaIdentical(DataColumn dc)
        {
            Debug.Assert(dc != null);
            if ((((((dc.ColumnName != this._columnName) || (dc.Namespace != this._namespace)) || ((dc.DataType != this._dataType) || (dc.Prefix != this._prefix))) || (((dc.ColumnMapping != this._columnMapping) || (dc.ColumnMapping != this._columnMapping)) || ((dc.AllowDBNull != this._allowNull) || (dc.AutoIncrement != this._autoIncrement)))) || ((((dc.AutoIncrementStep != this._autoIncrementStep) || (dc.AutoIncrementSeed != this._autoIncrementSeed)) || ((dc.Caption != this._caption) || !AreDefaultValuesEqual(dc.DefaultValue, this._defaultValue))) || (dc.MaxLength != this._maxLength))) || (dc.Expression != this._expression))
            {
                return false;
            }
            return true;
        }

        internal void SetColumnExpression(DataColumn dc)
        {
            Debug.Assert(dc != null);
            if (!((this._expression == null) || this._expression.Equals(string.Empty)))
            {
                dc.Expression = this._expression;
            }
        }
    }
}
