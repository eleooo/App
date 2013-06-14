using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace SubSonic.Utilities
{
    public class DataFormat
    {
        /// <summary>
        /// 将DataTable格式化成字节数组byte[]
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatData(DataTable dt)
        {
            if (dt == null)
                return null;
            byte[] binaryDataResult = null;
            using (MemoryStream memStream = new MemoryStream( ))
            {
                DataTableSurrogate dss = new DataTableSurrogate(dt);
                IFormatter brFormatter = new BinaryFormatter( );
                brFormatter.Serialize(memStream, dss);
                binaryDataResult = memStream.ToArray( );
            }
            return binaryDataResult;
        }

        /// <summary>
        /// 将DataSet格式化成字节数组byte[]
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatData(DataSet ds)
        {
            if (ds == null)
                return null;
            byte[] binaryDataResult = null;
            using (MemoryStream memStream = new MemoryStream( ))
            {
                DataSetSurrogate dss = new DataSetSurrogate(ds);
                IFormatter brFormatter = new BinaryFormatter( );
                brFormatter.Serialize(memStream, dss);
                binaryDataResult = memStream.ToArray( );
            }
            return binaryDataResult;
        }

        /// <summary>
        /// 将DataTable格式化成字节数组byte[]，并且已经经过压缩
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatDataZip(DataTable dt)
        {
            return Compress(GetBinFormatData(dt));
        }

        /// <summary>
        /// 将DataSet格式化成字节数组byte[]，并且已经经过压缩
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatDataZip(DataSet ds)
        {
            return Compress(GetBinFormatData(ds));
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            byte[] bData;
            using (MemoryStream temp = new MemoryStream( ))
            {
                using (MemoryStream ms = new MemoryStream( ))
                {
                    ms.Write(data, 0, data.Length);
                    ms.Position = 0L;
                    byte[] buffer = new byte[1024];
                    using (GZipStream stream = new GZipStream(ms, CompressionMode.Decompress, true))
                    {
                        int read = stream.Read(buffer, 0, buffer.Length);
                        while (read > 0)
                        {
                            temp.Write(buffer, 0, read);
                            read = stream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
                return temp.ToArray( );
            }
        }

        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            byte[] bData;
            using (MemoryStream ms = new MemoryStream( ))
            {
                using (GZipStream stream = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    stream.Write(data, 0, data.Length);
                }
                return ms.ToArray( );
            }
        }

        /// <summary>
        /// 将字节数组反序列化成DataTable对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ConvertToDataTable(byte[] binaryData)
        {
            if (binaryData == null || binaryData.Length == 0)
                return null;
            DataTable dt = null;
            using (MemoryStream ms = new MemoryStream(binaryData))
            {
                IFormatter brFormatter = new BinaryFormatter( );
                Object obj = brFormatter.Deserialize(ms);
                DataTableSurrogate dts = obj as DataTableSurrogate;
                if (dts != null)
                    dt = dts.ConvertToDataTable( );
            }
            return dt;
        }

        /// <summary>
        /// 将字节数组反序列化成DataSet对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ConvertToDataSet(byte[] binaryData)
        {
            if (binaryData == null || binaryData.Length == 0)
                return null;
            DataSet ds = null;
            using (MemoryStream ms = new MemoryStream(binaryData))
            {
                IFormatter brFormatter = new BinaryFormatter( );
                Object obj = brFormatter.Deserialize(ms);
                DataSetSurrogate dss = obj as DataSetSurrogate;
                if (dss != null)
                    ds = dss.ConvertToDataSet( );
            }
            return ds;
        }

        /// <summary>
        /// 将字节数组反解压后序列化成DataTable对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ConvertToDataTableUnZip(byte[] binaryData)
        {
            return ConvertToDataTable(Decompress(binaryData));
        }

        /// <summary>
        /// 将字节数组反解压后序列化成DataSet对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ConvertToDataSetUnZip(byte[] binaryData)
        {
            return ConvertToDataSet(Decompress(binaryData));
        }

        /// <summary>
        /// 将object格式化成字节数组byte[]
        /// </summary>
        /// <param name="data">object对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatData(object data)
        {
            if (data == null)
                return null;
            byte[] binaryDataResult = null;
            using (MemoryStream ms = new MemoryStream( ))
            {
                IFormatter brFormatter = new BinaryFormatter( );
                brFormatter.Serialize(ms, data);
                return ms.ToArray( );
            }
        }

        /// <summary>
        /// 将objec格式化成字节数组byte[]，并压缩
        /// </summary>
        /// <param name="data">object对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBinFormatDataZip(object data)
        {
            return Compress(GetBinFormatData(data));
        }

        /// <summary>
        /// 将字节数组反序列化成object对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>object对象</returns>
        public static object RetrieveObject(byte[] binaryData)
        {
            if (binaryData == null || binaryData.Length == 0)
                return null;
            using (MemoryStream memStream = new MemoryStream(binaryData))
            {
                IFormatter brFormatter = new BinaryFormatter( );
                return brFormatter.Deserialize(memStream);
            }
        }

        /// <summary>
        /// 将字节数组解压后反序列化成object对象
        /// </summary>
        /// <param name="binaryData">字节数组</param>
        /// <returns>object对象</returns>
        public static object RetrieveObjectUnZip(byte[] binaryData)
        {
            return RetrieveObject(Decompress(binaryData));
        }
    }
}