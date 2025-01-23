namespace MyNewProject
{
    public partial class MainPage : ContentPage
    {
        private readonly int countdown = 3;
        private readonly string[] options = [ "Piedra", "Papel", "Tijeras" ];
        private readonly Random random = new();
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

                await CountdownLabel.ScaleTo(1.5, 150, Easing.SpringOut); // Aumenta el tamaño
                await CountdownLabel.ScaleTo(1, 100, Easing.SpringIn);   // Reduce el tamaño de vuelta al original
                await Task.Delay(500); // Esperar un segundo sin bloquear la UI
            }

            CountdownLabel.IsVisible = false;

            string machineChoice = options[random.Next(options.Length)];
            DetermineResult(userChoice, machineChoice);

            // Hacer el botón reiniciar visible
            StartButton.IsVisible = false;
            RestartButton.IsVisible = true;
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

        // Reiniciar la partida
        private void OnRestartClicked(object sender, EventArgs e)
        {
            // Reiniciar variables y UI para una nueva partida
            userChoice = string.Empty;
            CountdownLabel.IsVisible = false;
            ResultLabel.Text = string.Empty;

            // Restablecer el borde de los botones de opciones
            lastSelectedButton.BorderColor = Colors.Transparent;

            // Hacer el botón de empezar visible nuevamente y habilitarlo
            StartButton.IsVisible = true;
            StartButton.Text = "Empezar";
            StartButton.IsEnabled = false;

            // Hacer el botón de reiniciar invisible
            RestartButton.IsVisible = false;
        }
    }
}
