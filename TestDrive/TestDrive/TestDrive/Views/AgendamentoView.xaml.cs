using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentoView : ContentPage
    {

        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo)
        {
            InitializeComponent();
            this.ViewModel = new AgendamentoViewModel(veiculo);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento", async (Agendamento agendamento) =>
            {
                bool confirmacao = await DisplayAlert("Salvar Agendamento", "Deseja mesmo enviar o agendamento?", "sim", "não");
                if (confirmacao) {
                    this.ViewModel.SalvaAgendamento();
                    //DisplayAlert(
                    //"Agendamento",
                    //string.Format(
                    //    @"nome: {0}, Fone:{1}, E-mail:{2}, Data Agendamento: {3:dd/MM/yyyy}, Hora Agendamento: {4}",
                    //   agendamento.nome, agendamento.Fone, agendamento.email, agendamento.DataAgendamento, agendamento.HoraAgendamento),
                    //"OK"
                    //);
                }
            });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", (Agendamento agendamento) =>
            {
                DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "ok");
            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", (ArgumentException exception) =>
            {
                DisplayAlert("Agendamento", "Falha ao agendar o test drive, verifique os dados e tente novamente mais tarde!", "ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");

            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");

            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}