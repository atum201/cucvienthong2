using System;
using System.Collections.Generic;
using System.Text;


namespace DTQG_KV
{
    /// <summary>
    /// Thuc hien cac business cua phan UserRegister
    /// </summary>
    /// <Modified>
    ///	Name		       Date		     Comment 
    ///	TrườngTV	  05/06/2008	         Thêm mới
    ///	</Modified>
    public class ClientRegistration : AppSrvLib.EFilingPackage
    {
        
        
        
        /// <summary>
        /// Overload 
        /// </summary>
        public ClientRegistration()
        {

            mPackageDataMessage = new byte[TOTAL_DATA_SIZE];
        }
        /// <summary>
        ///  Lấy package từ server về đưa vào client 
        /// </summary>
        /// <param name="package">Package</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  15/06/2008	         Thêm mới
        ///	</Modified>
        public void GetPackageSettoClient(byte[] package)
        {
            if (package == null)
                return;
            //Kiểm tra nếu kích thước Package <= header -> chưa có DATA 
            if (package.Length <= TOTAL_HEADER_SIZE)
            {
                //Coppy vào Header
                Array.Copy(package, FIRST_INDEX, mPackageHeaderNoMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
            }
            else
            {
                //Đã có data
                Array.Copy(package, FIRST_INDEX, mPackageHeaderNoMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
                Array.Copy(package, TOTAL_HEADER_SIZE, mPackageDataMessage, FIRST_INDEX, TOTAL_DATA_SIZE);
            }
        }

        /// <summary>
        /// Tạo mới transaction 
        /// </summary>
        /// <returns>byte array </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  15/06/2008	    Thêm mới
        ///	</Modified>
        public byte[] CreateBeginTransaction()
        {
            // Khoi tao mot package begin transaction
            // Tao transaction_type
            this.SetTransactionType(KIND_TRANSACTION_BEGIN);
            // tao transaction_id
            // this.SetTransactionID(null);    
            //tra ve package
            return this.mPackageHeaderNoMessage;
        }
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="package"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public void SendXML(string strXML)
        {
            //Kích thước file
            long longLengthXML = strXML.Length;
            byte[] bytelengthXML = new byte[TOTAL_DATA_SIZE];
            //file data
            byte[] byteData = new byte[longLengthXML];
            //byte lengt XML 
            bytelengthXML = EFilingCommunication.ConverttoLong16(longLengthXML);
            //byte data 
            byteData = ASCIIEncoding.ASCII.GetBytes(strXML);
            //Gep data vào package
            //Ghep byte length vào mHeader
            Array.Copy(bytelengthXML, FIRST_INDEX, mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE, TOTAL_BYTES);
            //Ghep byte data vào mData 
            Array.Copy(byteData, FIRST_INDEX, mPackageDataMessage, FIRST_INDEX, longLengthXML);
        }
        /// <summary>
        /// Send data 
        /// </summary>
        /// <param name="package"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public void SendDataMessage(byte[] bytedata)
        {
            CreateBeginTransaction();
            Array.Copy(bytedata, FIRST_INDEX, mPackageDataMessage, FIRST_INDEX, TOTAL_DATA_SIZE);
        }

        /// <summary>
        /// Tạo Header đăng ký.
        /// </summary>
        /// <param name="strTransactionType">transactiontype</param>
        /// <param name="strTransactionID">transactionId</param>
        /// <param name="strUserID">UserID</param>
        /// <param name="intTotalbyte">Totalbyte</param>
        /// <param name="intDivisionMax">Divisionmax</param>
        /// <returns>byte array</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] CreateHeaderUserRegister(string strTransactionType
            , string strTransactionID, string strUserID, int intTotalbyte, int intDivisionMax)
        {
            //Send transactionType 
            this.SetTransactionType(strTransactionType);
            //Send transaction ID 
            this.SetTransactionID(strTransactionID);
            //Send UserID
            this.SetUser_ID(strUserID);
            //Send  Toltalbyte  file 
            this.SetTotalbyte(intTotalbyte);
            //Send Division Max
            this.SetDivisionMax(intDivisionMax);
            return mPackageHeaderNoMessage;
        }
        /// <summary>
        /// Đưa data vào package  
        /// </summary>
        /// <param name="FileCertificate">Cer</param>
        /// <returns>byte array </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] CreateDataPackageUserRegister(byte[] FileCertificate)
        {
            //Coppy DATA vào Package
            Array.Copy(FileCertificate, FIRST_INDEX, mPackageDataMessage, TOTAL_HEADER_SIZE, 1024);
            return mPackageDataMessage;
        }


        #region  Phan tich package tra ve cua server
        /// <summary>
        /// Đưa dữ liệu của Header và DATA đóng gói thành package để gửi lên Server.
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV        15/06/2008	     Thêm mới
        ///	</Modified>        
        public byte[] SetPackagetoServer()
        {
            //Tạo 1  package có độ dài bằng tổng header và data
            byte[] package = new byte[TOTAL_HEADER_SIZE + TOTAL_DATA_SIZE];
            //Coppy header vào package
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX, package, FIRST_INDEX, TOTAL_HEADER_SIZE);
            //Coppy DATA vào packager 
            Array.Copy(mPackageDataMessage, FIRST_INDEX, package, TOTAL_HEADER_SIZE, TOTAL_DATA_SIZE);
            //Trả về package đã được đóng gói.
            return package;
        }
        /// <summary>
        /// Phân tích Package do server tra về.
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>Trả về mã interger</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public int AnalyzeServeregisterPackage(byte[] package)
        {
            this.GetPackageSettoClient(package);
            ////Phân tích Package trả về from server             
            //Bắt đầu yêu cầu gủi thông tin header
            if (this.GetStringTransactionType() == KIND_TRANSACTION_BEGIN)
            {
                //Chưa nhận được tranID từ server gửi về
                if (this.GetTransactionID() == null)
                {
                    return 0;
                }
                //nhận được tranID
                else
                {
                    //Ser trả về DivisioMax , client tiep tuc gui fram 
                    if (this.GetStringApplicationReposeCode() == KIND_REPOSECODE1)
                    {
                        return 1;
                    }
                }
            }
            //Nhận được yêu cầu gửi thông tin + data 
            else if (this.GetStringTransactionType() == KIND_USER_TRANSACTION)
            {
                //Ser yêu cầu gửi thông tin đăng ký User
                if (this.GetStringApplicationReposeCode() == KIND_REPOSECODE2)
                {
                    return 2;
                }
                //Ser yêu cầu gửi Data 
                else if (this.GetStringApplicationReposeCode() == KIND_REPOSECODE3)
                {
                    return 3;
                }
            }
            //Kết thúc transaction 
            else if (this.GetStringTransactionType() == KIND_TRANSACTION_END)
            {
            }
            return 0;
        }


        #endregion  Phan tich package tra ve cua server
    }
}

