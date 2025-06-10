using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Xml.Linq;

namespace JoinUp
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        public static int memoryIdUser = -1; // -1, если не авторизован
        public static string memoryNameUser = "";
        public static string memoryPassUser = "";

        string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\ConsoleApp1\\bin\\Debug\\net8.0\\events.db;Version=3;";
        string memberInsertQuery = "INSERT INTO участники (имя, пароль) VALUES (@name, @password);";
        string memberCheckUsersQuery = "SELECT COUNT(*) FROM участники WHERE имя = @name AND пароль = @password;\r\n";
        string organizerCheckUsersQuery = "SELECT COUNT(*) FROM организаторы WHERE имя = @name AND пароль = @password;\r\n";
        string organizerInsertQuery = "INSERT INTO организаторы (имя, пароль) VALUES (@name, @password);";

        /// <summary>
        /// Проверка заполненности полей
        /// </summary>
        int checkFields()
        {
            bool fieldsIsCorrect = false;

            if (userField.Text == "" | passField.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return 0;
            }

            else
            {
                fieldsIsCorrect = true;
                return 1;
            }
        }

        /// <summary>
        /// Проверка что пользователь существует в базе данных
        /// </summary>
        /// <returns></returns>
        int checkExistUsers()
        {   
            if (MainWindow.enterFlag == 'm')
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(memberCheckUsersQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", userField.Text);
                        command.Parameters.AddWithValue("@password", passField.Text);

                        long count = (long)command.ExecuteScalar();

                        if (count == 0)
                        {
                            return 1;
                        }

                        else
                        {
                            return 0;
                        }
                    }

                }
            }

            else
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(organizerCheckUsersQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", userField.Text);
                        command.Parameters.AddWithValue("@password", passField.Text);

                        long count = (long)command.ExecuteScalar();

                        if (count == 0)
                        {
                            return 1;
                        }

                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Функция логирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PressLoginButt(object sender, RoutedEventArgs e)
        {
            if (checkFields() == 1)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query;
                    if (MainWindow.enterFlag == 'm')
                        query = "SELECT id, имя FROM участники WHERE имя = @name AND пароль = @password;";
                    else
                        query = "SELECT id, имя FROM организаторы WHERE имя = @name AND пароль = @password;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", userField.Text);
                        command.Parameters.AddWithValue("@password", passField.Text);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                RegPage.memoryIdUser = reader.GetInt32(0);  // Сохраняем ID пользователя
                                memoryNameUser = reader.GetString(1); // Сохраняем имя

                                MessageBox.Show("Вход совершён!");
                                RegFrame.Navigate(new MenuPage());

                                loginButton.Visibility = Visibility.Collapsed;
                                regButton.Visibility = Visibility.Collapsed;
                                passField.Visibility = Visibility.Collapsed;
                                userField.Visibility = Visibility.Collapsed;
                                titleApp.Visibility = Visibility.Collapsed;
                                logText.Visibility = Visibility.Collapsed;
                                passText.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                MessageBox.Show("Такого пользователя не существует!");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Функция регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PressRegButt(object sender, RoutedEventArgs e)
        {
            if (MainWindow.enterFlag == 'm')
            {
                if (checkFields() == 1)
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        if (checkExistUsers() == 1)
                        {
                            using (var command = new SQLiteCommand(memberInsertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@name", userField.Text);
                                command.Parameters.AddWithValue("@password", passField.Text);

                                command.ExecuteNonQuery();

                                MessageBox.Show("Регистрация прошла успешно!");
                            }
                        }

                        else
                        {
                            MessageBox.Show("Пользователь с указанными данными уже существует!");
                        }
                    }
                }
            }

            else
            {
                if (checkFields() == 1)
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        if (checkExistUsers() == 1)
                        {
                            using (var command = new SQLiteCommand(organizerInsertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@name", userField.Text);
                                command.Parameters.AddWithValue("@password", passField.Text);

                                command.ExecuteNonQuery();

                                MessageBox.Show("Регистрация прошла успешно!");
                            }
                        }

                        else
                        {
                            MessageBox.Show("Пользователь с указанными данными уже существует!");
                        }
                    }
                }
            }
            
        }
    }
}
