using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace JoinUp
{
    public partial class MenuPage : Page
    {
        string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";
        public MenuPage()
        {
            InitializeComponent();

            if (MainWindow.enterFlag == 'm')
            {
                flagText.Text = "Вы - участник";
                nameUserr.Text = $"Добро пожаловать, {RegPage.memoryNameUser}!";
                createEvent.Visibility = Visibility.Collapsed;
                openVisitButt.Visibility = Visibility.Collapsed;
                deleteEvent.Visibility = Visibility.Collapsed;
            }
            else
            {
                flagText.Text = "Вы - организатор";
                nameUserr.Text = $"Добро пожаловать, {RegPage.memoryNameUser}!";
                sendBidButt.Visibility = Visibility.Collapsed;
            }

            LoadEvents();
        }

        // Метод для загрузки мероприятий из базы данных
        private void LoadEvents()
        {
            string query;

            // Определяем запрос в зависимости от типа пользователя
            if (MainWindow.enterFlag == 'o') // 'o' — организатор
            {
                query = "SELECT название, описание, дата FROM мероприятия WHERE организатор_id = @ОрганизаторId";
            }
            else // 'm' — участник
            {
                query = "SELECT название, описание, дата FROM мероприятия";
            }

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (MainWindow.enterFlag == 'o')
                        {
                            // Указываем id организатора
                            command.Parameters.AddWithValue("@ОрганизаторId", RegPage.memoryIdUser);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            var eventsList = new List<Event>();

                            while (reader.Read())
                            {
                                var eventItem = new Event
                                {
                                    Name = reader.GetString(0),
                                    Description = reader.IsDBNull(1) ? "" : reader.GetString(1), // На случай, если описание пустое
                                    Date = reader.GetString(2)
                                };
                                eventsList.Add(eventItem);
                            }

                            eventsDataGrid.ItemsSource = eventsList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке мероприятий: {ex.Message}");
            }
        }


        // Кнопка создания мероприятия
        private void createEventButt(object sender, RoutedEventArgs e)
        {
            CreateEvent crEvwindow = new CreateEvent();
            crEvwindow.Show();
        }

        private void DeleteEventClick(object sender, RoutedEventArgs e)
        {
            if (eventsDataGrid.SelectedItem is Event selectedEvent)
            {
                string deleteEventQuery = "DELETE FROM мероприятия WHERE название = @МероприятиеНазвание";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(deleteEventQuery, connection))
                    {
                        // Передаем название мероприятия
                        command.Parameters.AddWithValue("@МероприятиеНазвание", selectedEvent.Name);

                        try
                        {
                            // Выполняем запрос
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Мероприятие успешно удалено.");
                                LoadEvents();  // Обновляем список мероприятий
                            }
                            else
                            {
                                MessageBox.Show("Не удалось найти мероприятие для удаления.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при удалении мероприятия: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие для удаления.");
            }
        }

        private void OpenVisit_Click(object sender, RoutedEventArgs e)
        {
            if (eventsDataGrid.SelectedItem is Event selectedEvent)
            {
                string eventName = selectedEvent.Name;
                string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";

                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        string findEventIdQuery = "SELECT id FROM мероприятия WHERE название = @Название";
                        using (var command = new SQLiteCommand(findEventIdQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Название", eventName);

                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                int eventId = Convert.ToInt32(result);

                                VisitEvents visitEventsWindow = new VisitEvents(eventId); // <-- СЮДА ПЕРЕДАЕМ eventId
                                visitEventsWindow.Show();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось найти выбранное мероприятие.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии посещаемости: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие из списка!");
            }
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            LoadEvents();
        }

        private void SendBitButton(object sender, RoutedEventArgs e)
        {
            if (eventsDataGrid.SelectedItem is Event selectedEvent)
            {
                string eventName = selectedEvent.Name;

                string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";

                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Сначала находим id мероприятия по названию
                        string findEventIdQuery = "SELECT id FROM мероприятия WHERE название = @Название";
                        using (var findCommand = new SQLiteCommand(findEventIdQuery, connection))
                        {
                            findCommand.Parameters.AddWithValue("@Название", eventName);

                            object result = findCommand.ExecuteScalar();

                            if (result != null)
                            {
                                int eventId = Convert.ToInt32(result);

                                // Теперь добавляем участие
                                string insertAttendanceQuery = "INSERT INTO посещение (мероприятие_id, участник_id, явка) VALUES (@МероприятиеId, @УчастникId, 0)";
                                using (var insertCommand = new SQLiteCommand(insertAttendanceQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@МероприятиеId", eventId);
                                    insertCommand.Parameters.AddWithValue("@УчастникId", RegPage.memoryIdUser);

                                    insertCommand.ExecuteNonQuery();

                                    MessageBox.Show("Вы успешно подали заявку на участие!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не удалось найти выбранное мероприятие.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке заявки: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие из списка!");
            }
        }

    }

    // Класс для представления мероприятия
    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
