using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Globalization;

namespace SubSonic.Utilities
{
    [Serializable]
    public class DataSetSurrogate
    {
        // Fields
        private bool _caseSensitive;
        private string _datasetName;
        private DataTableSurrogate[] _dataTableSurrogates;
        private bool _enforceConstraints;
        private Hashtable _extendedProperties;
        private ArrayList _fkConstraints;
        private CultureInfo _locale;
        private string _namespace;
        private string _prefix;
        private ArrayList _relations;

        // Methods
        public DataSetSurrogate(DataSet ds)
        {
            if (ds == null)
            {
                throw new ArgumentNullException("The parameter dataset is null");
            }
            this._datasetName = ds.DataSetName;
            this._namespace = ds.Namespace;
            this._prefix = ds.Prefix;
            this._caseSensitive = ds.CaseSensitive;
            this._locale = ds.Locale;
            this._enforceConstraints = ds.EnforceConstraints;
            this._dataTableSurrogates = new DataTableSurrogate[ds.Tables.Count];
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                this._dataTableSurrogates[i] = new DataTableSurrogate(ds.Tables[i]);
            }
            this._fkConstraints = this.GetForeignKeyConstraints(ds);
            this._relations = this.GetRelations(ds);
            this._extendedProperties = new Hashtable( );
            if (ds.ExtendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in ds.ExtendedProperties.Keys)
                {
                    this._extendedProperties.Add(obj2, ds.ExtendedProperties[obj2]);
                }
            }
        }

        public DataSet ConvertToDataSet( )
        {
            DataSet ds = new DataSet( );
            this.ReadSchemaIntoDataSet(ds);
            this.ReadDataIntoDataSet(ds);
            return ds;
        }

        private ArrayList GetForeignKeyConstraints(DataSet ds)
        {
            Debug.Assert(ds != null);
            ArrayList list = new ArrayList( );
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                for (int j = 0; j < table.Constraints.Count; j++)
                {
                    System.Data.Constraint constraint = table.Constraints[j];
                    ForeignKeyConstraint constraint2 = constraint as ForeignKeyConstraint;
                    if (constraint2 != null)
                    {
                        string constraintName = constraint.ConstraintName;
                        int[] numArray = new int[constraint2.RelatedColumns.Length + 1];
                        numArray[0] = ds.Tables.IndexOf(constraint2.RelatedTable);
                        int index = 1;
                        while (index < numArray.Length)
                        {
                            numArray[index] = constraint2.RelatedColumns[index - 1].Ordinal;
                            index++;
                        }
                        int[] numArray2 = new int[constraint2.Columns.Length + 1];
                        numArray2[0] = i;
                        for (index = 1; index < numArray2.Length; index++)
                        {
                            numArray2[index] = constraint2.Columns[index - 1].Ordinal;
                        }
                        ArrayList list2 = new ArrayList( );
                        list2.Add(constraintName);
                        list2.Add(numArray);
                        list2.Add(numArray2);
                        list2.Add(new int[] 
                                { 
                                    Convert.ToInt32(constraint2.AcceptRejectRule),
                                    Convert.ToInt32(constraint2.UpdateRule), 
                                    Convert.ToInt32(constraint2.DeleteRule)
                                });
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
            }
            return list;
        }

        private ArrayList GetRelations(DataSet ds)
        {
            Debug.Assert(ds != null);
            ArrayList list = new ArrayList( );
            foreach (DataRelation relation in ds.Relations)
            {
                string relationName = relation.RelationName;
                int[] numArray = new int[relation.ParentColumns.Length + 1];
                numArray[0] = ds.Tables.IndexOf(relation.ParentTable);
                int index = 1;
                while (index < numArray.Length)
                {
                    numArray[index] = relation.ParentColumns[index - 1].Ordinal;
                    index++;
                }
                int[] numArray2 = new int[relation.ChildColumns.Length + 1];
                numArray2[0] = ds.Tables.IndexOf(relation.ChildTable);
                for (index = 1; index < numArray2.Length; index++)
                {
                    numArray2[index] = relation.ChildColumns[index - 1].Ordinal;
                }
                ArrayList list2 = new ArrayList( );
                list2.Add(relationName);
                list2.Add(numArray);
                list2.Add(numArray2);
                list2.Add(relation.Nested);
                Hashtable hashtable = new Hashtable( );
                if (relation.ExtendedProperties.Keys.Count > 0)
                {
                    foreach (object obj2 in relation.ExtendedProperties.Keys)
                    {
                        hashtable.Add(obj2, relation.ExtendedProperties[obj2]);
                    }
                }
                list2.Add(hashtable);
                list.Add(list2);
            }
            return list;
        }

        private bool IsSchemaIdentical(DataSet ds)
        {
            Debug.Assert(ds != null);
            if ((ds.DataSetName != this._datasetName) || (ds.Namespace != this._namespace))
            {
                return false;
            }
            Debug.Assert(this._dataTableSurrogates != null);
            if (ds.Tables.Count != this._dataTableSurrogates.Length)
            {
                return false;
            }
            return true;
        }

        public void ReadDataIntoDataSet(DataSet ds)
        {
            if (ds == null)
            {
                throw new ArgumentNullException("The dataset parameter cannot be null");
            }
            ArrayList readOnlyList = this.SuppressReadOnly(ds);
            ArrayList constraintRulesList = this.SuppressConstraintRules(ds);
            Debug.Assert(this.IsSchemaIdentical(ds));
            Debug.Assert(this._dataTableSurrogates != null);
            Debug.Assert(ds.Tables.Count == this._dataTableSurrogates.Length);
            bool enforceConstraints = ds.EnforceConstraints;
            ds.EnforceConstraints = false;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                this._dataTableSurrogates[i].ReadDataIntoDataTable(ds.Tables[i], false);
            }
            ds.EnforceConstraints = enforceConstraints;
            this.ResetReadOnly(ds, readOnlyList);
            this.ResetConstraintRules(ds, constraintRulesList);
        }

        public void ReadSchemaIntoDataSet(DataSet ds)
        {
            DataTable table;
            if (ds == null)
            {
                throw new ArgumentNullException("The dataset parameter cannot be null");
            }
            ds.DataSetName = this._datasetName;
            ds.Namespace = this._namespace;
            ds.Prefix = this._prefix;
            ds.CaseSensitive = this._caseSensitive;
            ds.Locale = this._locale;
            ds.EnforceConstraints = this._enforceConstraints;
            Debug.Assert(this._dataTableSurrogates != null);
            foreach (DataTableSurrogate surrogate in this._dataTableSurrogates)
            {
                table = new DataTable( );
                surrogate.ReadSchemaIntoDataTable(table);
                ds.Tables.Add(table);
            }
            this.SetForeignKeyConstraints(ds, this._fkConstraints);
            this.SetRelations(ds, this._relations);
            Debug.Assert(this._dataTableSurrogates != null);
            Debug.Assert(ds.Tables.Count == this._dataTableSurrogates.Length);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                table = ds.Tables[i];
                this._dataTableSurrogates[i].SetColumnExpressions(table);
            }
            Debug.Assert(this._extendedProperties != null);
            if (this._extendedProperties.Keys.Count > 0)
            {
                foreach (object obj2 in this._extendedProperties.Keys)
                {
                    ds.ExtendedProperties.Add(obj2, this._extendedProperties[obj2]);
                }
            }
        }

        private void ResetConstraintRules(DataSet ds, ArrayList constraintRulesList)
        {
            Debug.Assert(ds != null);
            Debug.Assert(constraintRulesList != null);
            foreach (ArrayList list in constraintRulesList)
            {
                Debug.Assert(list.Count == 2);
                int[] numArray = (int[])list[0];
                int[] numArray2 = (int[])list[1];
                Debug.Assert(numArray.Length == 2);
                int num = numArray[0];
                int num2 = numArray[1];
                Debug.Assert(ds.Tables.Count > num);
                DataTable table = ds.Tables[num];
                Debug.Assert(table.Constraints.Count > num2);
                ForeignKeyConstraint constraint = (ForeignKeyConstraint)table.Constraints[num2];
                Debug.Assert(numArray2.Length == 3);
                constraint.AcceptRejectRule = (AcceptRejectRule)numArray2[0];
                constraint.UpdateRule = (Rule)numArray2[1];
                constraint.DeleteRule = (Rule)numArray2[2];
            }
        }

        private void ResetReadOnly(DataSet ds, ArrayList readOnlyList)
        {
            Debug.Assert(ds != null);
            Debug.Assert(readOnlyList != null);
            foreach (object obj2 in readOnlyList)
            {
                int[] numArray = (int[])obj2;
                Debug.Assert(numArray.Length == 2);
                int num = numArray[0];
                int num2 = numArray[1];
                Debug.Assert(ds.Tables.Count > num);
                Debug.Assert(ds.Tables[num].Columns.Count > num2);
                DataColumn column = ds.Tables[num].Columns[num2];
                Debug.Assert(column != null);
                column.ReadOnly = true;
            }
        }

        private void SetForeignKeyConstraints(DataSet ds, ArrayList constraintList)
        {
            Debug.Assert(ds != null);
            Debug.Assert(constraintList != null);
            foreach (ArrayList list in constraintList)
            {
                Debug.Assert(list.Count == 5);
                string constraintName = (string)list[0];
                int[] numArray = (int[])list[1];
                int[] numArray2 = (int[])list[2];
                int[] numArray3 = (int[])list[3];
                Hashtable hashtable = (Hashtable)list[4];
                Debug.Assert(numArray.Length >= 1);
                DataColumn[] parentColumns = new DataColumn[numArray.Length - 1];
                int index = 0;
                while (index < parentColumns.Length)
                {
                    Debug.Assert(ds.Tables.Count > numArray[0]);
                    Debug.Assert(ds.Tables[numArray[0]].Columns.Count > numArray[index + 1]);
                    parentColumns[index] = ds.Tables[numArray[0]].Columns[numArray[index + 1]];
                    index++;
                }
                Debug.Assert(numArray2.Length >= 1);
                DataColumn[] childColumns = new DataColumn[numArray2.Length - 1];
                for (index = 0; index < childColumns.Length; index++)
                {
                    Debug.Assert(ds.Tables.Count > numArray2[0]);
                    Debug.Assert(ds.Tables[numArray2[0]].Columns.Count > numArray2[index + 1]);
                    childColumns[index] = ds.Tables[numArray2[0]].Columns[numArray2[index + 1]];
                }
                ForeignKeyConstraint constraint = new ForeignKeyConstraint(constraintName, parentColumns, childColumns);
                Debug.Assert(numArray3.Length == 3);
                constraint.AcceptRejectRule = (AcceptRejectRule)numArray3[0];
                constraint.UpdateRule = (Rule)numArray3[1];
                constraint.DeleteRule = (Rule)numArray3[2];
                Debug.Assert(hashtable != null);
                if (hashtable.Keys.Count > 0)
                {
                    foreach (object obj2 in hashtable.Keys)
                    {
                        constraint.ExtendedProperties.Add(obj2, hashtable[obj2]);
                    }
                }
                Debug.Assert(ds.Tables.Count > numArray2[0]);
                ds.Tables[numArray2[0]].Constraints.Add(constraint);
            }
        }

        private void SetRelations(DataSet ds, ArrayList relationList)
        {
            Debug.Assert(ds != null);
            Debug.Assert(relationList != null);
            foreach (ArrayList list in relationList)
            {
                Debug.Assert(list.Count == 5);
                string relationName = (string)list[0];
                int[] numArray = (int[])list[1];
                int[] numArray2 = (int[])list[2];
                bool flag = (bool)list[3];
                Hashtable hashtable = (Hashtable)list[4];
                Debug.Assert(numArray.Length >= 1);
                DataColumn[] parentColumns = new DataColumn[numArray.Length - 1];
                int index = 0;
                while (index < parentColumns.Length)
                {
                    Debug.Assert(ds.Tables.Count > numArray[0]);
                    Debug.Assert(ds.Tables[numArray[0]].Columns.Count > numArray[index + 1]);
                    parentColumns[index] = ds.Tables[numArray[0]].Columns[numArray[index + 1]];
                    index++;
                }
                Debug.Assert(numArray2.Length >= 1);
                DataColumn[] childColumns = new DataColumn[numArray2.Length - 1];
                for (index = 0; index < childColumns.Length; index++)
                {
                    Debug.Assert(ds.Tables.Count > numArray2[0]);
                    Debug.Assert(ds.Tables[numArray2[0]].Columns.Count > numArray2[index + 1]);
                    childColumns[index] = ds.Tables[numArray2[0]].Columns[numArray2[index + 1]];
                }
                DataRelation relation = new DataRelation(relationName, parentColumns, childColumns, false)
                {
                    Nested = flag
                };
                Debug.Assert(hashtable != null);
                if (hashtable.Keys.Count > 0)
                {
                    foreach (object obj2 in hashtable.Keys)
                    {
                        relation.ExtendedProperties.Add(obj2, hashtable[obj2]);
                    }
                }
                ds.Relations.Add(relation);
            }
        }

        private ArrayList SuppressConstraintRules(DataSet ds)
        {
            Debug.Assert(ds != null);
            ArrayList list = new ArrayList( );
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                for (int j = 0; j < table.Constraints.Count; j++)
                {
                    System.Data.Constraint constraint = table.Constraints[j];
                    if (constraint is ForeignKeyConstraint)
                    {
                        ForeignKeyConstraint constraint2 = (ForeignKeyConstraint)constraint;
                        ArrayList list2 = new ArrayList( );
                        list2.Add(new int[] { i, j });
                        //list2.Add(new int[] { constraint2.AcceptRejectRule, constraint2.UpdateRule, constraint2.DeleteRule });
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
            return list;
        }

        private ArrayList SuppressReadOnly(DataSet ds)
        {
            Debug.Assert(ds != null);
            ArrayList list = new ArrayList( );
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable table = ds.Tables[i];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if ((table.Columns[j].Expression == string.Empty) && table.Columns[j].ReadOnly)
                    {
                        table.Columns[j].ReadOnly = false;
                        list.Add(new int[] { i, j });
                    }
                }
            }
            return list;
        }
    }

}
