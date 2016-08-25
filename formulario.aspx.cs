using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Media;


public partial class formulario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyRequest.RootObject objInitialRequest = new MyRequest.RootObject();
            objInitialRequest.context.system = new MyRequest.System();
            Session["conversation"] = objInitialRequest;
            Session["conversation_Id"] = string.Empty;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        #region Conversation 2.0
        
        var url = "https://watson-api-explorer.mybluemix.net/conversation/api/v1/workspaces/63ad9983-9e5c-446c-ab3a-05512e95ddec/message?version=2016-07-11";
        string credentials = "ec56a3dc-6b4f-4e54-9513-e28a3aea9d04" + ":" + "3CXhyq4yULYA";

        WatsonResponse.RootObject objResponse = new WatsonResponse.RootObject();
        var jsonData = string.Empty;

        if (string.IsNullOrEmpty(Session["conversation_Id"].ToString()))
        {
            MyRequest.RootObject objFirstRequest = new MyRequest.RootObject();
            objFirstRequest.input.text = txtPregunta.Text;
            jsonData = JsonConvert.SerializeObject(objFirstRequest.input);
        }
        else
        {
            //Acá va toda la lógica de modificación de contexto
            MyRequest.RootObject objMyRequest = new MyRequest.RootObject();
            objMyRequest.context.conversation_id = ((WatsonResponse.RootObject)Session["conversation"]).context.conversation_id;
            objMyRequest.workspace_id = "63ad9983-9e5c-446c-ab3a-05512e95ddec";
            objMyRequest.input.text = txtPregunta.Text;
            objMyRequest.context.monto = 1000000;
            objMyRequest.context.salario = 10000;
            objMyRequest.context.antiguedad = 10;
            objMyRequest.context.system = new MyRequest.System();
            objMyRequest.context.system.dialog_request_counter = ((WatsonResponse.RootObject)Session["conversation"]).context.system.dialog_request_counter;
            objMyRequest.context.system.dialog_stack = ((WatsonResponse.RootObject)Session["conversation"]).context.system.dialog_stack;
            objMyRequest.context.system.dialog_turn_counter = ((WatsonResponse.RootObject)Session["conversation"]).context.system.dialog_turn_counter;
            jsonData = JsonConvert.SerializeObject(objMyRequest);
        }

        using (var client = new WebClient())
        {
            client.Headers.Add("content-type", "application/json");
            client.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

            var response = client.UploadString(url, jsonData);
            objResponse = JsonConvert.DeserializeObject<WatsonResponse.RootObject>(response.ToString());

            Session["conversation"] = objResponse;
            Session["conversation_Id"] = objResponse.context.conversation_id;

            //txtResult.Text = objResponse.output.text[objResponse.output.text.Count - 1];
            byte[] bytes = Encoding.Default.GetBytes(objResponse.output.text[objResponse.output.text.Count - 1]);
            txtResult.Text = Encoding.UTF8.GetString(bytes);

            txtPregunta.Text = string.Empty;

            #region speech

            var urlS = "https://stream.watsonplatform.net/text-to-speech/api/v1/synthesize?voice=es-ES_LauraVoice";
            string credentialsS = "690bf31f-e87b-47ab-873d-07cd889eb96c" + ":" + "ZChNsgrcXtS0";
            string jsonDataS = "{\"text\":\"" + txtResult.Text + "\"}";

            using (var clientS = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                client.Headers.Add("accept", "audio/wav");
                client.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentialsS));
                byte[] responseS = client.UploadData(urlS, Encoding.UTF8.GetBytes(jsonDataS));
                //File.WriteAllBytes(@"c:\resp1.wav", responseS);
                File.WriteAllBytes(Server.MapPath("~/resp.wav"), responseS);
            }

            WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
            //Player.URL = @"c:\resp1.wav";
            Player.URL = Server.MapPath("~/resp.wav");
            Player.controls.play();
            //File.Delete(@"c:\resp1.wav");
            File.Delete(Server.MapPath("~/resp.wav"));

            #endregion

        }
        #endregion
    }

    public Stream GenerateStreamFromString(string s)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public void SaveStreamToFile(string fileFullPath, Stream stream)
    {
        if (stream.Length == 0) return;

        // Create a FileStream object to write a stream to a file
        using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
        {
            // Fill the bytes[] array with the stream data
            byte[] bytesInStream = new byte[stream.Length];
            stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

            // Use FileStream object to write to the specified file
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        }
    }
   

    #region "NLC"
    //NLC
    //string url = "https://gateway.watsonplatform.net/natural-language-classifier/api/v1/classifiers/340008x87-nlc-1691/classify?text=";
    //url = url + txtPregunta.Text;
    //WebRequest request = WebRequest.Create(url);
    //string credenciales = "f4bb6476-01b5-4c12-9bd1-59dd88697b21" + ":" + "PhSyKoVZUgrZ";
    //request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credenciales));
    //WebResponse response = request.GetResponse();
    //Stream streamResponse = response.GetResponseStream();
    //StreamReader reader = new StreamReader(streamResponse, Encoding.UTF8);
    //string json = reader.ReadToEnd();
    //txtResult.Text = json;
    #endregion

    #region Conversation 1.0
    ////Conversation
    //MiModelo.RootObject objFirstRequest = new MiModelo.RootObject();
    //objFirstRequest.input.text = txtPregunta.Text;

    //var url = "https://watson-api-explorer.mybluemix.net/conversation/api/v1/workspaces/63ad9983-9e5c-446c-ab3a-05512e95ddec/message?version=2016-07-11";
    ////var jsonData = "{\"input\": {\"text\": \"" + txtPregunta.Text + "\"}, \"alternate_intents\": true}";
    //string credenciales = "ec56a3dc-6b4f-4e54-9513-e28a3aea9d04" + ":" + "3CXhyq4yULYA";

    //MiModelo.RootObject objResponse = new MiModelo.RootObject();
    //var jsonData = string.Empty;

    //string convId = ((MiModelo.RootObject)Session["conversation"]).context.conversation_id;

    //if (string.IsNullOrEmpty(convId))
    //{
    //    jsonData = JsonConvert.SerializeObject(objFirstRequest.input);
    //}
    //else
    //{
    //    //Acá va toda la lógica de modificación de contexto
    //    objConversation = (MiModelo.RootObject)Session["conversation"];
    //    objConversation.workspace_id = "63ad9983-9e5c-446c-ab3a-05512e95ddec";
    //    objConversation.input.text = txtPregunta.Text;
    //    objConversation.context.monto = 100;
    //    //objConversation.output = null; // new MiModelo.Output();
    //    jsonData = JsonConvert.SerializeObject(objConversation);
    //}

    //using (var client = new WebClient())
    //{
    //    client.Headers.Add("content-type", "application/json");
    //    client.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credenciales));
    //    var response = client.UploadString(url, jsonData);
    //    objResponse = JsonConvert.DeserializeObject<MiModelo.RootObject>(response.ToString());

    //    Session["conversation"] = objResponse;

    //    txtResult.Text = objResponse.output.text[objResponse.output.text.Count-1];
    //    txtPregunta.Text = string.Empty;
    //}
    #endregion
}
