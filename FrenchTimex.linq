<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Caching.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Utilities.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Tasks.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web</Namespace>
</Query>

void Main()
{
	var queries = File.ReadAllLines(@"C:\Users\t-abradw\Desktop\test2.tsv"); // path of the testing data set
	
	Stopwatch sw = new Stopwatch();
	var count = 0;
	
	using (var streamWriter = new StreamWriter(@"C:\Users\t-abradw\Desktop\f-score75.txt", true, Encoding.UTF8))
    foreach (var q in queries)
    {
        WebClient wc = new WebClient();
		wc.Headers.Add("Authorization", "Basic RGljZURldjoxNUFmNDI5QTk3ODE0OTBlQjgzMjZkQTIyZTRGZjcyNw==");
		wc.Encoding = Encoding.UTF8;
		
		Again:
        sw.Start();
		try
		{
			// the response, you need to change the app id and the app key
        	var response = wc.DownloadString(@"https://dialogice2.cloudapp.net:8081/api/v1/application?id=4c733647-f01a-42a2-8bc5-d6e948201f75&subscription-key=7dce7a433f4142ac93c3e7e08b650afa&q=" + HttpUtility.UrlEncode(q));
			Console.WriteLine(string.Format("{0} : {1}", count , sw.ElapsedMilliseconds));
        	sw.Reset();
			streamWriter.WriteLine(response + ",");
		}
		catch(Exception e)
		{
			Console.WriteLine(string.Format("{0} : Error {1}", count, e ));
			//streamWriter.WriteLine("Error: " + q);
			goto Again;
		}
		Thread.Sleep(300);
		count++;
    }

}

// Define other methods and classes here