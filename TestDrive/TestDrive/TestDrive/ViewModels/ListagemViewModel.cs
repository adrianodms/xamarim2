using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Veiculo> Veiculos { get; set; }
        private Veiculo veiculoSelecionado;
        const string URL_GET_VEICULOS = "http://aluracar.herokuapp.com/";
        public Veiculo VeiculoSelecionado
        {
            get
            {
                return veiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if(veiculoSelecionado != null)
                MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
            }
        }
        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculo>();
        }

        public async void GetVeiculos()
        {
            Aguarde = true;
            HttpClient cliente = new HttpClient();
            string resultado =  await cliente.GetStringAsync(URL_GET_VEICULOS);
            VeiculoJson[] veiculos = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

            foreach (var veiculoJson in veiculos)
            {
                this.Veiculos.Add(new Veiculo
                {
                    Nome = veiculoJson.nome,
                    Preco = veiculoJson.preco
                });
            }
            Aguarde = false;
        }

        class VeiculoJson
        {
            public string nome { get; set; }
            public decimal preco { get; set; }
        }

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }
    }
}
