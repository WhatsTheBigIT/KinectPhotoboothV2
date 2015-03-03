using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KinectPhotobooth.Services
{
    public class EmailService
    {

        private string _SMTPEmailServer = "...";   
        private string _SMTPEmailUserID = "...";   //Must be your usedID, and it must be authorized to use Kinectpb@microsoft.com
        private string _SMTPEmailPassword = "...";         //Must be your password.


        //This is the body of the email being sent.  In the future, this should be a txt file which could be modified.  For now, it's a string :-(
        #region Email Body
        private static readonly string body = @"
<p style='color:midnightblue;font-size:24px'>Greetings from the Microsoft Kinect Photo Booth!</p>
<br />
We hope you had a fantastic time at Hack Illinois. The attached image is your photo taken at the Microsoft Kinect for Windows Photo Booth. <br /> <br />
You, too, can build amazing apps with the Kinect for Windows.  If you already have a Kinect with your XBox One, you can use it on your PC with the Kinect adapter.  Both are avaibale 
from the Microsoft Store. </br>
http://www.microsoft.com/en-us/kinectforwindows/purchase/default.aspx#tab=2

<br />
<br />
Here are some resources which you may find usefull:
<br />
    <ul>
        <li><a href='http://www.microsoft.com/about/corporatecitizenship/en-us/youthspark/youthsparkhub/'>YouthSpark Hub:</a> Central resource for all things YouthSpark</li>        
        <li><a href='http://www.microsoft.com/click/services/Redirect2.ashx?CR_CC=200256686'>BizSpark:</a> Get the tools and resources to help build your business!</li>
        <li><a href='http://www.microsoft.com/click/services/Redirect2.ashx?CR_CC=200256687'>DreamSpark:</a> Program from Microsoft for students.</li>
        <li><a href='http://www.microsoft.com/click/services/Redirect2.ashx?CR_CC=200256683'>Microsoft Virtual Academy:</a> Free training at your fingertips.</li>
        <li><a href='http://appstudio.windows.com/'>Microsoft App Studio:</a> Build your app quickly with this web-based tool.</li>
        <li><a href='http://www.microsoft.com/en-us/kinectforwindows/'>Kinect</a> Resources for this amazing device.</li>
    </ul>
<br />
<br />

For a list of additional resources from Microsoft, visit: http://aka.ms/StudentLinks <br />
<br/><br/>
Follow me on Twitter <a href='https://twitter.com/KinectPhotos'>@KinectPhotos</a>

<br />
Kindest Regards,
<br />
Microsoft Kinect for Windows Photo Booth
";
        #endregion
   
        public Task<bool> SendMail(string sendToEmail, BitmapEncoder bitmap)
        {

            bool returnValue = true;

            MailMessage msg = new MailMessage("kinectpb@microsoft.com", sendToEmail, "Your Microsoft Kinect Photo Booth Picture", body);
            
                msg.BodyEncoding = System.Text.Encoding.Unicode;
                msg.IsBodyHtml = true;    
            ContentType ct = new ContentType();
            ct.MediaType = MediaTypeNames.Image.Jpeg;
            ct.Name = "MSKinectPhotobooth.png";

            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream);
            stream.Position = 0;
            Attachment data = new Attachment(stream, "MSPhotobooth.png");
            msg.Attachments.Add(data);
            Task<bool> t = new Task<bool>(() =>
            {
                try
                {


                    //SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
                    SmtpClient smtpClient = new SmtpClient(_SMTPEmailServer)
                    {
                        UseDefaultCredentials = false,
                        
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(_SMTPEmailUserID, _SMTPEmailPassword),
                    };

                    smtpClient.Send(msg);
                    smtpClient.Dispose();
                }
                catch (Exception ex)
                {
                    
                     returnValue = false;
                }
                finally
                {
                    msg.Dispose();
                }

                return returnValue;
            });

            t.Start();




            return t;



        }
    }
}
