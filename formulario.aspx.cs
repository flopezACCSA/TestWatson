using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;

public partial class formulario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string url = "https://gateway.watsonplatform.net/natural-language-classifier/api/v1/classifiers/340008x87-nlc-1691/classify?text=";
        url = url + txtPregunta.Text;
        WebRequest request = WebRequest.Create(url);
        string credenciales = "f4bb6476-01b5-4c12-9bd1-59dd88697b21" + ":" + "PhSyKoVZUgrZ";
        request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credenciales));
        WebResponse response = request.GetResponse();
        Stream streamResponse = response.GetResponseStream();
        StreamReader reader = new StreamReader(streamResponse, Encoding.UTF8);
        string json = reader.ReadToEnd();
        txtResult.Text = json;
    }
}