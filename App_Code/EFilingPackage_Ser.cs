using System;
using System.Collections.Generic;
using System.Text;

namespace DTQG_KV
{

    /// <summary>
    /// Lop co so cho cac lop ClientTransaction,ServerTransaction
    /// Xu ly thong tin cua mot package (Tao moi, phan tich )
    /// transaction_type | transaction_id | user_id | code | Total_bytes | Division Max | Division Size | Division Offset | Application Reponse Code| Data_message
    ///  3               |    36          |   10    |   7  |  16         |   16         |  16           | 16         4       |   xxx
    /// </summary>    
    ///	Name		       Date		     Comment 
    ///	TrườngTV	  05/06/2008	         Thêm mới
    ///	</Modified>
    public class EFilingPackage
    {
        public static int TOTAL_DATA_SIZE = 16 * 1024;
        protected byte[] mPackageHeaderNoMessage = new byte[TOTAL_HEADER_SIZE];
        protected byte[] mPackageDataMessage;
        // Các hàng số lưu thông tin kích thước các trường.
        public const int FIRST_INDEX = 0;
        public const int TRANSACTION_TYPE = 3;
        public const int TRANSACTION_ID = 36;
        public const int USER_ID = 10;
        public const int CODE = 17;
        public const int TOTAL_BYTES = 16;
        public const int DIVISION_MAX = 16;
        public const int DIVISION_SIZE = 16;
        public const int DIVISION_OFFSET = 16;
        public const int APPLICATION_REPONSE_CODE = 4;
        public const int DIVISION_HASH = 16;
        public const int DIVISION_RC = 4;
        public const int TOTAL_HEADER_SIZE = 182;
        // Transaction Type
        public const string KIND_TRANSACTION_BEGIN = "beg";
        public const string KIND_USER_TRANSACTION = "urg"; // User register
        public const string KIND_TRANSACTION_END = "end";
        public const string KIND_REPOSECODE1 = "S1";
        public const string KIND_REPOSECODE2 = "S2";
        public const string KIND_REPOSECODE3 = "S3";
        public const string KIND_REPOSECODE4 = "S4";


        #region member

        private byte[] byteTransactionType;

        public byte[] TransactionType
        {
            get { return byteTransactionType; }
            set { byteTransactionType = value; }
        }
        //transactionID
        private byte[] byteTransactionID;

        public byte[] TransactionID
        {
            get { return byteTransactionID; }
            set { byteTransactionID = value; }
        }
        //UserID
        private byte[] byteUserID;

        public byte[] UserID
        {
            get { return byteUserID; }
            set { byteUserID = value; }
        }
        //code 
        private byte[] byteCode;

        public byte[] Code
        {
            get { return byteCode; }
            set { byteCode = value; }
        }
        //Totalbytes 
        private byte[] byteTotalbyte;

        public byte[] TotalByte
        {
            get { return byteTotalbyte; }
            set { byteTotalbyte = value; }
        }
        //DivisionMax
        private byte[] byteDivisionMax;

        public byte[] DivisionMax
        {
            get { return byteDivisionMax; }
            set { byteDivisionMax = value; }
        }
        //DivisionSize
        private byte[] byteDivisionSize;

        public byte[] DivisionSize
        {
            get { return byteDivisionSize; }
            set { byteDivisionSize = value; }
        }
        //DivisionOffset
        private byte[] byteDivisionOffset;

        public byte[] DivisionOffset
        {
            get { return byteDivisionOffset; }
            set { byteDivisionOffset = value; }
        }
        //ApplicationReposecode
        private byte[] byteApplicationCode;

        public byte[] ApplicationCode
        {
            get { return byteApplicationCode; }
            set { byteApplicationCode = value; }
        }
        //Division Hash 
        private byte[] byteDivision_Hash;

        public byte[] Division_Hash
        {
            get { return byteDivision_Hash; }
            set { byteDivision_Hash = value; }
        }
        private byte[] byteDivision_DC;

        public byte[] Division_DC
        {
            get { return byteDivision_DC; }
            set { byteDivision_DC = value; }
        }

        //DataMessage
        private byte[] byteDataMessage;

        public byte[] DataMessage
        {
            get { return byteDataMessage; }
            set { byteDataMessage = value; }
        }
        #endregion  member



        public EFilingPackage()
        {
        }
        /// <summary>
        /// Get Package do Client gửi về gán vào mHeader và mDATA
        /// </summary>
        /// <param name="package"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public void SetPackage(byte[] package)
        {
            //Coppy dữ liệu từ package vào mHeader
            Array.Copy(package, FIRST_INDEX, mPackageHeaderNoMessage, FIRST_INDEX, TOTAL_HEADER_SIZE);
            //Coppy dữ liệu từ package vào mDATA 
            Array.Copy(package, TOTAL_HEADER_SIZE, mPackageDataMessage, FIRST_INDEX, TOTAL_DATA_SIZE);
        }
        #region Các hàm tạo các trường trong  header

        /// <summary>
        /// Set transactiontype 
        /// </summary>
        /// <param name="KindOfTransactionType">type</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public void SetTransactionType(string KindOfTransactionType)
        {
            //Đưa type vào mHeader.--gửi về Client 
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(KindOfTransactionType), FIRST_INDEX,
                                                    mPackageHeaderNoMessage, FIRST_INDEX, TRANSACTION_TYPE);
        }
        /// <summary>
        /// Lay ra gia tri cua mot mang byte cua TransactionType trong Header
        /// </summary>       
        /// <returns> mang ca byte</returns>
        /// <summary>
        /// Lay ra gia tri cua mot mang byte cua TransactionType trong Header
        /// </summary>       
        /// <returns> mang ca byte</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetBytesTransactionType()
        {
            //get transaction type            
            byte[] byteTransactionType = new byte[TRANSACTION_TYPE];
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX, byteTransactionType, FIRST_INDEX, TRANSACTION_TYPE);
            return byteTransactionType;
        }
        /// <summary>
        /// Lay ra gia tri cua mot chuoi cua TransactionType trong Header
        /// </summary>
        /// <returns>mot chuoi</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringTransactionType()
        {
            //get transaction type
            byte[] byteTransactionType = new byte[TRANSACTION_TYPE];
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX, byteTransactionType, FIRST_INDEX, TRANSACTION_TYPE);
            return ASCIIEncoding.ASCII.GetString(byteTransactionType);
        }

        /// <summary>
        /// Set transactionID
        /// </summary>
        /// <param name="KindOfTransactionID">ID</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        

        public void SetTransactionID(string strTransactionID)
        {
            //Client gửi transactionID 
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(strTransactionID), FIRST_INDEX,
                                                    mPackageHeaderNoMessage, TRANSACTION_TYPE, TRANSACTION_ID);
        }
        /// <summary>
        /// Get transactionID
        /// </summary>     
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetTransactionID()
        {
            //Tạo mảng có kịch thước bằng tranID
            byte[] byteTransactionID = new byte[TRANSACTION_ID];
            //Coppy tranID từ mHeader 
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE, byteTransactionID, FIRST_INDEX, TRANSACTION_ID);
            //if(EFilingCommunication.RemoveNullCharacter(byteTransactionID))
            //tranID đây,
            if (EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(byteTransactionID)) == string.Empty)
            {
                return null;
            }
            else
            {
                return byteTransactionID;
            }
        }
        /// <summary>
        /// Get string  transactionID
        /// </summary>     
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringTransactionID()
        {
            //Tạo mảng có kịch thước bằng tranID
            byte[] byteTransactionID = new byte[TRANSACTION_ID];
            //Coppy tranID từ mHeader 
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE, byteTransactionID, FIRST_INDEX, TRANSACTION_ID);
            //if(EFilingCommunication.RemoveNullCharacter(byteTransactionID))
            //tranID đây,
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(byteTransactionID));

        }
        /// <summary>
        /// Set User ID 
        /// </summary>
        /// <param name="strUserID">chuỗi UserID</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>     
        public void SetUser_ID(string strUserID)
        {
            //Client gủi thông tin đăng ký User vào mHeader để gửi đến Server
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(strUserID), FIRST_INDEX,
                                                mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID
                                                , USER_ID);
        }
        /// <summary>
        /// Get UserID 
        /// </summary>       
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetbyteUserID()
        {
            //Client lấy về UserID do Server gửi.
            byte[] byteUserID = new byte[USER_ID];
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX,
                                                byteUserID, TRANSACTION_TYPE + TRANSACTION_ID
                                                , USER_ID);
            //trả về mảng byte User
            return byteUserID;
        }
        /// <summary>
        /// Get String UserID 
        /// </summary>        
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringUserID()
        {
            //Client lấy về UserID convert to String.
            byte[] byteUserID = new byte[USER_ID];
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX,
                                                byteUserID, TRANSACTION_TYPE + TRANSACTION_ID
                                                , USER_ID);
            return ASCIIEncoding.ASCII.GetString(byteUserID);
        }
        /// <summary>
        /// Set code 
        /// </summary>
        /// <param name="Code"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV        20/06/2008	    Thêm mới
        ///	</Modified>        
        public void SetCode(string Code)
        {
            //Client gửi code lên Server
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(Code), FIRST_INDEX,
                                                mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID
                                                , CODE);
        }
        /// <summary>
        /// Get byte code
        /// </summary>
        /// <returns>byte array </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetbyteCode()
        {
            byte[] byteCode = new byte[CODE];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID, byteCode,
                                                FIRST_INDEX, CODE);
            return byteCode;
        }
        /// <summary>
        /// Get byte code
        /// </summary>
        /// <returns>byte array </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetFileName()
        {
            return ASCIIEncoding.ASCII.GetString(GetbyteCode());
        }
        /// <summary>
        /// Get String code 
        /// </summary>
        /// <returns>String </returns>
        public string GetStringCode()
        {
            //tạo mảng kích thước bằng Code
            byte[] byteCode = new byte[CODE];
            //Get về từ mHeader đửa vào mảng đã tạo.
            Array.Copy(mPackageHeaderNoMessage, FIRST_INDEX,
                                                byteCode, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID
                                                , CODE);
            return ASCIIEncoding.ASCII.GetString(byteCode);
        }
        /// <summary>
        /// Set total byte 
        /// </summary>
        /// <param name="intTotalbyte">Total byte </param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV        20/06/2008	     Thêm mới        
        ///	</Modified>        
        public void SetTotalbyte(long longTotalbyte)
        {
            //Tổng số byte kiểu long được Convert sạng Byte16-> Coppy vào mHeader.
            Array.Copy(EFilingCommunication.ConverttoLong16(longTotalbyte), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE
                                        , TOTAL_BYTES);
        }
        /// <summary>
        /// Get byte total byte
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetbyteTotalByte()
        {
            //Get về Totalbyte do Server gửi về.
            byte[] bytetotalByte = new byte[TOTAL_BYTES];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE,
                                   bytetotalByte, FIRST_INDEX
                                                , TOTAL_BYTES);
            return bytetotalByte;
        }

        /// <summary>
        /// Get String total byte
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringTotalByte()
        {
            //Get về tổng số byte do Ser vể gửi về dạng String.
            byte[] bytetotalByte = new byte[TOTAL_BYTES];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE,
                                   bytetotalByte, FIRST_INDEX
                                                , TOTAL_BYTES);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(bytetotalByte));
        }
        /// <summary>
        /// Set DivisionMax
        /// </summary>
        /// <param name="intDivisionMax">DivisiOnMax</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public void SetDivisionMax(long longDivisionMax)
        {
            //Tổng số byte kiểu long được Convert sạng Byte16-> Coppy vào mHeader.
            Array.Copy(EFilingCommunication.ConverttoLong16(longDivisionMax), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES
                                        , DIVISION_MAX);
        }
        /// <summary>
        /// Get byte DivisionMax
        /// </summary>
        /// <returns>byte array </returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>             
        public byte[] GetbyteDivisionMax()
        {
            //Get về byte DivisionMax 
            byte[] byteDivisionMax = new byte[DIVISION_MAX];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                       byteDivisionMax, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES
                                                , DIVISION_MAX);
            return byteDivisionMax;
        }
        /// <summary>
        /// Get String DivisionMax
        /// </summary>
        /// <returns>string</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public string GetStringDivisionMax()
        {
            //Lấy về DivisionMax.
            byte[] byteDivisionMax = new byte[DIVISION_MAX];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                                       byteDivisionMax, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES
                                      , DIVISION_MAX);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(byteDivisionMax));
        }
        /// <summary>
        /// Set Division Sive 
        /// </summary>
        /// <param name="longDivisionSize">Division</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public void SetDivisionSize(long longDivisionSize)
        {
            //Set DivisionMax 
            Array.Copy(EFilingCommunication.ConverttoLong16(longDivisionSize), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX
                                        , DIVISION_SIZE);
        }
        /// <summary>
        /// Get byte DivisionSize
        /// </summary>
        /// <returns>DivisionSize</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public byte[] GetbyteDivisionSize()
        {
            byte[] byteDivisionSize = new byte[DIVISION_SIZE];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                       byteDivisionSize, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX
                                                , DIVISION_SIZE);
            return byteDivisionSize;
        }
        /// <summary>
        /// Get String  DivisionSize
        /// </summary>
        /// <returns></returns>
        public string GetStringDivisionSize()
        {
            byte[] byteDivisionSize = new byte[DIVISION_SIZE];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX,
                       byteDivisionSize, FIRST_INDEX, DIVISION_SIZE);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(byteDivisionSize));
        }
        /// <summary>
        /// Set Division Offset 
        /// </summary>
        /// <param name="intDivisionSize"></param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public void SetDivisionOffset(long longDivisionOffset)
        {
            Array.Copy(EFilingCommunication.ConverttoLong16(longDivisionOffset), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE
                                        , DIVISION_OFFSET);
        }
        /// <summary>
        /// Get byte DivisionOffset
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public byte[] GetbyteDivisionOffset()
        {
            byte[] byteDivisionOffset = new byte[DIVISION_OFFSET];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                       byteDivisionOffset, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE
                                                , DIVISION_OFFSET);
            return byteDivisionOffset;
        }

        /// <summary>
        /// Get String DivisionOffset
        /// </summary>
        /// <returns>String</returns>
        public string GetStringDivisionOffset()
        {
            byte[] byteDivisionOffset = new byte[DIVISION_OFFSET];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE,
                       byteDivisionOffset, FIRST_INDEX
                                                , DIVISION_OFFSET);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(byteDivisionOffset));
        }
        /// <summary>
        /// Set applicationReposecode
        /// </summary>
        /// <param name="strApplicationReposeCode">application reposecode</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public void SetApplicationReposeCode(string strApplicationReposeCode)
        {
            //Client gửi Code lên Server
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(strApplicationReposeCode.ToString()), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE + DIVISION_OFFSET
                                        , APPLICATION_REPONSE_CODE);
        }
        /// <summary>
        /// Get byte Application Repose code
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public byte[] GetbyteApplicationReposeCode()
        {
            byte[] byteApplicationReposeCode = new byte[APPLICATION_REPONSE_CODE];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                       byteApplicationReposeCode, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE + DIVISION_OFFSET
                                                , APPLICATION_REPONSE_CODE);
            return byteApplicationReposeCode;
        }

        /// <summary>
        /// Get int Application Repose code
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>              
        public string GetStringApplicationReposeCode()
        {
            byte[] byteApplicationReposeCode = new byte[APPLICATION_REPONSE_CODE];
            Array.Copy(mPackageDataMessage, FIRST_INDEX,
                       byteApplicationReposeCode, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE + DIVISION_OFFSET
                                                , APPLICATION_REPONSE_CODE);
            return ASCIIEncoding.ASCII.GetString(byteApplicationReposeCode);
        }
        /// <summary>
        /// Set DivisionHash
        /// </summary>
        /// <param name="intTotalbyte">Total byte </param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV        20/06/2008	     Thêm mới        
        ///	</Modified>        
        public void SetDivisionHash(byte[] byteDivisionHash)
        {
            //Tổng số byte kiểu long được Convert sạng Byte16-> Coppy vào mHeader.
            Array.Copy(byteDivision_Hash, FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + DIVISION_HASH
                                        , DIVISION_HASH);
        }
        /// <summary>
        /// Get DivisionHash
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetDivisionHash()
        {
            //Get về Totalbyte do Server gửi về.
            byte[] byteDivisionHash = new byte[DIVISION_HASH];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + TOTAL_BYTES + DIVISION_MAX + DIVISION_SIZE + DIVISION_OFFSET + APPLICATION_REPONSE_CODE,
                                   byteDivisionHash, FIRST_INDEX
                                                , DIVISION_HASH);
            return byteDivisionHash;
        }

        /// <summary>
        /// Get DivisionHash
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringDivisionHash()
        {
            //Get về tổng số byte do Ser vể gửi về dạng String.
            byte[] bytetotalByte = new byte[TOTAL_BYTES];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + DIVISION_HASH,
                                   bytetotalByte, FIRST_INDEX
                                                , DIVISION_HASH);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(bytetotalByte));
        }


        /// <summary>
        /// Set DivisionRC
        /// </summary>
        /// <param name="intTotalbyte">Total byte </param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV        20/06/2008	     Thêm mới        
        ///	</Modified>        
        public void SetDivisionRC(string strDivisionRC)
        {
            //Tổng số byte kiểu long được Convert sạng Byte16-> Coppy vào mHeader.
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(strDivisionRC), FIRST_INDEX,
                                        mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + DIVISION_HASH + DIVISION_RC
                                        , DIVISION_RC);
        }
        /// <summary>
        /// Get DivisionRC
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetDivisionRC()
        {
            //Get về Totalbyte do Server gửi về.
            byte[] byteDivisionRC = new byte[DIVISION_RC];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + DIVISION_HASH + DIVISION_RC,
                                   byteDivisionRC, FIRST_INDEX
                                                , DIVISION_RC);
            return byteDivisionRC;
        }

        /// <summary>
        /// Get DivisionRC
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      20/06/2008	    Thêm mới
        ///	</Modified>        
        public string GetStringDivisionRC()
        {
            //Get về tổng số byte do Ser vể gửi về dạng String.
            byte[] bytetotalByte = new byte[TOTAL_BYTES];
            Array.Copy(mPackageHeaderNoMessage, TRANSACTION_TYPE + TRANSACTION_ID + USER_ID + CODE + DIVISION_HASH + DIVISION_RC,
                                   bytetotalByte, FIRST_INDEX
                                                , DIVISION_RC);
            return EFilingCommunication.RemoveNullCharacter(ASCIIEncoding.ASCII.GetString(bytetotalByte));
        }

        #endregion cac ham tao cac fields trong header

        #region data
        /// <summary>
        /// Get về data
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        
        public byte[] GetDataMesage()
        {
            //get transaction type
            return mPackageDataMessage;
        }
        #endregion data

        #region Tao Header khong co du lieu (data message)


        public void CreatePackegaHeader(string strTransactionType
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
        }

        #endregion Tao Header khong co du lieu
        /// <summary>
        /// Tạo package có DATA
        /// </summary>
        /// <param name="strTransactionType">type</param>
        /// <param name="strTransactionID">ID</param>
        /// <param name="strUserID">User</param>
        /// <param name="intTotalbyte">Total</param>
        /// <param name="intDivisionMax">Dimax</param>
        /// <param name="intDivisionSize">DiSize</param>
        /// <param name="intDivisionOffset">Offset</param>
        /// <param name="byteDataMessage">Mes</param>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV      15/06/2008	    Thêm mới
        ///	</Modified>        

        public void PackageDataMessage(string strTransactionType
                    , string strTransactionID, string strUserID, int intTotalbyte, int intDivisionMax
                       , int intDivisionSize, int intDivisionOffset, byte byteDataMessage)
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
            //Send Division Size 
            this.SetDivisionSize(intDivisionSize);
            //Send Division Offset 
            this.SetDivisionOffset(intDivisionOffset);
            //Send DataMessage

        }

    }
}