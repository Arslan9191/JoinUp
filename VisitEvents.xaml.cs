using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace JoinUp
{
    public partial class VisitEvents : Window
    {
        private int eventId;
        private string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";

        public VisitEvents(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
            LoadParticipants();
        }

        private void LoadParticipants()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Получаем название мероприятия
                    string getEventNameQuery = "SELECT название FROM мероприятия WHERE id = @Id";
                    using (var nameCommand = new SQLiteCommand(getEventNameQuery, connection))
                    {
                        nameCommand.Parameters.AddWithValue("@Id", eventId);
                        object eventNameResult = nameCommand.ExecuteScalar();
                        EventTitle.Text = $"Участники мероприятия: {eventNameResult?.ToString() ?? "Неизвестно"}";
                    }

                    // Получаем участников
                    string query = @"SELECT посещение.id, участники.имя, посещение.явка
                                     FROM посещение
                                     JOIN участники ON посещение.участник_id = участники.id
                                     WHERE посещение.мероприятие_id = @МероприятиеId";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@МероприятиеId", eventId);

                        using (var reader = command.ExecuteReader())
                        {
                            var participants = new List<UsersEvents>();

                            while (reader.Read())
                            {
                                var participant = new UsersEvents
                                {
                                    AttendanceId = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Attendance = reader.GetBoolean(2)
                                };
                                participants.Add(participant);
                            }

                            participantsDataGrid.ItemsSource = participants;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке участников: {ex.Message}");
            }
        }

        private void MarkAttendance_Click(object sender, RoutedEventArgs e)
        {
            if (participantsDataGrid.SelectedItem is UsersEvents selectedParticipant)
            {
                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE посещение SET явка = 1 WHERE id = @Id";
                        using (var command = new SQLiteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Id", selectedParticipant.AttendanceId);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Явка для {selectedParticipant.Name} отмечена!");
                    LoadParticipants(); // Обновляем список
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении явки: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите участника из списка!");
            }
        }
    }

    public class UsersEvents
    {
        public int AttendanceId { get; set; }
        public string Name { get; set; }
        public bool Attendance { get; set; }
    }
}
