using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;


namespace DTQG_KV
{
    /// <summary>
    /// Summary description for ServerUserRegister
    /// </summary>    
    /// <Modified>
    ///	Name		       Date		     Comment 
    ///	TrườngTV	  05/06/2008	         Thêm mới
    ///	</Modified>
    public class KV_Server  : EFilingPackage
    {
        public KV_Server(byte[] package)
        {
            TOTAL_DATA_SIZE = package.Length - TOTAL_HEADER_SIZE;
            mPackageDataMessage = new byte[TOTAL_DATA_SIZE];
        }
        /// <summary>
        /// Get header Mesage
        /// </summary>
        /// <param name="headermessage"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  05/06/2008	         Thêm mới
        ///	</Modified>
        public void GetHeaderMessage(byte[] headermessage)
        {
            mPackageHeaderNoMessage = headermessage;
        }
        /// <summary>
        /// Create Begin transaction 
        /// </summary>
        /// <returns>byte arry </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  05/06/2008	         Thêm mới
        ///	</Modified>
        public byte[] CreateBeginTransaction()
        {
            // Khoi tao mot package begin transaction
            // Tao transaction_type
            this.SetTransactionType(KIND_TRANSACTION_BEGIN);
            // tao transaction_id
            this.SetTransactionID(null);
            //tra ve package
            return this.mPackageHeaderNoMessage;
        }
        /// <summary>
        /// Create header Usr Register.
        /// </summary>
        /// <param name="strTransactionType">type</param>
        /// <param name="strTransactionID">ID</param>
        /// <param name="strUserID">UserID</param>
        /// <param name="intTotalbyte">totalbyte</param>
        /// <param name="intDivisionMax"></param>
        /// <returns></returns>
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
            //return package
            return mPackageHeaderNoMessage;
        }
        /// <summary>
        /// Create package
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <param name="FileCertificate"></param>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  05/06/2008	         Thêm mới
        ///	</Modified>
        public byte[] CreateDataPackageUserRegister(byte[] UserInfo, byte[] FileCertificate)
        {
            //đẩy thông tin User vào package
            Array.Copy(UserInfo, FIRST_INDEX, mPackageDataMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
            //đưa thông tin về 
            Array.Copy(UserInfo, FIRST_INDEX, mPackageDataMessage, TOTAL_HEADER_SIZE, 1024);
            return mPackageDataMessage;
        }
        #region  Phan tich package tra ve cua server
        /// <summary>
        /// Get package do Client gửi về.
        /// </summary>
        /// <param name="package"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  05/06/2008	         Thêm mới
        ///	</Modified>
        public void GetPackageSettoServer(byte[] package)
        {
            try
            {
                if (package.Length <= TOTAL_HEADER_SIZE)
                {
                    Array.Copy(package, FIRST_INDEX, mPackageHeaderNoMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
                }
                else
                {
                    Array.Copy(package, FIRST_INDEX, mPackageHeaderNoMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
                    Array.Copy(package, TOTAL_HEADER_SIZE, mPackageDataMessage, FIRST_INDEX, TOTAL_DATA_SIZE);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Set package từ Server gửi client 
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  05/06/2008	         Thêm mới
        ///	</Modified>
        public byte[] SetPackagetoClient()
        {
            byte[] package = new byte[TOTAL_HEADER_SIZE + TOTAL_DATA_SIZE];
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX, package, FIRST_INDEX, TOTAL_HEADER_SIZE);
            Array.Copy(mPackageDataMessage, FIRST_INDEX, package, TOTAL_HEADER_SIZE, TOTAL_DATA_SIZE);
            return package;
        }
        /// <summary>
        /// Phân tích package do Client gửi.
        /// </summary>
        /// <param name="package">package</param>
        /// <returns>byte array</returns>
        public byte[] AnalyzeClientRegisterPackage(byte[] package)
        {
            this.GetPackageSettoServer(package);
            ////Phân tích Package trả về         
            ////1.  CLIENT gửi transactionType neu là beg ,code="EFILING", TotalByte (3 thong tin )
            if (this.GetStringTransactionType() == KIND_TRANSACTION_BEGIN)
            {
                if (this.GetTransactionID() == null)
                {
                    this.SetTransactionID(Guid.NewGuid().ToString());
                }
                return this.SetPackagetoClient();
            }
            //Kiểm tra type là URG 
            if (this.GetStringTransactionType() == KIND_USER_TRANSACTION)
            {
                this.SetApplicationReposeCode(KIND_REPOSECODE1);
                return this.SetPackagetoClient();
            }
            //Kiểm tra type
            if ((this.GetStringTransactionType() == KIND_USER_TRANSACTION))
            {
                this.SetApplicationReposeCode(KIND_REPOSECODE2);
                return this.SetPackagetoClient();
            }
            if (this.GetDataMesage() != null)
            {
                //Tạo package gửi về Client.
                return SetPackagetoClient();

            }

            return null;
        }

        #endregion  Phan tich package gủi đến của Client



    }
}
