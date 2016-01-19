using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tx_mailmerge.Models;
using TXTextControl.DocumentServer;

namespace tx_mailmerge.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Merge(DocumentViewModel model)
        {
            // convert the template to a byte array
            byte[] document = Convert.FromBase64String(model.BinaryDocument);

            // load the the data source
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("/App_Data/data/sample_db.xml"), XmlReadMode.Auto);

            byte[] sDocument;

            // create a new ServerTextControl and MailMerge component
            using (TXTextControl.ServerTextControl tx =
                new TXTextControl.ServerTextControl())
            {
                MailMerge mm = new MailMerge();
                mm.TextComponent = tx;

                // load the template and merge
                mm.LoadTemplateFromMemory(document, FileFormat.InternalUnicodeFormat);
                mm.Merge(ds.Tables[0], true);

                // save the document
                mm.SaveDocumentToMemory(out sDocument, TXTextControl.BinaryStreamType.InternalUnicodeFormat, null);
            }

            // encode and return the merged document
            return Convert.ToBase64String(sDocument);
        }
    }
}