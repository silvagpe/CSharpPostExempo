using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExemploPostPHP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            var baseAddress = "https://api.anotameupedido.com.br/picanhariatiton/integracao/salvarproduto";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";


            //Criar a lista de produtos
            List<ExpandoObject> dados = new List<ExpandoObject>();
            dynamic produto = new ExpandoObject();
            produto.cod_sinc = 1469;
            produto.nome = "BROCOLI";
            produto.descricao = "";
            produto.preco_uni = "10.5";
            produto.desconto = "0";
            produto.cod_sinc_subgrupo = "8";
            produto.ativo = true;


            dynamic unidade = new ExpandoObject();
            unidade.cod_sinc = "UN";
            unidade.sigla = "UN";
            unidade.descricao = "UN";

            //Adiciona a unidade ao produto
            produto.unidade = unidade;
            
            //Adiciona o produto a lista
            dados.Add(produto);

            dynamic json = new ExpandoObject();
            json.chave_api = "<MINHA CHAVE>";
            json.dados = dados.ToArray();
            
            //Converte para o objeto para string
            string parametros = JsonConvert.SerializeObject(json);



            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parametros);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            textBox1.Text = content;


        }
    }
}
