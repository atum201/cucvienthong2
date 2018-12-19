using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for MakeZip
/// </summary>
public class MakeZip
{

    private string strFileZipName;//Tên file zip sau khi nén thành công.
    private string strZipFilesName;//Tên các file cần nén.   
    private string[] arrstrZipFilesName;//Tên các file cần nén.
    public string[] arrZipFilesName
    {
        set { arrstrZipFilesName = value; }
        get { return arrstrZipFilesName; }
    }
    public string FileZipName
    {
        set { strFileZipName = value; }
        get { return strFileZipName; }
    }

    public string ZipFilesName
    {
        set { strZipFilesName = value; }
        get { return strZipFilesName; }
    }

    public bool MakeZip1()
    {
        bool returnvalue = true;
        try
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(FileZipName)))
            {
                //s.Password = strPassWord;
                //s.SetLevel(9); // 0 - store only to 9 - means best compression

                byte[] buffer = new byte[4096];

                foreach (string file in arrstrZipFilesName)
                {

                    // Using GetFileName makes the result compatible with XP
                    // as the resulting path is not absolute.
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    // Setup the entry data as required.

                    // Crc and size are handled by the library for seakable streams
                    // so no need to do them here.

                    // Could also use the last write time or similar for the file.
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                // Finish/Close arent needed strictly as the using statement does this automatically

                // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                // the created file would be invalid.
                s.Finish();

                // Close is important to wrap things up and unlock the file.
                s.Close();
            }
            returnvalue = true;
        }
        catch(Exception ex)
        {
            returnvalue = false;
        }
        return returnvalue;
    }


    public bool Zip()
    {
        bool returnvalue = true;
        try
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(ZipFilesName)))
            {
                byte[] buffer = new byte[4096];

                // Using GetFileName makes the result compatible with XP
                // as the resulting path is not absolute.
                ZipEntry entry = new ZipEntry(Path.GetFileName(FileZipName));

                // Setup the entry data as required.

                // Crc and size are handled by the library for seakable streams
                // so no need to do them here.

                // Could also use the last write time or similar for the file.
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);

                using (FileStream fs = File.OpenRead(FileZipName))
                {

                    // Using a fixed size buffer here makes no noticeable difference for output
                    // but keeps a lid on memory usage.
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
                // Finish/Close arent needed strictly as the using statement does this automatically

                // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                // the created file would be invalid.
                s.Finish();

                // Close is important to wrap things up and unlock the file.
                s.Close();
            }
            returnvalue = true;
        }
        catch
        {
            returnvalue = false;
        }
        return returnvalue;
    }

    public void Zip( string[]  ZipFileName)
    {       
            for (int i = 0; i < ZipFileName.Length; i++) {
                strZipFilesName = ZipFileName[i];
                this.Zip();
                
            
        }
       
    }

    public static bool Unzip(string FileZipPath, string UnzipToDirectory)
    {
        try
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(FileZipPath)))
            {
                // Tao mot thu muc vao trong thu muc co duong dan UnzipToDirectory
                // Ten thu muc tao trung voi FileZipPath
                string strFolderUnzip = Path.GetFileNameWithoutExtension(FileZipPath);
                // tao thu muc
               
                // Directory.CreateDirectory(UnzipToDirectory );


                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    // Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(UnzipToDirectory + "\\" + theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
