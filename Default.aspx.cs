using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Configuration;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        divMessage.InnerHtml = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if ((fileUpload1.PostedFile != null) && (fileUpload1.PostedFile.ContentLength > 0))
        {
            txtJson.Visible = false;

            if (!System.Web.MimeMapping.GetMimeMapping(fileUpload1.PostedFile.FileName).StartsWith("image/"))
            {
                divMessage.Style.Add("background-color", "#f44336");
                divMessage.InnerHtml = "<span class=\"closebtn\" onclick=\"this.parentElement.style.display='none';\">&times;</span><br />Please select a valid image file.";
                txtJson.Visible = false;
                divAd.InnerHtml = string.Empty;
                return;
            }

            string fileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
            if (!Directory.Exists(Server.MapPath("Faces")))
                Directory.CreateDirectory(Server.MapPath("Faces"));

            string SaveLocation = Server.MapPath("Faces") + "\\" + fileName;
            try
            {
                fileUpload1.PostedFile.SaveAs(SaveLocation);
                string faceJsonContent = UploadAndDownloadFile(SaveLocation);

                try
                {
                    if (File.Exists(SaveLocation))
                        File.Delete(SaveLocation);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                divMessage.Style.Add("background-color", "#f44336");
                divMessage.InnerHtml = "<span class=\"closebtn\" onclick=\"this.parentElement.style.display='none';\">&times;</span><br />Exception: " + ex.Message;
                divAd.InnerHtml = string.Empty;
                txtJson.Visible = false;
            }
        }
        else
        {
            divMessage.Style.Add("background-color", "#2196f3");
            divMessage.InnerHtml = "<span class=\"closebtn\" onclick=\"this.parentElement.style.display='none';\">&times;</span><br />Please select a file to upload.";
            divAd.InnerHtml = string.Empty;
            txtJson.Visible = false;
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "title", "StopProgress();", true);
    }

    private string UploadAndDownloadFile(string filePath)
    {
        string faceJsonContent = string.Empty;
        string imgBlobName = string.Format("{0}-{1}.jpg", Path.GetFileNameWithoutExtension(filePath), DateTime.Now.Millisecond);
        string jsonBlobName = string.Format("{0}-{1}.json", Path.GetFileNameWithoutExtension(filePath), DateTime.Now.Millisecond);
        
        CloudStorageAccount storageAcc = null;
        CloudBlobClient cloudBlobClient = null;
        CloudBlobContainer blobContainer = null;
        CloudBlockBlob blockBlob = null;

        try
        {
            CloudStorageAccount storageAccount1 = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            storageAcc = storageAccount1;
            cloudBlobClient = storageAcc.CreateCloudBlobClient();
            blobContainer = cloudBlobClient.GetContainerReference(ConfigurationManager.AppSettings["ImageContainerName"]);
            blobContainer.CreateIfNotExists();

            blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            blockBlob = blobContainer.GetBlockBlobReference(imgBlobName);
            using (Stream file = File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(file);
            }

            divMessage.Style.Add("background-color", "#4caf50");
            divMessage.InnerHtml = "<span class=\"closebtn\" onclick=\"this.parentElement.style.display='none';\">&times;</span><br />File (" + Path.GetFileName(fileUpload1.PostedFile.FileName) + ") uploaded successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }

        try
        {
            blobContainer = cloudBlobClient.GetContainerReference(ConfigurationManager.AppSettings["JsonContainerName"]);
            blockBlob = blobContainer.GetBlockBlobReference(jsonBlobName);

            int index = 1;
            do
            {
                bool isExists = blockBlob.Exists();

                if (isExists)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        blockBlob.DownloadToStream(memoryStream);
                        string json = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                        txtareaJson.InnerText = JToken.Parse(json).ToString();
                        txtJson.Visible = true;
                        faceJsonContent = txtareaJson.InnerText;
                        SetAd(faceJsonContent);
                    }
                    break;
                }
                else
                {
                    index++;
                    System.Threading.Thread.Sleep(3000);
                }
            } while (index <= 30);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return faceJsonContent;
    }

    private void SetAd(string faceJsonContent)
    {
        divAd.InnerHtml = string.Empty;
        if (string.IsNullOrEmpty(faceJsonContent))
            return;

        try
        {
            List<Face> faceList = Face.Deserialize(faceJsonContent);
            bool isBrightFace = faceList.Exists(item => item.faceAttributes.blur.value >= 0.5);
            bool isLighterFace = faceList.Exists(item => item.faceAttributes.blur.value < 0.5);

            if (isBrightFace)
                divAd.InnerHtml = "<img src=\"Ads/brigher-ad.png\" width=\"225\" />";
            else divAd.InnerHtml = "<img src=\"Ads/lighter-ad.png\" width=\"225\" />";
        }
        catch
        {
        }
    }
}