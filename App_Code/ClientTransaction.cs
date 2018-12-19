using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DTQG_KV;
using System.Net;
using System.Threading;


namespace DTQG_KV
{

    /// <summary>
    /// Lop ClientTransaction thuc hien cac giao dich voi server
    /// </summary>
    /// <Modified>
    ///	Name		       Date		     Comment 
    ///	TrườngTV	  05/06/2008	         Thêm mới
    ///	</Modified>
    public partial class ClientTransaction : BackgroundWorker
    {
        public ClientRegistration mUserRegistration;


        protected wVDC.VDC ws;
        protected byte[] _package;
        protected byte[] _serverpackage;
        protected Thread HashThread;
        protected string strTransactionID = "";
        public int ChunkSize = 16 * 1024;
        private CookieContainer cookies = new CookieContainer();

        public ClientTransaction()
        {
            base.WorkerReportsProgress = true;
            base.WorkerSupportsCancellation = true;
        }

        public wVDC.VDC WebService
        {
            get
            {
                if (this.ws == null)
                {
                    ws = new wVDC.VDC();
                    ws.PreAuthenticate = true;
                    ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    ws.CookieContainer = cookies;
                    ws.Timeout = 360000;//6
                }
                return ws;

            }
        }

        public string TransactionID
        {
            get
            {
                return strTransactionID;
            }
            set
            {
                strTransactionID = value;
            }
        }

        public byte[] Package
        {
            get
            {
                return _package;
            }
            set
            {
                _package = value;
            }
        }

        public byte[] ServerPackage
        {
            get
            {
                return _serverpackage;
            }
            set
            {
                _serverpackage = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.HashThread != null && this.HashThread.IsAlive)
                this.HashThread.Abort();

            base.Dispose(disposing);
        }


        protected override void OnDoWork(DoWorkEventArgs e)
        {
            base.OnDoWork(e);
            try
            {
                this.ServerPackage = this.WebService.AppendUpload(Package);
              


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}
