using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Data;
using ExtraerPrediccion.Models;
using System.Diagnostics;
using System.Net.Mail;

namespace CallRequestResponseService
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // CUS 03 -Interconectar componentes 
            InvokeRequestResponseService().Wait();
        }


        static async Task InvokeRequestResponseService()
        {

            // Interconectamos nuestra data local con nuestra data resultado de la predicción en la nueb 
            IEnumerable<ExtraerPrediccion.Models.CargaTemporal> lst;

            using (ExtraerPrediccion.Models.DBPREDICTIVOEntities db = new ExtraerPrediccion.Models.DBPREDICTIVOEntities())
            {
                // Obtenemos la data cargada desde la base de datos donde se genero la carga histórica de datos
                lst = db.CargaTemporal.ToList();

            }

            // Por cada código enviado de procesará la interconexión y envío y consumo de data
            int IndexCodigo = 0;
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            foreach (var item in lst)
            {


                using (var client = new HttpClient())
                {
                    // DataCargada
                    var CODIGO = item.CODIGO.ToString();
                    var DIA1 = Convert.ToInt32(item.DIA1).ToString();
                    var DIA2 = Convert.ToInt32(item.DIA2).ToString();
                    var DIA3 = Convert.ToInt32(item.DIA3).ToString();
                    var DIA4 = Convert.ToInt32(item.DIA4).ToString();
                    var DIA5 = Convert.ToInt32(item.DIA5).ToString();
                    var DIA6 = Convert.ToInt32(item.DIA6).ToString();
                    var DIA7 = Convert.ToInt32(item.DIA7).ToString();
                    var DIA8 = Convert.ToInt32(item.DIA8).ToString();
                    var DIA9 = Convert.ToInt32(item.DIA9).ToString();
                    var DIA10 = Convert.ToInt32(item.DIA10).ToString();
                    var DIA11 = Convert.ToInt32(item.DIA11).ToString();

                    // Envío de la data al servicio predictivo en AZURE
                    var scoreRequest = new
                    {

                        Inputs = new Dictionary<string, StringTable>() {
                        {

                            "input1",
                            new StringTable()
                            {
                                  ColumnNames = new string[] {"Col1", "Col2", "Col3", "Col4", "Col5", "Col6", "Col7", "Col8", "Col9", "Col10", "Col11", "Col12"},

                                Values = new string[,] {  { CODIGO, DIA1, DIA2, DIA3, DIA4, DIA5, DIA6, DIA7, DIA8, DIA9, DIA10, DIA11 } }
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>() {
                                      { "Account name", "" },
                                                                        }
                    };
                    const string apiKey = "nOBO1gD67I9AaAN0I25pRXzCJF+nNanb4kOnxMS6zU403hJxITfWt4Y/UfFK2Eyp/Zwlkb10zJhBEexuhiqLXQ=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    //client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/3c39f696d1ff4002b750eeef43304837/services/d8170eca54304ce0b9865cbc42c5ca2c/execute?api-version=2.0&details=true");
                    //https://ussouthcentral.services.azureml.net/workspaces/3c39f696d1ff4002b750eeef43304837/services/d8170eca54304ce0b9865cbc42c5ca2c/execute?api-version=2.0&format=swagger
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/3c39f696d1ff4002b750eeef43304837/services/d8170eca54304ce0b9865cbc42c5ca2c/execute?api-version=2.0&details=true");
                    // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                    // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                    // For instance, replace code such as:
                    //      result = await DoSomeTask()
                    // with the following:
                    //      result = await DoSomeTask().ConfigureAwait(false)


                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                    if (response.IsSuccessStatusCode)//respuesta
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Result: {0}", result);

                        //Extraemos el resultado predictivo para cargarlo a la base BDPREDICTIVA de la empresa
                        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                        var score = myDeserializedClass.Results.output1.value.Values.ElementAt(0).ElementAt(12).Substring(0, 4);//.Replace('.', ';');
                        // Formateamos el valor
                        decimal stockPredictivo = Convert.ToDecimal(score);
                        var codigoArticulo = myDeserializedClass.Results.output1.value.Values.ElementAt(0).ElementAt(0);

                        //Actualizamos los resultados en la tabla Stock de la empresa
                        using (DBPREDICTIVOEntities db = new DBPREDICTIVOEntities())
                        {
                            int oStock = Convert.ToInt16(Math.Ceiling(stockPredictivo));

                            db.sp_cargarStockPredictivo(oStock, Convert.ToInt32(codigoArticulo));
                            db.SaveChanges();
                            
                        }
                        IndexCodigo++;
                        Console.WriteLine(string.Format("Actualizando y cargando predicción- Artículo [" + IndexCodigo + "]-"+ codigoArticulo + "Stock Predictivo: ["+ Convert.ToString(Convert.ToInt16(Math.Ceiling(stockPredictivo))) + "]", response.StatusCode));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                        // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                        Console.WriteLine(response.Headers.ToString());

                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }

            }

            
            SendEmail("HERALDAVA@GMAIL.COM");
            Console.WriteLine("Stock predictivo actualizado");
            Console.ReadLine();
            timeMeasure.Stop();
            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMinutes} min");



        }




        static void SendEmail(string EmailDestino)
        {
            string EmailOrigen = "HERALDAVA@GMAIL.COM";
            string Contraseña = "/HERBERTSRS";
            //string url = UrlDomain + "/Acceso/Recovery/?token=" + Token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "GRUPO GALDIAZ-STOCK PREDICTIVO",
                                     "<p>Correo de confirmación de ejecución de demanda predictiva-stock de artículos.</p>" +
                                     "" + DateTime.Now + "'>Actualización exitosa.</a>");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        // Clases para los resultados predictivos que serán consumidos desde Azure
        public class Value
        {
            public List<string> ColumnNames { get; set; }
            public List<string> ColumnTypes { get; set; }
            public List<List<string>> Values { get; set; }
        }

        public class Output1
        {
            public string type { get; set; }
            public Value value { get; set; }
        }

        public class Results
        {
            public Output1 output1 { get; set; }
        }

        public class Root
        {
            public Results Results { get; set; }
        }

    }
}
