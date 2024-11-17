using CadastroEventos.Models;
using CadastroEventos.Views;

namespace CadastroEventos.Views
{
    public partial class CadastroPage : ContentPage
    {
        public CadastroPage()
        {
            InitializeComponent();
           
            DataInicioPicker.Date = DateTime.Now;
            DataTerminoPicker.Date = DateTime.Now.AddDays(1);
        }

        private void OnDataInicioChanged(object sender, DateChangedEventArgs e)
        {
            if (DataTerminoPicker.Date < e.NewDate)
            {
                DataTerminoPicker.Date = e.NewDate;
                DisplayAlert("Aviso", "A data de t�rmino foi ajustada para n�o ser anterior � data de in�cio.", "OK");
            }
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            try
            {

                var evento = new Evento
                {
                    Nome = NomeEntry.Text,
                    DataInicio = DataInicioPicker.Date,
                    DataTermino = DataTerminoPicker.Date,
                    NumeroParticipantes = int.Parse(NumeroParticipantesEntry.Text),
                    Local = LocalEntry.Text,
                    CustoPorParticipante = decimal.Parse(CustoPorParticipanteEntry.Text)
                };


                if (string.IsNullOrWhiteSpace(evento.Nome) || string.IsNullOrWhiteSpace(evento.Local))
                {
                    await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
                    return;
                }

                if (evento.DataTermino < evento.DataInicio)
                {
                    await DisplayAlert("Erro", "A data de t�rmino n�o pode ser antes da data de in�cio.", "OK");
                    return;
                }


                await Navigation.PushAsync(new ResumoPage(evento));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Verifique os dados informados! " + ex.Message, "OK");
            }
        }
    }
}