using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace To_Do_List
{
    class Buttoncommand
    {
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
            SaveTasksToFile("tasks.txt");
            MessageBox.Show("Задачи сохранены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTasksFromFile("tasks.txt");
            MessageBox.Show("Задачи загружены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
