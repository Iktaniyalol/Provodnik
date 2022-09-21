using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Проводник
{
    public partial class RenameForm : Form
    {
        Regex reg = new Regex(@"[\\\/\:\*\?"+"\""+@"\<\>]"); //О да, мои любимые регулярные выражения, как же я их люблю, емое ребят, каждый день бы писала с ними что-то (пожалуйста помогите)
        MainForm parent; //Главная форма
        public RenameForm(string originalFilePath, MainForm parent) //Иницизализация формы
        {
            InitializeComponent();
            OriginalFilePathLapel.Text = originalFilePath; //Выводим 
            FileNameTextBox.Text = Path.GetFileName(originalFilePath); //Выводим название файла (изначальное)
            this.parent = parent; //Главная форма
        }

        private void OkButton_Click(object sender, EventArgs e) //Нажали на кнопку "ок"
        {
            if (reg.IsMatch(FileNameTextBox.Text) || FileNameTextBox.Text.Trim() == "") //Соответствует ли имя нашему регулярному выражению и не пустое ли оно
            { 
                MessageBox.Show("Недопустимые символы: \\/:*?\"<>; а также пустая строка."); //Ха, на что ты надеялся? ЮЗЕР!
                return;
            }
            string newFilePath = Path.GetDirectoryName(OriginalFilePathLapel.Text) + "\\" + FileNameTextBox.Text.Trim(); //Новый путь файла с новым именем
            if(newFilePath == OriginalFilePathLapel.Text) //Пользователь оставил имя таким же, как и было 
            {
                parent.pasted = true; //Файл вставлен
                //Блин ну е мое, опять использую уже существующие переменные для своих корыстных целей. Ужас, попаду за это в ад программистов
                parent.pastedObjectName = newFilePath; //Записываю путь
                parent.UpdateList(); //Обновляю список
                Close(); //Закрываю форму
                return;
            }
            try
            {
                if (File.Exists(OriginalFilePathLapel.Text) || Directory.Exists(OriginalFilePathLapel.Text)) //Если файл или папка существует и пользователь черепаха
                {
                    Directory.Move(OriginalFilePathLapel.Text, newFilePath); //Перемещаю
                    parent.pasted = true; //Файл вставлен
                    //Блин ну е мое, опять использую уже существующие переменные для своих корыстных целей. Ужас, попаду за это в ад программистов
                    parent.pastedObjectName = newFilePath; //Записываю путь
                }
                else
                {
                    MessageBox.Show("Переименуемого объекта более не существует."); //Вывод ошибки
                }
                parent.UpdateList(); //Обновляю список
                Close(); //Закрываю форму
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message); //Вывод ошибки
                parent.UpdateList(); //Обновляю список
            }
        }
        private void CancelButton_Click(object sender, EventArgs e) //Нажали кнопку отмена
        {
            parent.UpdateList(); //Обновляю список
            Close(); //Закрываю форму
        }
    }
}
