using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ParCit
{
    public static class PushCitations
    {
        public static void UploadFile(string fileName)
        {
            var request = FtpWebRequest.Create("ftp://ftp.bibliopedia.us/"+fileName);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential("jstor", "jstor");

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(fileName);

            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();
        }
    }
}
