using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace DTQG_KV
{

    /// <summary>
    /// EFiling Communication
    /// </summary>
    /// <Modified>
    ///	Name		       Date		     Comment 
    ///	TrườngTV	  05/06/2008	         Thêm mới
    ///	</Modified>
    public class EFilingCommunication
    {
        private string hostname;
        private string ipAddress;
        private string port;


        public EFilingCommunication()
        {


        }
        #region "Proproties"
        public string HostName
        {
            get
            {
                return hostname;
            }
            set
            {
                hostname = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        public string Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        #endregion
        /// <summary>
        /// Check EReceiving Application server is running
        /// </summary>
        /// <returns> OK or FAIL </returns>

        public string PingEReciveingServer()
        {
            return "";
        }

        /// <summary>
        /// Remove char null
        /// </summary>
        /// <param name="strInput">string /param>
        /// <returns>string</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  21/06/2008         Thêm mới
        ///	</Modified>

        public static string RemoveNullCharacter(string strInput)
        {

            return strInput.Replace("\0", "");

        }
        /// <summary>
        /// Convet long to char[] 16
        /// </summary>
        /// <param name="longData">long </param>
        /// <returns>string</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  21/06/2008         Thêm mới
        ///	</Modified>
        public static byte[] ConverttoLong16(long longData)
        {
            int intlengthdatatem = 0;
            //Mảng byte16 
            byte[] byteLong16 = new byte[16];
            //Nếu kích thước longdata<16 thì thêm "0" phía trước
            if (longData.ToString().Length <= 16)
            {
                int intLengthLong16 = longData.ToString().Length;
                //Duyệt 16 phần tử của mảng byte16
                for (int intLength = 0; intLength < 16; intLength++)
                {
                    //Các phần tử đầu, cần thêm "0"
                    if (intLength < (16 - intLengthLong16))
                    {
                        byteLong16[intLength] = byte.Parse("0");
                    }
                    else
                    {
                        int intlengthdata = longData.ToString().Length;
                        //Các phần tử còn lại đưa vào lần lượt
                        if (intlengthdatatem <= intlengthdata)
                        {
                            byteLong16[intLength] =
                            (byte)longData.ToString()[intlengthdatatem];
                            //0000000000000001234
                            intlengthdatatem++;
                        }
                    }
                }
            }
            return byteLong16;
        }



        /// <summary>
        /// Convet long to char[] 16
        /// </summary>
        /// <param name="longData">long </param>
        /// <returns>string</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  21/06/2008         Thêm mới
        ///	</Modified>
        public static byte[] ConverttoString36(string strData)
        {
            int intlengthdatatem = 0;
            //Mảng byte16 
            byte[] byteLong36 = new byte[36];
            //Nếu kích thước strData<36 thì thêm "0" phía trước
            if (strData.ToString().Length <= 36)
            {
                int intLengthLong36 = strData.ToString().Length;
                //Duyệt 36 phần tử của mảng byte36
                for (int intLength = 0; intLength < 36; intLength++)
                {
                    //Các phần tử đầu, cần thêm "0"
                    if (intLength < (36 - intLengthLong36))
                    {
                        byteLong36[intLength] = byte.Parse("0");
                    }
                    else
                    {
                        int intlengthdata = strData.ToString().Length;
                        //Các phần tử còn lại đưa vào lần lượt
                        if (intlengthdatatem <= intlengthdata)
                        {
                            byteLong36[intLength] =
                            (byte)strData.ToString()[intlengthdatatem];
                            //0000000000000001234
                            intlengthdatatem++;
                        }
                    }
                }
            }
            return byteLong36;
        }

        /// <summary>
        /// Convet long to char[] 16
        /// </summary>
        /// <param name="longData">long </param>
        /// <returns>string</returns>
        /// <Modified>
        ///	Name		       Date		     Comment 
        ///	TrườngTV	  21/06/2008         Thêm mới
        ///	</Modified>
        public static byte[] ConverttoBufferData(byte[] buffer)
        {
            int intlengthdatatem = 0;
            //Mảng byte16 
            byte[] byteData = new byte[EFilingPackage.TOTAL_DATA_SIZE];

            //Nếu kích thước strData<TOTAL_DATA_SIZE thì thêm "0" phía trước
            if (buffer.Length < EFilingPackage.TOTAL_DATA_SIZE)
            {
                int intLengthLong36 = buffer.Length;
                //Duyệt 36 phần tử của mảng byte36
                for (int intLength = 0; intLength < EFilingPackage.TOTAL_DATA_SIZE; intLength++)
                {
                    //Các phần tử đầu, cần thêm "0"
                    if (intLength < (EFilingPackage.TOTAL_DATA_SIZE - intLengthLong36))
                    {
                        byteData[intLength] = byte.Parse("0");
                    }
                    else
                    {
                        int intlengthdata = buffer.Length;
                        //Các phần tử còn lại đưa vào lần lượt
                        if (intlengthdatatem <= intlengthdata)
                        {
                            byteData[intLength] = (byte)buffer[intlengthdatatem];
                            //0000000000000001234
                            intlengthdatatem++;
                        }
                    }
                }
                return byteData;
            }
            return buffer;
        }





    }
}
