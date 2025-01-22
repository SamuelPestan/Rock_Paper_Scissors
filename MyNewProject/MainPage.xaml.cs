namespace MyNewProject
{
    public partial class MainPage : ContentPage
    {
        private int countdown = 3;
        private string[] options = { "Piedra", "Papel", "Tijeras" };
        private Random random = new Random();
        private string userChoice;
        private Button lastSelectedButton;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            OptionsLayout.IsVisible = true;
            CountdownLabel.IsVisible = true;

            for (int i = countdown; i > 0; i--)
            {
                CountdownLabel.Text = i.ToString();
                await Task.Delay(1000); // Esperar un segundo sin bloquear la UI
            }

            CountdownLabel.IsVisible = false;

            string machineChoice = options[random.Next(options.Length)];
            DetermineResult(userChoice, machineChoice);
        }

        private void OnOptionSelected(object sender, EventArgs e) 
        { 
            if (sender is Button selectedButton)
            {
                userChoice = selectedButton.Text;

                // Quitar el borde del ultimo botón en caso de que exista
                if (lastSelectedButton != null)
                {
                    lastSelectedButton.BorderColor = Colors.Transparent;
                }

                // Agregar el borde al botón actual seleccionado
                selectedButton.BorderColor = Colors.Coral;
                lastSelectedButton = selectedButton;

                StartButton.IsEnabled = true;
            }
        }

        private void DetermineResult(string user, string machine)
        {
            if (user == machine)
            {
                ResultLabel.Text = $"Empate. Ambos eligieron {user}.";
            }
            else if ((user == "Piedra" && machine == "Tijeras") ||
                     (user == "Papel" && machine == "Piedra") ||
                     (user == "Tijeras" && machine == "Papel"))
            {
                ResultLabel.Text = $"¡Ganaste! Elegiste {user} y la máquina {machine}.";
            }
            else
            {
                ResultLabel.Text = $"Perdiste. Elegiste {user} y la máquina {machine}.";
            }
        }
    }
}
