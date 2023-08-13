using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace To_Do_List
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
            taskListView.ItemsSource = tasks;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(titleTextBox.Text) && !string.IsNullOrWhiteSpace(descriptionTextBox.Text))
            {
                tasks.Add(new TaskItem { TaskTitle = titleTextBox.Text, TaskDescription = descriptionTextBox.Text });
                titleTextBox.Clear();
                descriptionTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите заголовок и описание задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить задачи",
                DefaultExt = ".txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveTasksToFile(saveFileDialog.FileName);
                MessageBox.Show("Задачи сохранены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Выбрать файл с задачами",
                DefaultExt = ".txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadTasksFromFile(openFileDialog.FileName);
                MessageBox.Show("Задачи загружены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик события клика по чекбоксу (пометка задачи как выполненной)
            CheckBox checkBox = (CheckBox)sender;
            TaskItem task = (TaskItem)checkBox.DataContext;
            task.IsCompleted = checkBox.IsChecked ?? false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик события клика по кнопке "Удалить"
            Button deleteButton = (Button)sender;
            TaskItem task = (TaskItem)deleteButton.DataContext;
            if (task.IsCompleted)
            {
                tasks.Remove(task);
            }
        }

        private void SaveTasksToFile(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TaskItem task in tasks)
            {
                sb.AppendLine($"{task.TaskTitle}|{task.TaskDescription}|{task.IsCompleted}");
            }

            File.WriteAllText(fileName, sb.ToString());
        }

        private void LoadTasksFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                tasks.Clear();
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        tasks.Add(new TaskItem
                        {
                            TaskTitle = parts[0],
                            TaskDescription = parts[1],
                            IsCompleted = bool.Parse(parts[2])
                        });
                    }
                }
            }
        }
    }
}