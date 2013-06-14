using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;

namespace SubSonic.Utilities
{
    [Serializable]
    public class DataTableSurrogate
    {
        // Fields
        private bool _caseSensitive;
        private Hashtable _colErrors;
        private DataColumnSurrogate[] _dataColumnSurrogates;
        private string _displayExpression;
        private Hashtable _extendedProperties;
        private CultureInfo _locale;
        private int _minimumCapacity;
        private string _namespace;
        private string _prefix;
        private object[][] _records;
        private Hashtable _rowErrors;
        private BitArray _rowStates;
        private string _tableName;
        private ArrayList _uniqueConstraints;

        // Methods
        public DataTableSurrogate(DataTable dt)
        {
            int num;
            this._rowErrors = new Hashtable( );
            this._colErrors = new Hashtable( );
            if (dt == null)
            {
                throw new ArgumentNullException("The parameter dt is null");
            }
            this._tableName = dt.TableName;
            this._namespace = dt.Namespace;
            this._prefix = dt.Prefix;
            this._caseSensitive = dt.CaseSensitive;
            this._locale = dt.Locale;
            this._displayExpression = dt.DisplayExpression;
            this._minimumCapacity = dt.MinimumCapacity;
            this._dataColumnSurrogates = new DataColumnSurrogate[dt.Columns.Count];
            for (num = 0; num < dt.Columns.Count; num++)
            {
                this._dataColumnSurrogates[num] = new DataColumnSurrogate(dt.Columns[num]);
            }
            this._uniqueConstraints = this.GetUniqueConstraints(dt);
            this._extendedProperties = new Hashtable( );
            if (dt.ExtendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in dt.ExtendedProperties.Keys)
                {
                    this._extendedProperties.Add(obj2, dt.ExtendedProperties[obj2]);
                }
            }
            if (dt.Rows.Count > 0)
            {
                this._rowStates = new BitArray(dt.Rows.Count << 1);
                this._records = new object[dt.Columns.Count][];
                for (num = 0; num < dt.Columns.Count; num++)
                {
                    this._records[num] = new object[dt.Rows.Count << 1];
                }
                for (num = 0; num < dt.Rows.Count; num++)
                {
                    this.GetRecords(dt.Rows[num], num << 1);
                }
            }
        }

        private DataRow ConstructRow(DataTable dt, DataRowState rowState, int bitIndex)
        {
            int num2;
            Debug.Assert(dt != null);
            Debug.Assert(this._records != null);
            DataRow row = dt.NewRow( );
            int count = dt.Columns.Count;
            Debug.Assert(this._records.Length == count);
            switch (rowState)
            {
                case DataRowState.Unchanged:
                    for (num2 = 0; num2 < count; num2++)
                    {
                        Debug.Assert(this._records[num2].Length > bitIndex);
                        row[num2] = this._records[num2][bitIndex];
                    }
                    dt.Rows.Add(row);
                    row.AcceptChanges( );
                    return row;

                case DataRowState.Added:
                    num2 = 0;
                    while (num2 < count)
                    {
                        Debug.Assert(this._records[num2].Length > (bitIndex + 1));
                        row[num2] = this._records[num2][bitIndex + 1];
                        num2++;
                    }
                    dt.Rows.Add(row);
                    return row;

                case DataRowState.Deleted:
                    for (num2 = 0; num2 < count; num2++)
                    {
                        Debug.Assert(this._records[num2].Length > bitIndex);
                        row[num2] = this._records[num2][bitIndex];
                    }
                    dt.Rows.Add(row);
                    row.AcceptChanges( );
                    row.Delete( );
                    return row;

                case DataRowState.Modified:
                    for (num2 = 0; num2 < count; num2++)
                    {
                        Debug.Assert(this._records[num2].Length > bitIndex);
                        row[num2] = this._records[num2][bitIndex];
                    }
                    dt.Rows.Add(row);
                    row.AcceptChanges( );
                    row.BeginEdit( );
                    for (num2 = 0; num2 < count; num2++)
                    {
                        Debug.Assert(this._records[num2].Length > (bitIndex + 1));
                        row[num2] = this._records[num2][bitIndex + 1];
                    }
                    row.EndEdit( );
                    return row;
            }
            throw new InvalidEnumArgumentException(string.Format("Unrecognized row state {0}", rowState));
        }

        public DataRow ConvertToDataRow(DataTable dt, int bitIndex)
        {
            DataRowState rowState = this.ConvertToRowState(bitIndex);
            DataRow row = this.ConstructRow(dt, rowState, bitIndex);
            this.ConvertToRowError(row, bitIndex >> 1);
            return row;
        }

        public DataTable ConvertToDataTable( )
        {
            DataTable dt = new DataTable( );
            this.ReadSchemaIntoDataTable(dt);
            this.ReadDataIntoDataTable(dt);
            return dt;
        }

        private void ConvertToRowError(DataRow row, int rowIndex)
        {
            Debug.Assert(row != null);
            Debug.Assert(this._rowErrors != null);
            Debug.Assert(this._colErrors != null);
            if (this._rowErrors.ContainsKey(rowIndex))
            {
                row.RowError = (string)this._rowErrors[rowIndex];
            }
            if (this._colErrors.ContainsKey(rowIndex))
            {
                ArrayList list = (ArrayList)this._colErrors[rowIndex];
                int[] numArray = (int[])list[0];
                string[] strArray = (string[])list[1];
                Debug.Assert(numArray.Length == strArray.Length);
                for (int i = 0; i < numArray.Length; i++)
                {
                    row.SetColumnError(numArray[i], strArray[i]);
                }
            }
        }

        private DataRowState ConvertToRowState(int bitIndex)
        {
            Debug.Assert(this._rowStates != null);
            Debug.Assert(this._rowStates.Length > bitIndex);
            bool flag = this._rowStates[bitIndex];
            bool flag2 = this._rowStates[bitIndex + 1];
            if (!(flag || flag2))
            {
                return DataRowState.Unchanged;
            }
            if (!(flag || !flag2))
            {
                return DataRowState.Added;
            }
            if (!(!flag || flag2))
            {
                return DataRowState.Modified;
            }
            if (!flag || !flag2)
            {
                throw new ArgumentException("Unrecognized bitpattern");
            }
            return DataRowState.Deleted;
        }

        private void ConvertToSurrogateRecords(DataRow row, int bitIndex)
        {
            int num2;
            Debug.Assert(row != null);
            Debug.Assert(this._records != null);
            int count = row.Table.Columns.Count;
            DataRowState rowState = row.RowState;
            Debug.Assert(this._records.Length == count);
            if (rowState != DataRowState.Added)
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    Debug.Assert(this._records[num2].Length > bitIndex);
                    this._records[num2][bitIndex] = row[num2, DataRowVersion.Original];
                }
            }
            if ((rowState != DataRowState.Unchanged) && (rowState != DataRowState.Deleted))
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    Debug.Assert(this._records[num2].Length > (bitIndex + 1));
                    this._records[num2][bitIndex + 1] = row[num2, DataRowVersion.Current];
                }
            }
        }

        private void ConvertToSurrogateRowError(DataRow row, int rowIndex)
        {
            Debug.Assert(row != null);
            Debug.Assert(this._rowErrors != null);
            Debug.Assert(this._colErrors != null);
            if (row.HasErrors)
            {
                this._rowErrors.Add(rowIndex, row.RowError);
                DataColumn[] columnsInError = row.GetColumnsInError( );
                if (columnsInError.Length > 0)
                {
                    int[] numArray = new int[columnsInError.Length];
                    string[] strArray = new string[columnsInError.Length];
                    for (int i = 0; i < columnsInError.Length; i++)
                    {
                        numArray[i] = columnsInError[i].Ordinal;
                        strArray[i] = row.GetColumnError(columnsInError[i]);
                    }
                    ArrayList list = new ArrayList( );
                    list.Add(numArray);
                    list.Add(strArray);
                    this._colErrors.Add(rowIndex, list);
                }
            }
        }

        private void ConvertToSurrogateRowState(DataRowState rowState, int bitIndex)
        {
            Debug.Assert(this._rowStates != null);
            Debug.Assert(this._rowStates.Length > bitIndex);
            switch (rowState)
            {
                case DataRowState.Unchanged:
                    this._rowStates[bitIndex] = false;
                    this._rowStates[bitIndex + 1] = false;
                    return;

                case DataRowState.Added:
                    this._rowStates[bitIndex] = false;
                    this._rowStates[bitIndex + 1] = true;
                    return;

                case DataRowState.Deleted:
                    this._rowStates[bitIndex] = true;
                    this._rowStates[bitIndex + 1] = true;
                    return;

                case DataRowState.Modified:
                    this._rowStates[bitIndex] = true;
                    this._rowStates[bitIndex + 1] = false;
                    return;
            }
            throw new InvalidEnumArgumentException(string.Format("Unrecognized row state {0}", rowState));
        }

        private void GetRecords(DataRow row, int bitIndex)
        {
            Debug.Assert(row != null);
            this.ConvertToSurrogateRowState(row.RowState, bitIndex);
            this.ConvertToSurrogateRecords(row, bitIndex);
            this.ConvertToSurrogateRowError(row, bitIndex >> 1);
        }

        private ArrayList GetUniqueConstraints(DataTable dt)
        {
            Debug.Assert(dt != null);
            ArrayList list = new ArrayList( );
            for (int i = 0; i < dt.Constraints.Count; i++)
            {
                System.Data.Constraint constraint = dt.Constraints[i];
                UniqueConstraint constraint2 = constraint as UniqueConstraint;
                if (constraint2 != null)
                {
                    string constraintName = constraint.ConstraintName;
                    int[] numArray = new int[constraint2.Columns.Length];
                    for (int j = 0; j < numArray.Length; j++)
                    {
                        numArray[j] = constraint2.Columns[j].Ordinal;
                    }
                    ArrayList list2 = new ArrayList( );
                    list2.Add(constraintName);
                    list2.Add(numArray);
                    list2.Add(constraint2.IsPrimaryKey);
                    Hashtable hashtable = new Hashtable( );
                    if (constraint2.ExtendedProperties.Keys.Count > 0)
                    {
                        foreach (object obj2 in constraint2.ExtendedProperties.Keys)
                        {
                            hashtable.Add(obj2, constraint2.ExtendedProperties[obj2]);
                        }
                    }
                    list2.Add(hashtable);
                    list.Add(list2);
                }
            }
            return list;
        }

        private bool IsSchemaIdentical(DataTable dt)
        {
            Debug.Assert(dt != null);
            if ((dt.TableName != this._tableName) || (dt.Namespace != this._namespace))
            {
                return false;
            }
            Debug.Assert(this._dataColumnSurrogates != null);
            if (dt.Columns.Count != this._dataColumnSurrogates.Length)
            {
                return false;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn dc = dt.Columns[i];
                DataColumnSurrogate surrogate = this._dataColumnSurrogates[i];
                if (!surrogate.IsSchemaIdentical(dc))
                {
                    return false;
                }
            }
            return true;
        }

        public void ReadDataIntoDataTable(DataTable dt)
        {
            this.ReadDataIntoDataTable(dt, true);
        }

        internal void ReadDataIntoDataTable(DataTable dt, bool suppressSchema)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("The datatable parameter cannot be null");
            }
            Debug.Assert(this.IsSchemaIdentical(dt));
            ArrayList readOnlyList = null;
            ArrayList constraintRulesList = null;
            if (suppressSchema)
            {
                readOnlyList = this.SuppressReadOnly(dt);
                constraintRulesList = this.SuppressConstraintRules(dt);
            }
            if ((this._records != null) && (dt.Columns.Count > 0))
            {
                Debug.Assert(this._records.Length > 0);
                int num = this._records[0].Length >> 1;
                for (int i = 0; i < num; i++)
                {
                    this.ConvertToDataRow(dt, i << 1);
                }
            }
            if (suppressSchema)
            {
                this.ResetReadOnly(dt, readOnlyList);
                this.ResetConstraintRules(dt, constraintRulesList);
            }
        }

        public void ReadSchemaIntoDataTable(DataTable dt)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("The datatable parameter cannot be null");
            }
            dt.TableName = this._tableName;
            dt.Namespace = this._namespace;
            dt.Prefix = this._prefix;
            dt.CaseSensitive = this._caseSensitive;
            dt.Locale = this._locale;
            dt.DisplayExpression = this._displayExpression;
            dt.MinimumCapacity = this._minimumCapacity;
            Debug.Assert(this._dataColumnSurrogates != null);
            for (int i = 0; i < this._dataColumnSurrogates.Length; i++)
            {
                DataColumn column = this._dataColumnSurrogates[i].ConvertToDataColumn( );
                dt.Columns.Add(column);
            }
            this.SetUniqueConstraints(dt, this._uniqueConstraints);
            Debug.Assert(this._extendedProperties != null);
            if (this._extendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in this._extendedProperties.Keys)
                {
                    dt.ExtendedProperties.Add(obj2, this._extendedProperties[obj2]);
                }
            }
        }

        private void ResetConstraintRules(DataTable dt, ArrayList constraintRulesList)
        {
            Debug.Assert(dt != null);
            Debug.Assert(constraintRulesList != null);
            DataSet dataSet = dt.DataSet;
            foreach (ArrayList list in constraintRulesList)
            {
                Debug.Assert(list.Count == 2);
                int[] numArray = (int[])list[0];
                int[] numArray2 = (int[])list[1];
                Debug.Assert(numArray.Length == 2);
                int num = numArray[0];
                int num2 = numArray[1];
                Debug.Assert(dataSet.Tables.Count > num);
                DataTable table = dataSet.Tables[num];
                Debug.Assert(table.Constraints.Count > num2);
                ForeignKeyConstraint constraint = (ForeignKeyConstraint)table.Constraints[num2];
                Debug.Assert(numArray2.Length == 3);
                constraint.AcceptRejectRule = (AcceptRejectRule)numArray2[0];
                constraint.UpdateRule = (Rule)numArray2[1];
                constraint.DeleteRule = (Rule)numArray2[2];
            }
        }

        private void ResetReadOnly(DataTable dt, ArrayList readOnlyList)
        {
            Debug.Assert(dt != null);
            Debug.Assert(readOnlyList != null);
            DataSet dataSet = dt.DataSet;
            foreach (object obj2 in readOnlyList)
            {
                int num = (int)obj2;
                Debug.Assert(dt.Columns.Count > num);
                dt.Columns[num].ReadOnly = true;
            }
        }

        internal void SetColumnExpressions(DataTable dt)
        {
            Debug.Assert(dt != null);
            Debug.Assert(this._dataColumnSurrogates != null);
            Debug.Assert(dt.Columns.Count == this._dataColumnSurrogates.Length);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn dc = dt.Columns[i];
                this._dataColumnSurrogates[i].SetColumnExpression(dc);
            }
        }

        private void SetUniqueConstraints(DataTable dt, ArrayList constraintList)
        {
            Debug.Assert(dt != null);
            Debug.Assert(constraintList != null);
            foreach (ArrayList list in constraintList)
            {
                Debug.Assert(list.Count == 4);
                string name = (string)list[0];
                int[] numArray = (int[])list[1];
                bool isPrimaryKey = (bool)list[2];
                Hashtable hashtable = (Hashtable)list[3];
                DataColumn[] columns = new DataColumn[numArray.Length];
                for (int i = 0; i < numArray.Length; i++)
                {
                    Debug.Assert(dt.Columns.Count > numArray[i]);
                    columns[i] = dt.Columns[numArray[i]];
                }
                UniqueConstraint constraint = new UniqueConstraint(name, columns, isPrimaryKey);
                Debug.Assert(hashtable != null);
                if (hashtable.Keys.Count > 0)
                {
                    foreach (object obj2 in hashtable.Keys)
                    {
                        constraint.ExtendedProperties.Add(obj2, hashtable[obj2]);
                    }
                }
                dt.Constraints.Add(constraint);
            }
        }

        private ArrayList SuppressConstraintRules(DataTable dt)
        {
            Debug.Assert(dt != null);
            ArrayList list = new ArrayList( );
            DataSet dataSet = dt.DataSet;
            if (dataSet != null)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    DataTable table = dataSet.Tables[i];
                    for (int j = 0; j < table.Constraints.Count; j++)
                    {
                        System.Data.Constraint constraint = table.Constraints[j];
                        if (constraint is ForeignKeyConstraint)
                        {
                            System.Data.ForeignKeyConstraint constraint2 = (ForeignKeyConstraint)constraint;
                            if (constraint2.RelatedTable == dt)
                            {
                                ArrayList list2 = new ArrayList( );
                                list2.Add(new int[] { i, j });
                                list2.Add(new int[] 
                                { 
                                    Convert.ToInt32(constraint2.AcceptRejectRule),
                                    Convert.ToInt32(constraint2.UpdateRule), 
                                    Convert.ToInt32(constraint2.DeleteRule)
                                });
                                list.Add(list2);
                                constraint2.AcceptRejectRule = AcceptRejectRule.None;
                                constraint2.UpdateRule = Rule.None;
                                constraint2.DeleteRule = Rule.None;
                            }
                        }
                    }
                }
            }
            return list;
        }

        private ArrayList SuppressReadOnly(DataTable dt)
        {
            Debug.Assert(dt != null);
            ArrayList list = new ArrayList( );
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((dt.Columns[i].Expression == string.Empty) && dt.Columns[i].ReadOnly)
                {
                    list.Add(i);
                }
            }
            return list;
        }
    }


}
