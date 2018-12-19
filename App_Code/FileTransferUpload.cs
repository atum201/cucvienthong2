using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using DTQG_KV;
//using EFiling.Business;


namespace AppSrvLib
{
    /// <summary>
    /// A class to upload a file to a web server using WSE 3.0 web services, with the MTOM standard.
    /// To use this class, drag/drop an instance onto a windows form, and create event handlers for ProgressChanged
    /// and RunWorkerCompleted.  
    /// Make sure to specify the LocalFilePath before you call RunWorkerAsync() to begin the upload
    /// </summary>
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	     Modifier   
    ///	</Modified>       
    public class FileTransferUpload : FileTransferBase
    {

        private ClientRegistration objClienttransaction;
        private string strPackageCuoi = "";

        public FileTransferUpload()
        {

        }
        /// <summary>
        /// Khoi tao theo mot duong dan toi webservice
        /// </summary>
        /// <param name="UrlWebService">Duong dan toi Webservice</param>

        /// <summary>
        /// Start the upload operation synchronously.
        /// </summary>
        /// <Modified>
        ///	Name		         Date		     Comment 
        ///	TrườngTV	      27/06/2008	     Modifier   
        ///	</Modified>       
        public void RunWorkerSync(DoWorkEventArgs e)
        {
            OnDoWork(e);
        }


        /// <summary>
        /// Hash bufer 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>buffer </returns>        
        private byte[] HashBuffer(byte[] buffer)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash;
            hash = md5.ComputeHash(buffer);
            return (hash);
        }
        private byte[] Upload(byte[] buffer, long Offset)
        {

            //One file  :
            //-Dat chuck size -> chuck  
            //-Ta co chuck Buffer (Dinh Buffer vao dataMesage )
            // -Lay ra Hashvalue (Dinh vao DiviHash )
            // -Moi lan gui dinh kem Offset vao .
            //-Tat ca deu duoc dinh vao PbACKAGE 
            //set data 

            ClientRegistration.TOTAL_DATA_SIZE = buffer.Length;
            objClienttransaction = new ClientRegistration();
            byte[] bufferdata = buffer;
            this.objClienttransaction.SendDataMessage((bufferdata));
            //Set Hashvalue
            this.objClienttransaction.SetDivisionHash(this.HashBuffer(bufferdata));
            //Set Offset
            this.objClienttransaction.SetDivisionOffset(this.Offset);
            //Set file Name 
            this.objClienttransaction.SetFileName(this.LocalFileName);
            if (strPackageCuoi == "STOP") {
                objClienttransaction.SetDivisionRC(ASCIIEncoding.ASCII.GetBytes("STOP"));                
            }
            return objClienttransaction.SetPackagetoServer();

        }
        private string GetFilingbookID(string strLocalFilePath)
        {
            string strFilingbookid;
            string strLocalFileTemplete = strLocalFilePath;
            strFilingbookid = strLocalFileTemplete.Replace(".zip", "");
            strFilingbookid = strFilingbookid.Substring(strFilingbookid.LastIndexOf("_")).Replace("_", "");
            return strFilingbookid;

        }
        /// <summary> 
        /// This method starts the uplaod process. It supports cancellation, reporting progress, and exception handling.
        /// The argument is the start position, usually 0
        /// </summary>
        /// <Modified>
        ///	Name		         Date		     Comment 
        ///	TrườngTV	      27/06/2008	     Modifier   
        ///	</Modified>       
        protected override void OnDoWork(DoWorkEventArgs e)
        {

            base.OnDoWork(e);
          
            this.Offset = Int64.Parse(e.Argument.ToString());
            int numIterations = 0;	// this is used with a modulus of the sampleInterval to check if the chunk size should be adjusted.  it is started at 1 so that the first check will be skipped because it may involve an initial delay in connecting to the web service
            this.LocalFileName = Path.GetFileName(this.LocalFilePath);
            if (this.AutoSetChunkSize)
                this.ChunkSize = 1024;	// 16Kb default          

            if (!File.Exists(LocalFilePath))
                throw new Exception(String.Format("Could not find file {0}", LocalFilePath));

            long FileSize = new FileInfo(LocalFilePath).Length;
            string FileSizeDescription = CalcFileSize(FileSize); // e.g. "2.4 Gb" instead of 240000000000000 bytes etc...			
            byte[] Buffer = new byte[ChunkSize];    // this buffer stores each chunk, for sending to the web service via MTOM

            //Check exits file local 

            //.HistorySendData objHistoryError = new HistorySendData();
            //if (objHistoryError.CheckError(this.GetFilingbookID(LocalFilePath)))
            //{
            //    // Error is true -> Send Offset 
            //    this.Offset = long.Parse(objHistoryError.CrrentOffset.ToString());
            //    this.ChunkSize = objHistoryError.ChuckSize;
            //    //If send success then  delete record
            //}
            //else
            //{
            //    //Reset Offset 
            //    this.Offset = 0;
            //    //Defaufl chuck size                 
            //}
            //this.Offset = 0;
            // this.Offset = long.Parse(objHistoryError.CrrentOffset.ToString());


            using (FileStream fs = new FileStream(this.LocalFilePath, FileMode.Open, FileAccess.Read))
            {

                int BytesRead;
                // send the chunks to the web service one by one, until FileStream.Read() returns 0, meaning the entire file has been read.

                do
                {
                    
                    fs.Position = this.Offset;
                    BytesRead = fs.Read(Buffer, 0, ChunkSize);	// read the next chunk (if it exists) into the buffer.  the while loop will terminate if there is nothing left to read

                    // check if this is the last chunk and resize the bufer as needed to avoid sending a mostly empty buffer (could be 10Mb of 000000000000s in a large chunk)
                    if (BytesRead != Buffer.Length)
                    {
                        this.ChunkSize = BytesRead;
                        byte[] TrimmedBuffer = new byte[BytesRead];
                        Array.Copy(Buffer, TrimmedBuffer, BytesRead);
                        Buffer = TrimmedBuffer;	// the trimmed buffer should become the new 'buffer'
                        //Đây là Package cuối cùng -** Gửi cho Server 1 message 
                        //objClienttransaction.SetDivisionRC(ASCIIEncoding.ASCII.GetBytes("STOP"));             
                        
                        strPackageCuoi = "STOP";
                        
                    }
                    if (Buffer.Length == 0)
                    {
                        //Delete  
                        try
                        {
                            //new HistorySendData().Delete(GetFilingbookID(this.LocalFilePath));                                                     
                        }
                        catch (Exception ex)
                        {
                        }
                        break;	//more to send
                    }
                    try
                    {
                        //Đóng gói dữ liệu gửi lên Server      
                        byte[] byteBuf = this.Upload(Buffer, this.Offset);
                         byte[] bytePackage = this.WebService.AppendUpload(byteBuf);
                        objClienttransaction.GetPackageSettoClient(bytePackage);
                        string strReposeCode = objClienttransaction.GetStringDivisionRC();
                        if (strReposeCode != "GOOD")
                        {
                            //Retry.                        
                        }
                        else
                        {
                            // Offset is o((nly updated AFTER a successful send of the bytes. this helps the 'retry' feature to resume the upload from the current Offset position if AppendChunk fails.
                            //Kiểm tra AppRC trước khi quyết định tăng Offset
                            //Nêu OK -. tăng, ngược lại không tăng.
                            this.Offset += BytesRead;	// save the offset position for resume
                        }
                        // update the user interface
                        string SummaryText = String.Format("Transferred {0} / {1}", CalcFileSize(this.Offset), FileSizeDescription);
                        this.ReportProgress((int)(((decimal)Offset / (decimal)FileSize) * 100), SummaryText);
                    }
                    catch (Exception ex)
                    {
                        // is Error save current status to Database                                                 
                        Debug.WriteLine("Exception: " + ex.ToString());
                        // rewind the filestream and keep trying
                        fs.Position -= BytesRead;
                        Thread.Sleep(Timewait);
                        if (NumRetries++ >= MaxRetries) // too many retries, bail out
                        {
                            //PositionFileError posFileError = new PositionFileError(GetFilingbookID(LocalFilePath), this.ChunkSize, this.Offset, 0, 0);
                            //this.OnErrorSendFile(                             
                            //new HistorySendData().InsertError(GetFilingbookID(LocalFilePath), this.ChunkSize, this.Offset);
                            fs.Close();
                            throw new Exception(String.Format("Error occurred during upload, too many retries. \n{0}", ex.ToString()));
                        }
                    }
                    numIterations++;

                } while (BytesRead > 0 && !this.CancellationPending);              
                
            }

        }



    }
}