using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Threading.Tasks;
using System.Windows.Forms;


using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EscritorioAPi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Usuario
        {
            public string correo { get; set; }
            public string clave { get; set; }
        }

        public class Respuesta
        {
            public string token { get; set; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //=============== autenticacion ===========
            var cliente1 = new HttpClient();

            Usuario ob_Usuario = new Usuario() { correo ="c@gmail.com",clave ="123" };

            var content = new StringContent(JsonConvert.SerializeObject(ob_Usuario), Encoding.UTF8, "application/json");
            var response1 = await cliente1.PostAsync("http://localhost:5124/api/Autenticacion/Validar", content);
            var json_respuesta1 = await response1.Content.ReadAsStringAsync();

            var ob_respuesta =  JsonConvert.DeserializeObject<Respuesta>(json_respuesta1);

            //=============== 
            var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ob_respuesta.token);
            var response = await client2.GetAsync("http://localhost:5124/api/Producto/Lista");
            var test = await response.Content.ReadAsStringAsync();
        }
    }
}
