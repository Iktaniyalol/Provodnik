using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Проводник
{
    public partial class MainForm : Form
    {
        List<string> objectsInFolder = new List<string>(); //Текущие файлы/папки(их пути) в директории
        List<string> fragmentedPath = new List<string>(); //Фрагменты текущего пути
        string pathForPathBox = ""; //Введённый пользователем путь
        string copyBuffer = ""; //Путь файла, который мы копируем
        bool copyCutMode = false; //Включен ли режим "Вырезать"
        string lastFolderPath; //При обновлении папки, в которой сейчас находится пользователь нужно сверить, равен ли его путь тому, что мы запомнили. Если нет, выделение убирается
        public bool pasted = false; //Произошла ли вставка файла или переименовывание
        public string pastedObjectName; //Имя вставленного объекта (переименованного)
        bool copied = false; //Работает ли кнопка вставить
        string history = ""; //История
        CalculateFolderSize calculator; //Глобальная переменная, где хранится объект отдельного класса для высчитывания размера папки
        public MainForm()
        {
            InitializeComponent();
            objectsInFolder.Add(".."); //Добавляется пункт "назад"
            UpdateList(); //Обновляем все в текущей папке
            PathBox.Text = ""; //Текущий путь (мы по умолчанию находимся в корневой директории)
            PathBox.BackColor = Color.FromArgb(225, 225, 225); //Меняем цвет панельки на серый, т.к. она неактивна
            lastFolderPath = GetCurrentFolderPath(); //Текущий путь
        }
        public void UpdateList()
        {
            if (fragmentedPath.Count > 0 && !Directory.Exists(GetCurrentFolderPath())) //Существует ли папка, в которой мы сейчас находимся (ее могли удалить)
            {
                MessageBox.Show("Ошибка: данной папки более не существует."); //Всплывающее окно
                fragmentedPath.Clear(); //Очищаем фрагменты текущего пути
            }
            int selectedindex = -2; //Значение по умолчанию
            int selectedindexPaste = -2; //Значение по умолчанию
            bool foundPasted = false; //Найден ли файл, который мы вставили
            if (lastFolderPath == GetCurrentFolderPath()) //Изменился ли путь
                selectedindex = ObjectContainer.SelectedIndex; //Если неизменился, выделяем выбранный объект
            ClearObjectsInFolder(); //Очищаем objectsInFolder
            ObjectContainer.Items.Clear(); //Очищаем ListBox
            if (fragmentedPath.Count == 0) //Мы в корневой папке
            {
                string[] disks = Environment.GetLogicalDrives(); //Получаем логические диски
                for (int j = 0; j < disks.Length; j++) //перебираем их
                {
                    objectsInFolder.Add(disks[j].Substring(0, disks[j].Length - 1)); //Добавляем в objectsInFolder (Добавляем в текущие объекты)
                    ObjectContainer.Items.Add(disks[j].Substring(0, disks[j].Length - 1)); //Выводим в listBox
                    if (objectsInFolder[j + 1] == pastedObjectName) //Дада, я ленивая. Я решила воспользоваться pasted чтоб выделить, из какого диска пользователь вышел. Не судите пожалуйста строго, ставьте классы и подписывайтесь
                    {
                        foundPasted = true;
                        selectedindexPaste = j;
                    }

                }
            }
            else //иначе
            {
                string[] objects = Directory.GetDirectories(GetCurrentFolderPath()); //Получаем директории в текущей папке
                for (int j = 0; j < objects.Length; j++) //Перебираем их
                {
                    objectsInFolder.Add(objects[j]); //Добавляем в текущие объекты
                }
                objects = Directory.GetFiles(GetCurrentFolderPath()); //Получаем файлы в текущей папке
                for (int j = 0; j < objects.Length; j++) //Перебираем их
                {
                    objectsInFolder.Add(objects[j]); //Добавляем в текущие объекты
                }
                ObjectContainer.Items.Add(".."); //Добавляем "назад"
                for (int i = 1; i < objectsInFolder.Count; i++) //Перебираем все объекты (objectsInFolder)
                {
                    string filename = Path.GetFileName(objectsInFolder[i]); //Имя файла/папки
                    if (copyCutMode && objectsInFolder[i] == copyBuffer) filename = "✂ " + filename; //Если данный файл "вырезается", добавляем символ ножниц
                    if (Directory.Exists(objectsInFolder[i])) //Если это папка, добавляем символ папки
                        filename = "📂 " + filename;
                    ObjectContainer.Items.Add(filename); //Выводим в listBox
                    if (objectsInFolder[i] == pastedObjectName) //Если объект "вставляется"/"переименовывается", то записываем его индекс и меняем индикатор foundPasted на true
                    {
                        foundPasted = true;
                        selectedindexPaste = i;
                    }
                }
            }
            PathBox.Text = GetCurrentFolderPath(); //Выводим текущий путь
            pathForPathBox = PathBox.Text; //Пишем, что текущий введённый путь пользователем равен текущему пути
            if (pasted && foundPasted) //Если произошла вставка/переименовывание и мы нашли вставляемый/переименованный объект
            {
                ObjectContainer.SelectedIndex = selectedindexPaste; //Ставим, что выбранный объект тот, который мы переименовали/вставили
                pasted = false; //вставка/переименовывание закончилась
            }
            else if (selectedindex > -2 && ObjectContainer.Items.Count > selectedindex) //Если индекс выбранного объекта больше -2 и количество объектов в listBox больше этого индекса
                ObjectContainer.SelectedIndex = selectedindex; //выделяем в listBox выбранный объект
            lastFolderPath = GetCurrentFolderPath(); //Меняем последний путь на текущий
            PropertiesPanel.Visible = false; //Выключаем панельку свойств
        }
        private string GetCurrentFolderPath()
        {
            string path = ""; //Инициализируем пустой string
            if (fragmentedPath.Count == 0) return path; //Если фрагментов пути нет, возвращаем ""
            for (int i = 0; i < fragmentedPath.Count; i++) //Перебираем фрагменты пути
            {
                path += fragmentedPath[i] + "\\"; //Строим путь
            }
            return path; //Возвращаем
        }
        private void ClearObjectsInFolder() //Очищаем текущие объекты кроме "назад"
        {
            while (objectsInFolder.Count > 1) //Пока у нас больше 1 объекта
            {
                objectsInFolder.RemoveAt(1); //Удаляем
            }
        }
        private void ObjectContainer_MouseDoubleClick(object sender, MouseEventArgs e) //Эвент двойного клика мышки
        {
            if (ObjectContainer.SelectedIndex == -1) return; //Ничего не выделено, ничего не открываем
            Rectangle itemZone = ObjectContainer.GetItemRectangle(ObjectContainer.SelectedIndex); //Получаем зону выделенного объекта
            if (itemZone.Contains(e.Location)) //Если мы нажали в данной зоне
            {
                OpenSelected(); //Открываем
            }
        }
        private void OpenSelected() //Открыть выбранный объект
        {
            if (fragmentedPath.Count == 0) //Если мы в корневой директории
            {
                if (Directory.Exists(objectsInFolder[ObjectContainer.SelectedIndex + 1])) //Если диск никуда не пропал пока мы его открывали
                {
                    history = GetCurrentFolderPath(); //Записываем в историю корневую директорию
                    fragmentedPath.Add(ObjectContainer.Items[ObjectContainer.SelectedIndex].ToString()); //Добавляем в фрагменты пути диск, куда мы отправились
                    UpdateList(); //Выводим объекты из диска
                    BackButton.Enabled = true; //Включаем кнопку назад
                    ForwardButton.Enabled = false; //Выключаем кнопку вперед (Ну а вдруг, а вы что хотели? думали все так просто в вашем программировании? Я сама в шоке)
                }
                else
                {
                    UpdateList(); //Обновляем и выводим текущие логические диски
                }
            }
            else //О нет, мы не в корневой директории
            {
                if (objectsInFolder[ObjectContainer.SelectedIndex] == "..") //Нажали назад
                {
                    history = GetCurrentFolderPath(); //Записываем в историю текущую папку

                    pasted = true; //Дада, я ленивая. Я решила воспользоваться pasted чтоб выделить, из какой папки пользователь вышел. Не судите пожалуйста строго, ставьте классы и подписывайтесь
                    pastedObjectName = GetCurrentFolderPath().Substring(0, GetCurrentFolderPath().Length - 1); //Записываем имя якобы вставляемого объекта, но на деле это просто папка из которой пользователь вышел
                    // СЕРЬЕЗНО НЕ СУДИТЕ СТРОГО
                    fragmentedPath.RemoveAt(fragmentedPath.Count - 1); //Удаляем фрагмент пути
                    UpdateList(); //Обновляем и выводим текущие объекты
                    BackButton.Enabled = true; //Включаем кнопку назад
                    ForwardButton.Enabled = false; //Выключаем кнопку вперед (Ну а вдруг, а вы что хотели? думали все так просто в вашем программировании? Я сама в шоке)
                    return;
                }
                string filePath = objectsInFolder[ObjectContainer.SelectedIndex]; //Берем путь к файлу/папке, которую открываем
                if (Directory.Exists(filePath)) //Существует ли директория. Ну вдруг что, удалили например
                {
                    string historyTemp = GetCurrentFolderPath(); //Добавляем в временную переменную текущую папку
                    fragmentedPath.Add(Path.GetFileName(objectsInFolder[ObjectContainer.SelectedIndex])); //Добавляем в фрагменты имя
                    try
                    {
                        Directory.GetDirectories(GetCurrentFolderPath()); //Пробуем получить директорию из пути
                        history = historyTemp; //Записываем в историю текущую папку
                        BackButton.Enabled = true; //Включаем кнопку назад
                        ForwardButton.Enabled = false; //Выключаем кнопку вперед (Ну а вдруг, а вы что хотели? думали все так просто в вашем программировании? Я сама в шоке)
                    }
                    catch (Exception ex) //Э блин, у тебя нет прав
                    {
                        MessageBox.Show("Возникла ошибка: " + ex.Message); //Увы, ты безправный
                        fragmentedPath.RemoveAt(fragmentedPath.Count - 1); //Удаляем фрагмент пути, куда пользователь собирался намылиться
                    }
                    UpdateList(); //Обновляем и выводим текущие файлы/папки
                }
                else if (File.Exists(filePath)) //Если это файл
                {
                    Process openFile = new Process(); //Создаем объект класса Process
                    openFile.StartInfo = new ProcessStartInfo() //Меняем настройки процесса, запускаем через программу по умолчанию
                    {
                        UseShellExecute = true,
                        FileName = filePath
                    };
                    openFile.Start(); //Запускаем файл
                }
                else
                {
                    MessageBox.Show("Данного объекта более не существует."); //Ой, объект испарился пока ты пытался его открыть. Попробуй в другой раз
                    UpdateList(); //Обновляем и выводим текущие файлы/папки
                }
            }
        }
        private void PathBox_Leave(object sender, EventArgs e) //Эвент, покидаем панельку текущего пути
        {
            pathForPathBox = PathBox.Text; //Записываем, что ввел пользователь
            PathBox.BackColor = Color.FromArgb(225, 225, 225); //Серый цвет (неактивно)
            PathBox.Text = GetCurrentFolderPath(); //Выводим текущий путь
        }

        private void PathBox_Enter(object sender, EventArgs e)
        {
            PathBox.BackColor = Color.FromArgb(255, 255, 255); //Делаем панельку активной (меняем цвет на белый)
            PathBox.Text = pathForPathBox; //Выводим что пользователь не довводил в прошлый раз
            //Правда круто? Мы сохраняем куда пользователь хотел перейти
            //Хехехехе
        }

        private void ObjectContainer_MouseDown(object sender, MouseEventArgs e) //Нажали мышкой по listBox
        {
            if (e.Button == MouseButtons.Right) //Правой кнопкой
            {
                ObjectContainer.SelectedIndex = ObjectContainer.IndexFromPoint(e.Location); //Выделяем объект, который пользователь выбрал
            }

        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e) //Контекстное меню, открытие
        {
            if (ObjectContainer.SelectedIndex == -1) //Если пользователь не выбирал объект
            {
                //Все вырубаем, ибо нефиг!
                OpenButton.Enabled = false;
                CopyButton.Enabled = false;
                CutButton.Enabled = false;
                RenameButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                //Все врубаем
                OpenButton.Enabled = true;
                CopyButton.Enabled = true;
                CutButton.Enabled = true;
                RenameButton.Enabled = true;
                DeleteButton.Enabled = true;
            }
            if (copied) //Если пользователь копировал/вырезал файл
            {
                PasteButton.Enabled = true;
            }
            if (fragmentedPath.Count == 0) //Пользователь в корневой директории
            {
                if (ObjectContainer.SelectedIndex == -1) //Если пользователь не выбирал объект
                {
                    OpenButton.Enabled = false; //Че ты пытался открыть? Все нормально?
                }
                else
                {
                    OpenButton.Enabled = true; //Можно открыть
                }
                //Все вырубаем, ибо нефиг!
                CopyButton.Enabled = false;
                CutButton.Enabled = false;
                RenameButton.Enabled = false;
                DeleteButton.Enabled = false;
                PasteButton.Enabled = false;
            }
        }

        private void OpenButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку открытия в контекстном меню
        {
            OpenSelected(); //Открыть выбранный файл
        }

        private void CopyButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку копирования в контекстном меню
        {
            if (fragmentedPath.Count == 0) //Пользователь в корневой директории
            {
                MessageBox.Show("Нельзя скопировать диск."); //Ты серьезно? Как у тебя это получилось вообще? Господи, опять эти пользователи как-то ломают мою программу!
            }
            else
            {
                copyBuffer = objectsInFolder[ObjectContainer.SelectedIndex]; //Записываем в буфер путь копируемого файла/папки
                copied = true; //Мы копируем
                copyCutMode = false; //Мы не вырезаем, мы копируем
                UpdateList(); //Обновляем и выводим текущие файлы/папки
            }
        }

        private void CutButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку вырезать в контекстном меню
        {
            if (fragmentedPath.Count == 0) //Пользователь в корневой директории
            {
                MessageBox.Show("Нельзя вырезать диск."); //Ты серьезно? Как у тебя это получилось вообще? Господи, опять эти пользователи как-то ломают мою программу!
            }
            else
            {
                copyBuffer = objectsInFolder[ObjectContainer.SelectedIndex]; //Записываем в буфер путь вырезаемого файла/папки
                copied = true; //Мы копируем
                copyCutMode = true; //Мы вырезаем
                UpdateList(); //Обновляем и выводим текущие файлы/папки
            }
        }

        private void PasteButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку вставить в контекстном меню
        {
            try
            {
                if (fragmentedPath.Count == 0) //Пользователь в корневой директории
                {
                    MessageBox.Show("Это интерфейс с дисками, сюда нельзя вставить файлы"); //Ох уж эти юзеры, я даже комментировать это не буду
                }
                if (Directory.Exists(copyBuffer)) //Существует ли папка, которую мы должны вставить. Ну мало ли
                {
                    bool success = false; //Получилось ли вставить
                    if (Directory.Exists(GetCurrentFolderPath() + Path.GetFileName(copyBuffer))) //Существует ли уже такая папка
                    {
                        if (MessageBox.Show("Такой файл уже существует в данной папке, заменить?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes) //Диалоговое окно, надо ли заменить?
                        {
                            success = CopyDirectory(copyBuffer, GetCurrentFolderPath() + Path.GetFileName(copyBuffer), true); //Результат копирования
                            if (!success) //Копирование не удалось
                            {
                                MessageBox.Show("Возникли ошибки, папка скопирована с заменой частично или не скопирована вовсе."); //Выводим ошибку
                            }
                        }
                        else return;
                    }
                    else
                    {
                        success = CopyDirectory(copyBuffer, GetCurrentFolderPath() + Path.GetFileName(copyBuffer), false); //Копируем без проблем
                        if (!success) //Копирование не удалось
                        {
                            MessageBox.Show("Возникли ошибки, папка скопирована частично или не скопирована вовсе."); //Выводим ошибку
                        }
                    }
                    if (copyCutMode && success) //Если вырезаем и успешно вставлено выше
                    {
                        try
                        {
                            Directory.Delete(copyBuffer); //Удаляем вырезаемую директорию
                            copyBuffer = GetCurrentFolderPath() + Path.GetFileName(copyBuffer); //Вставляем в буффер
                        }
                        catch
                        {

                        }
                        copyCutMode = false; //Теперь у нас мод копирования
                    }
                    pasted = true; //Вставили
                    pastedObjectName = GetCurrentFolderPath() + Path.GetFileName(copyBuffer); //Путь вставляемого объекта
                }
                else if (File.Exists(copyBuffer)) //Существует ли файл, который мы должны вставить. Ну мало ли
                {
                    if (File.Exists(GetCurrentFolderPath() + Path.GetFileName(copyBuffer))) //Существует ли уже такой файл
                    {
                        if (MessageBox.Show("Такой файл уже существует в данной папке, заменить?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes) //Диалоговое окно, надо ли заменить?
                        {
                            File.Copy(copyBuffer, GetCurrentFolderPath() + Path.GetFileName(copyBuffer), true); //Копирование
                        }
                        else return;
                    }
                    else
                    {
                        File.Copy(copyBuffer, GetCurrentFolderPath() + Path.GetFileName(copyBuffer), false); //Просто копирование без вопросов
                    }
                    if (copyCutMode) //Если вырезаем
                    {
                        try
                        {
                            File.Delete(copyBuffer); //Удаляем вырезанный файл

                            copyBuffer = GetCurrentFolderPath() + Path.GetFileName(copyBuffer); //Вставляем в буффер
                        }
                        catch
                        {

                        }
                        copyCutMode = false; //Теперь у нас мод копирования
                    }
                    pasted = true; //Вставили
                    pastedObjectName = GetCurrentFolderPath() + Path.GetFileName(copyBuffer);//Путь вставляемого объекта
                }
                else
                {
                    MessageBox.Show("Копируемый объект не существует"); //Объекта который копируем уже не существует
                    copied = false; //Выключаем кнопку вставить
                    copyBuffer = ""; //Очищаем буффер
                }
            }
            catch (Exception ex) //Ой, ошибочка. Кажется что-то пошло не так
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message);
                if (!(File.Exists(copyBuffer) || Directory.Exists(copyBuffer))) //Если файла или директории не существует
                {
                    copied = false; //Выключаем кнопку вставить
                    copyBuffer = ""; //Очищаем буффер
                }
            }
            UpdateList(); //Обновляем список файлов и папок
        }

        private void RenameButton_Click(object sender, EventArgs e) //Нажали на переименовывание в контекстном меню
        {
            RenameForm renameForm = new RenameForm(objectsInFolder[ObjectContainer.SelectedIndex], this); //Открываем форму переименовывания
            renameForm.Show(); //Показываем

        }

        private void DeleteButton_Click(object sender, EventArgs e) //Нажали на удаление в контекстном меню
        {
            if (File.Exists(objectsInFolder[ObjectContainer.SelectedIndex])) //Существует ли выделенный файл
            {
                File.Delete(objectsInFolder[ObjectContainer.SelectedIndex]); //Удаляем файл
                UpdateList(); //Обновляем список файлов и папок
            }
            else if (Directory.Exists(objectsInFolder[ObjectContainer.SelectedIndex])) //Существует ли выделенная папка
            {
                Directory.Delete(objectsInFolder[ObjectContainer.SelectedIndex], true); //Удаляем папку
                UpdateList(); //Обновляем список файлов и папок
            }
            else
            {
                MessageBox.Show("Данный объект уже не существует."); //Объекта уже не существует
                UpdateList();  //Обновляем список файлов и папок
            }
        }

        private void PathBox_KeyDown(object sender, KeyEventArgs e) //Эвент нажатия клавиши в строке пути
        {
            if (e.KeyCode == Keys.Enter) //Нажали на enter
            {
                if (PathBox.Text.Trim() == "") //Если пусть пустой, то мы идем в корневую директорию
                {
                    string folder = GetCurrentFolderPath(); //Получаем текущую папку
                    fragmentedPath.Clear(); //Очищаем фрагменты пути
                    UpdateList(); //Обновляем список файлов и папок
                    if (folder != GetCurrentFolderPath()) //Если текущий путь изменился, т.е. мы не были в той папке, куда переместились
                    {
                        history = folder; //Добавляем прошлую папку в историю
                        BackButton.Enabled = true; //Включаем кнопку назад
                        ForwardButton.Enabled = false; //Выключаем кнопку вперед
                    }
                }
                else if (File.Exists(PathBox.Text)) //Если файл по введенному пути существует
                {
                    Process openFile = new Process(); //Создаем объект класса процесс
                    openFile.StartInfo = new ProcessStartInfo() //Указываем параметры, файл откроется программой по умолчанию
                    {
                        UseShellExecute = true,
                        FileName = PathBox.Text
                    };
                    openFile.Start(); //Запускаем
                    ObjectContainer.Focus(); //Переводим фокус на панель с объектами
                }
                else if (Directory.Exists(PathBox.Text)) //Если папка по введенному пути существует
                {
                    string folder = GetCurrentFolderPath(); //Получаем текущую папку
                    fragmentedPath.Clear(); //Очищаем фрагменты пути
                    string[] fragments = PathBox.Text.Split('\\'); //Получаем новые фрагменты
                    for (int i = 0; i < fragments.Length - 1; i++) //Перебираем, все кроме последнего
                    {
                        fragmentedPath.Add(fragments[i]); //Добавляем в массив
                    }
                    if (fragments[fragments.Length - 1] != "") //Пользователь мог ввести в конце \, поэтому мы избавляемся от "пустой" папки 
                        fragmentedPath.Add(fragments[fragments.Length - 1]); //Добавляем в массив
                    ObjectContainer.Focus(); //Переводим фокус на панель с объектами
                    if (folder != GetCurrentFolderPath())  //Если текущий путь изменился, т.е. мы не были в той папке, куда переместились
                    {
                        history = folder; //Добавляем прошлую папку в историю
                        BackButton.Enabled = true; //Включаем кнопку назад
                        ForwardButton.Enabled = false; //Выключаем кнопку вперед
                    }
                    UpdateList(); //Обновляем список файлов и папок
                }
                else
                {
                    ObjectContainer.Focus(); //Переводим фокус на панель с объектами
                    MessageBox.Show("Объект не найден."); //Объект не нашелся
                }
            }
        }

        private void ReloadButton_Click(object sender, EventArgs e) //Нажали на кнопку обновления текущей директории
        {
            UpdateList(); //Обновляем список
        }

        private void BackButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку назад
        {
            if (history == "") //Если история пустая
            {
                string historyTemp = GetCurrentFolderPath(); //Получаем текущую папку
                fragmentedPath.Clear(); //Очищаем фрагменты пути
                UpdateList(); //Обновляем список
                history = historyTemp; //Записываем в историю папку, в которой были
                ForwardButton.Enabled = true; //Включаем кнопку вперед
                BackButton.Enabled = false; //Выключаем кнопку назад
            }
            else if (Directory.Exists(history)) //Если папка из истории существует
            {
                string historyTemp = GetCurrentFolderPath(); //Получаем текущую папку
                fragmentedPath.Clear(); //Очищаем фрагменты пути
                string[] fragments = history.Split('\\'); //Получаем новые фрагменты
                for (int i = 0; i < fragments.Length - 1; i++) //Перебираем, все кроме последнего
                {
                    fragmentedPath.Add(fragments[i]); //Добавляем в массив
                }
                if (fragments[fragments.Length - 1] != "") //Пользователь мог ввести в конце \, поэтому мы избавляемся от "пустой" папки 
                    fragmentedPath.Add(fragments[fragments.Length - 1]); //Добавляем в массив
                history = historyTemp; //Записываем в историю папку, в которой были
                ForwardButton.Enabled = true; //Включаем кнопку вперед
                BackButton.Enabled = false; //Выключаем кнопку назад
                UpdateList(); //Обновляем список
            }
            else
            {
                MessageBox.Show("Папки более не существует."); //Папки уже нет
                BackButton.Enabled = false; //Выключем кнопку назад
                ForwardButton.Enabled = false; //Выключем кнопку вперед
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e) //Эвент нажатия на кнопку вперед
        {
            if (history == "") //Если история пустая
            {
                string historyTemp = GetCurrentFolderPath(); //Получаем текущую папку
                fragmentedPath.Clear(); //Очищаем фрагменты пути
                UpdateList(); //Обновляем список
                history = historyTemp; //Записываем в историю папку, в которой были
                ForwardButton.Enabled = false; //Выключаем кнопку вперед
                BackButton.Enabled = true; //Включаем кнопку назад
            }
            else if (Directory.Exists(history)) //Если папка из истории существует
            {
                string historyTemp = GetCurrentFolderPath(); //Получаем текущую папку
                fragmentedPath.Clear(); //Очищаем фрагменты пути
                string[] fragments = history.Split('\\'); //Получаем новые фрагменты
                for (int i = 0; i < fragments.Length - 1; i++) //Перебираем, все кроме последнего
                {
                    fragmentedPath.Add(fragments[i]); //Добавляем в массив
                }
                if (fragments[fragments.Length - 1] != "") //Пользователь мог ввести в конце \, поэтому мы избавляемся от "пустой" папки 
                    fragmentedPath.Add(fragments[fragments.Length - 1]); //Добавляем в массив
                history = historyTemp; //Записываем в историю папку, в которой были
                ForwardButton.Enabled = false; //Выключаем кнопку вперед
                BackButton.Enabled = true; //Включаем кнопку назад
                UpdateList(); //Обновляем список
            }
            else
            {
                MessageBox.Show("Папки более не существует."); //Папки уже нет
                BackButton.Enabled = false; //Выключем кнопку назад
                ForwardButton.Enabled = false; //Выключем кнопку вперед
            }
        }

        private void ObjectContainer_MouseClick(object sender, MouseEventArgs e) //Нажатие на элемент в listBox
        {
            if (fragmentedPath.Count == 0 || ObjectContainer.SelectedIndex == 0 || ObjectContainer.SelectedIndex == -1) //Если фрагментов пути нет или выбранный элемент под индексом 0 (назад) или -1 (ничего не выбрано)
            {
                PropertiesPanel.Visible = false; //Прячем панель со свойствами
                return;
            }
            Rectangle itemZone = ObjectContainer.GetItemRectangle(ObjectContainer.SelectedIndex); //Прямоугольная зона элемента
            if (itemZone.Contains(e.Location)) //Если мы нажали на элемент (клик внутри зоны)
            {
                if (calculator != null) calculator.disabled = true; //Если мы создавали отдельный объект для высчитывания размера папки, при выборе другой папки предыдущий поток нужно остановить
                try
                {
                    if (File.Exists(objectsInFolder[ObjectContainer.SelectedIndex])) //Если это файл
                    {
                        InfoNameLabel.Text = Path.GetFileName(objectsInFolder[ObjectContainer.SelectedIndex]); //Выводим название файла
                        FileInfo fileInfo = new FileInfo(objectsInFolder[ObjectContainer.SelectedIndex]); //Получаем инфо о файле
                        InfoCreatedLabel.Text = fileInfo.CreationTime.ToString(); //Когда создали
                        InfoChangedLabel.Text = fileInfo.LastWriteTime.ToString(); //Когда изменяли
                        InfoOpenedLabel.Text = fileInfo.LastAccessTime.ToString(); //Когда открывали
                        InfoFullPathLabel.Text = objectsInFolder[ObjectContainer.SelectedIndex]; //Полный путь
                        long size = fileInfo.Length; //Размер
                        string unit = "Б"; //Б - байт, по умолчанию
                        if (size > 1024)
                        {
                            size /= 1024;
                            unit = "КБ"; //КБ - кбайт
                        }
                        if (size > 1024)
                        {
                            size /= 1024;
                            unit = "МБ"; //МБ - мбайт
                        }
                        if (size > 1024)
                        {
                            size /= 1024;
                            unit = "ГБ"; //ГБ - Гбайт
                        }
                        SizeLabel.Visible = true; //Показываем текст "Размер"
                        InfoSizeLabel.Visible = true; //Показываем текст с размером
                        InfoSizeLabel.Text = size.ToString() + " " + unit; //Выводим размер
                        PropertiesPanel.Visible = true; //Показываем панель
                    }
                    else if (Directory.Exists(objectsInFolder[ObjectContainer.SelectedIndex])) //Если это папка
                    {
                        string folder = objectsInFolder[ObjectContainer.SelectedIndex]; //Выбранная папка
                        InfoNameLabel.Text = Path.GetFileName(folder); //Выводим название папки
                        InfoCreatedLabel.Text = Directory.GetCreationTime(folder).ToString(); //Когда создали
                        InfoChangedLabel.Text = Directory.GetLastWriteTime(folder).ToString(); //Когда изменяли
                        InfoOpenedLabel.Text = Directory.GetLastAccessTime(folder).ToString(); //Когда открывали
                        InfoFullPathLabel.Text = folder; //Полный путь
                        calculator = new CalculateFolderSize(this); //Создаем объект нашего отдельного класса
                        SizeLabel.Visible = true; //Показываем текст "Размер"
                        InfoSizeLabel.Visible = true; //Показываем текст с размером
                        InfoSizeLabel.Text = "Вычисляю..."; //Выводим размер
                        Thread calculateSize = new Thread(() => calculator.CalculateSize(folder)); //Поток вычисления размера папки

                        calculateSize.Start(); //Включаем поток
                        PropertiesPanel.Visible = true; //Показываем панель
                    }
                    else
                    {
                        UpdateList(); //Обновляем список
                        PropertiesPanel.Visible = false; //Скрываем панель
                    }
                }
                catch //Что-то пошло не так
                {
                    UpdateList(); //Обновляем список
                    PropertiesPanel.Visible = false; //Скрываем панель
                }
            }
        }
        private bool CopyDirectory(string sourcePath, string destPath, bool replaceAll) //Копирование папки
        {
            bool success = true;  //Получилось ли вставить
            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath); //Если такой папки не существует, создаем
            string[] files = Directory.GetFiles(sourcePath); //Получаем файлы из папки, которую хотим скопировать
            foreach (string file in files) //Перебираем файлы
            {
                try
                {
                    File.Copy(file, destPath + "\\" + Path.GetFileName(file), replaceAll); //Копируем файл туда, куда мы перенесли папку
                }
                catch
                {
                    success = false; //Если что-то пошло не так, вставить не получилось
                }
            }
            string[] directories = Directory.GetDirectories(sourcePath); //Получаем папки внутри папки, которую хотим скопировать
            foreach (string directory in directories) //Перебираем папки
            {
                success = success && CopyDirectory(directory, destPath + "\\" + Path.GetFileName(directory), replaceAll); //Если у нас был хотя бы 1 фейл при переносе, мы конечно пробуем дальше, но предупреждение пользователю все равно выдаст
            }
            return success; //Возвращаем результат
        }

        private void DesktopButton_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //путь к рабочему столу
            string folder = GetCurrentFolderPath(); //Получаем текущую папку
            fragmentedPath.Clear(); //Очищаем фрагменты пути
            string[] fragments = desktop.Split('\\'); //Получаем новые фрагменты
            for (int i = 0; i < fragments.Length - 1; i++) //Перебираем, все кроме последнего
            {
                fragmentedPath.Add(fragments[i]); //Добавляем в массив
            }
            if (fragments[fragments.Length - 1] != "") //Пользователь мог ввести в конце \, поэтому мы избавляемся от "пустой" папки 
                fragmentedPath.Add(fragments[fragments.Length - 1]); //Добавляем в массив

            if (folder != GetCurrentFolderPath()) //Если текущий путь изменился, т.е. мы не были в той папке, куда переместились
            {
                history = folder; //Добавляем прошлую папку в историю
                BackButton.Enabled = true; //Включаем кнопку назад
                ForwardButton.Enabled = false; //Выключаем кнопку вперед
            }
            UpdateList(); //Обновляем список файлов и папок
        }

        private class CalculateFolderSize //Отдельный класс, для удобства
        {
            long size = 0; //Подсчитанный размер
            MainForm parent; //Обьект формы
            public bool disabled = false; //Если мы выбрали другую папку, мы меняем эту переменную на true и поток не выполняет свою работу далее
            Action act; //Действие
            bool haveErrors = false; //Возникли ли при выполнении потока ошибки
            string output; //Выходящие данные

            public CalculateFolderSize(MainForm parent) //Конструктор, на вход подается папка и форма
            {
                this.parent = parent; //Записываем в глобальную переменную объект формы
                act = () => parent.InfoSizeLabel.Text = output; //Создаем действие, которое записывает получившийся размер в label
            }

            public void CalculateSize(string folder) //Рекурсивная функция вычисления размера папки
            {
                if (disabled) return; //Если мы выбрали другую папку, поток не должен проводить вычисления
                try
                {
                    foreach (string filePath in Directory.GetFiles(folder)) //Перебираем файлы в папке
                    {
                        FileInfo fileInfo = new FileInfo(filePath); //Получаем инфо о файле
                        size += fileInfo.Length; //Складываем размер
                    }
                }
                catch
                {
                    haveErrors = true; //Возникли ошибки при высчитывании, нет какого-то доступа к некоторым файлам или файлы были удалены
                }
                string unit = "Б"; //Б - байт, по умолчанию
                long tempsize = size;
                if (tempsize > 1024)
                {
                    tempsize /= 1024;
                    unit = "КБ"; //КБ - кбайт
                }
                if (tempsize > 1024)
                {
                    tempsize /= 1024;
                    unit = "МБ"; //МБ - мбайт
                }
                if (tempsize > 1024)
                {
                    tempsize /= 1024;
                    unit = "ГБ"; //ГБ - Гбайт
                }
                output = tempsize.ToString() + " " + unit; //Получившийся размер
                if (haveErrors) output += " (Данные недостоверны: при вычислении возникли ошибки)"; //Если у нас были ошибки, данные о размере папки недостоверны
                if (disabled) return;  //Если мы выбрали другую папку, поток не должен проводить вычисления
                parent.Invoke(act); //Выводим размер через invoke (Выполнить в потоке, в котором находится объект)
                try
                {
                    foreach (string folderPath in Directory.GetDirectories(folder)) //Перебираем папки
                    {
                        CalculateSize(folderPath); //Вызываем этот же метод
                    }
                }
                catch
                {
                    haveErrors = true; //Возникли ошибки при высчитывании, нет какого-то доступа к некоторым папкам
                }
            }
        }
    }
}
