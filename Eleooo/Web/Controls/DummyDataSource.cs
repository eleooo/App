using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;

namespace Eleooo.Web.Controls
{
    public enum QueryFetchType
    {
        DataTable,
        DataReader,
        Customize
    }

    internal sealed class DummyDataSource : ICollection, IEnumerable
    {
        // Fields
        private int dataItemCount;

        // Methods
        internal DummyDataSource(int dataItemCount)
        {
            this.dataItemCount = dataItemCount;
        }

        public void CopyTo(Array array, int index)
        {
            IEnumerator enumerator = this.GetEnumerator( );
            while (enumerator.MoveNext( ))
            {
                array.SetValue(enumerator.Current, index++);
            }
        }

        public IEnumerator GetEnumerator( )
        {
            return new DummyDataSourceEnumerator(this.dataItemCount);
        }

        // Properties
        public int Count
        {
            get
            {
                return this.dataItemCount;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        // Nested Types
        private class DummyDataSourceEnumerator : IEnumerator
        {
            // Fields
            private int count;
            private int index;

            // Methods
            public DummyDataSourceEnumerator(int count)
            {
                this.count = count;
                this.index = -1;
            }

            public bool MoveNext( )
            {
                this.index++;
                return (this.index < this.count);
            }

            public void Reset( )
            {
                this.index = -1;
            }

            // Properties
            public object Current
            {
                get
                {
                    return null;
                }
            }
        }
    }
}