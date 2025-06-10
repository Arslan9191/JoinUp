using System;
using System.Data.SQLite;
using System.Windows;

namespace JoinUp
{
    public partial class CreateEvent : Window
    {
        public CreateEvent()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";
        string insertEvent = "INSERT INTO мероприятия (название, описание, дата, организатор_id) VALUES (@nameEvent, @dcsrp, @date, @idOrgnz);";

        void RegEvent()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(insertEvent, connection))
                    {
                        command.Parameters.AddWithValue("@nameEvent", fieldNameEvent.Text);
                        command.Parameters.AddWithValue("@dcsrp", fieldDescriptionEvent.Text);
                        command.Parameters.AddWithValue("@date", fieldDateEvent.Text);
                        command.Parameters.AddWithValue("@idOrgnz", RegPage.memoryIdUser); // <--- Используем сохранённый ID организатора

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании мероприятия: {ex.Message}");
            }
        }

        private void PressCreateButt(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fieldNameEvent.Text) || string.IsNullOrWhiteSpace(fieldDateEvent.Text))
            {
                MessageBox.Show("Название и дата обязательны к заполнению!");
                return;
            }

            RegEvent();
            MessageBox.Show("Мероприятие создано!");
            this.Close(); // Закрываем окно после создания
        }
    }
}
