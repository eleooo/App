using System;
using System.Collections.Generic;
using System.Web;
using ICSharpCode.SharpZipLib;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Eleooo.Common
{
    public class ZipHelper
    {
        // Methods
        private static void direct(string PathMaster, DirectoryInfo di, ref ZipOutputStream s, Crc32 crc)
        {
            foreach (DirectoryInfo dirNext in di.GetDirectories("*"))
            {
                FileInfo[] a = dirNext.GetFiles( );
                writeStream(PathMaster, a, ref s, crc);
                direct(PathMaster, dirNext, ref s, crc);
            }
        }

        public static void UnZip(string[] args)
        {
            UnZip(args[0], args[1]);
        }

        public static bool UnZip(string filename, string directory)
        {
            bool bSuccee = false;
            try
            {
                ZipEntry theEntry;
                int Cnt = directory.Length;
                if (directory.Substring(Cnt - 1) != @"\")
                {
                    directory = directory + @"\";
                }
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                ZipInputStream s = new ZipInputStream(File.OpenRead(filename));
                while ((theEntry = s.GetNextEntry( )) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (directoryName != string.Empty)
                    {
                        Directory.CreateDirectory(directory + directoryName);
                    }
                    if (fileName != string.Empty)
                    {
                        FileStream streamWriter = File.Create(Path.Combine(directory, (theEntry.Name.Substring(0, 1) == @"\") ? theEntry.Name.Substring(1) : theEntry.Name));
                        int size = 0x800;
                        byte[] data = new byte[0x800];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size <= 0)
                            {
                                break;
                            }
                            streamWriter.Write(data, 0, size);
                        }
                        streamWriter.Close( );
                    }
                }
                s.Close( );
                bSuccee = true;
            }
            catch (Exception)
            {
                throw;
            }
            return bSuccee;
        }

        private static void writeStream(string PathMaster, FileInfo[] a, ref ZipOutputStream s, Crc32 crc)
        {
            foreach (FileInfo fi in a)
            {
                FileStream fs = fi.OpenRead( );
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(fi.FullName.Replace(PathMaster, ""))
                {
                    DateTime = DateTime.Now,
                    Size = fs.Length
                };
                fs.Close( );
                crc.Reset( );
                crc.Update(buffer);
                entry.Crc = crc.Value;
                s.PutNextEntry(entry);
                s.Write(buffer, 0, buffer.Length);
            }
        }

        public static void ZipFile(string FileToZip, string ZipedFile)
        {
            FileInfo finfo = new FileInfo(FileToZip);
            if (!finfo.Exists)
            {
                throw new FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream fs = File.OpenRead(FileToZip);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close( );
            ZipOutputStream ZipStream = new ZipOutputStream(File.Create(ZipedFile));
            ZipEntry ZipEntry = new ZipEntry(finfo.Name);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(6);
            ZipStream.Write(buffer, 0, buffer.Length);
            ZipStream.Finish( );
            ZipStream.Close( );
        }

        public static void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
        {
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream StreamToZip = new FileStream(FileToZip, FileMode.Open, FileAccess.Read);
            ZipOutputStream ZipStream = new ZipOutputStream(File.Create(ZipedFile));
            ZipEntry ZipEntry = new ZipEntry(ZipedFile);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[BlockSize];
            int size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ZipStream.Finish( );
            ZipStream.Close( );
            StreamToZip.Close( );
        }

        public static void ZipFileDictory(string[] args)
        {
            ZipFileDictory(args[0], args[1]);
        }

        public static void ZipFileDictory(string directory, string filename)
        {
            try
            {
                new FastZip { CreateEmptyDirectories = true }.CreateZip(filename, directory, true, "");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


}