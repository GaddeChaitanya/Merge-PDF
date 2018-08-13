using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string FilePath1 = Server.MapPath("2fa_backup_code_USCIS_myAccount.pdf");
        string FilePath2 = Server.MapPath("Action-Verbs-for-Resumes.pdf");

        WebClient User = new WebClient();

        Byte[] FileBuffer1 = User.DownloadData(FilePath1);
        Byte[] FileBuffer2 = User.DownloadData(FilePath2);

        //Byte[] newBuffer = User.DownloadData(FilePath);
        MemoryStream ms = new MemoryStream(FileBuffer1, 0,FileBuffer1.Length,true,true);

        MemoryStream sm = new MemoryStream(FileBuffer1, 0, FileBuffer1.Length, true, true);

       // sm.CopyTo(ms);
        MemoryStream m = new MemoryStream();
       // sm.CopyTo(m);
        m.Write(ms.GetBuffer(), 0, (int)ms.Length);
        m.Write(sm.GetBuffer(), 0, (int)sm.Length);
        //// sm.Write(ms.GetBuffer(), 0, (int)ms.Length);
        if (FileBuffer1 != null)

        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            //  Response.BinaryWrite(FileBuffer);

            // Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            //Response.OutputStream.Flush();
            //Response.OutputStream.Close();

            // m.Close();
            Response.AddHeader("content-length", m.ToArray().Length.ToString());
            Response.BinaryWrite(m.ToArray());
            // Response.WriteFile(FilePath);
            Response.End();
            //Response.ContentType = "application/pdf";

            ////Response.AddHeader("content-length", FileBuffer.Length.ToString());

            ////Response.BinaryWrite(FileBuffer);

            //Response.WriteFile(FilePath);

        }
    }
}