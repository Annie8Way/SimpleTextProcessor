using System;
using System.Windows.Forms;
using System.Drawing;

namespace Text_Editor
{
    public partial class Form1 : Form
    {
        #region Инициализация и подготовка
        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectionIndent = 30;
            richTextBox1.SelectionHangingIndent = -10;
            richTextBox1.SelectionRightIndent = 20;
        }

        //Загружаем шрифты, размеры, цвет
        private void Form1_Load(object sender, EventArgs e)
        {
            //шрифты
            foreach (FontFamily font in FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }
            toolStripComboBox1.SelectedItem = "Times New Roman";

            //размеры
            string[] sizes = { "8", "10", "12", "14", "16", "18", "20", "22", "24", "26" };
            toolStripComboBox2.Items.AddRange(sizes);
            toolStripComboBox2.SelectedItem = "14";

            //цвет
            цветToolStripMenuItem.BackColor = Color.Black;
        }

        //Флажок для обнаружения изменений в документе: 
        private bool Changes = false;
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            Changes = true;
        }
        #endregion

        #region Операции с файлом

        //Сохранялка
        private void MenuFileSaveAs()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                Text = "Файл [" + saveFileDialog1.FileName + "]";
                Changes = false;
            }
            else
            {
                Changes = false;
            }
        }

        //Открывалка
        private void OpenFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
                catch (ArgumentException)
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }

                Text = "Файл [" + openFileDialog1.FileName + "]";
            }
        }

        #endregion

        #region Кнопочки

        #region Операции с файлом
        //Создать файл
        private void создатьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Changes)
            {
                MenuFileSaveAs();
            }
        }

        //Открыть файл
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Changes)
            {
                MenuFileSaveAs();
            }
            OpenFile();
        }

        //Сохранить как
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuFileSaveAs();
        }

        //Закрыть
        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Changes)
            {
                MenuFileSaveAs();
            }
            Close();
        }

        //Закрыть через крестик
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Changes)
            {
                MenuFileSaveAs();
            }
        }
        #endregion

        #region Правка

        //отменить
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        //вернуть
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        //вырезать
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        //копировать
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        //вставить
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        //Выделить всё
        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }



        #endregion

        #endregion

        #region Выбор шрифта и размера

        //Фиксация изменения шрифта или размера
        private void ToolStript_SizesChanged(object sender, EventArgs e)
        {
            try
            {
                //Стиль
                FontStyle currStyle = richTextBox1.SelectionFont.Style;
                richTextBox1.SelectionFont = new Font(toolStripComboBox1.Text, Convert.ToInt32(toolStripComboBox2.Text), currStyle);
                richTextBox1.Refresh();
            }
            catch { }
        }

        #endregion

        #region Выбор цвета
        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
                цветToolStripMenuItem.BackColor = colorDialog1.Color;
            }
        }
        #endregion

        #region Выбор стиля символов

            #region Жирный

        private void ЖToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font currFont = richTextBox1.SelectionFont;
            FontStyle newStyle;

            try
            {
                if (currFont.Style == FontStyle.Bold)
                {
                    newStyle = FontStyle.Regular;
                }
                else
                {
                    newStyle = currFont.Style | FontStyle.Bold;
                }

                richTextBox1.SelectionFont = new Font(currFont.FontFamily, currFont.Size, newStyle);
                CheckStyle();
            }
            catch { }
        }
        #endregion

            #region Курсив

        private void IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font currFont = richTextBox1.SelectionFont;
            FontStyle newStyle;

            try
            {
                if (currFont.Style == FontStyle.Italic)
                {
                    newStyle = FontStyle.Regular;
                }
                else
                {
                    newStyle = currFont.Style | FontStyle.Italic;
                }

                richTextBox1.SelectionFont = new Font(currFont.FontFamily, currFont.Size, newStyle);
                CheckStyle();
            }
            catch { }
        }
        #endregion

            #region Подчёркнутый
        private void LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font currFont = richTextBox1.SelectionFont;
            FontStyle newStyle;

            try
            {
                if (currFont.Style == FontStyle.Underline)
                {
                    newStyle = FontStyle.Regular;
                }
                else
                {
                    newStyle = currFont.Style | FontStyle.Underline;
                }

                richTextBox1.SelectionFont = new Font(currFont.FontFamily, currFont.Size, newStyle);
                CheckStyle();
            }
            catch { }
        }
        #endregion

            #region Зачёркнутый
        private void SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font currFont = richTextBox1.SelectionFont;
            FontStyle newStyle;

            try
            {
                if (richTextBox1.SelectionFont.Strikeout == true)
                {
                    newStyle = FontStyle.Regular;
                }
                else
                {
                    newStyle = currFont.Style | FontStyle.Strikeout;
                }

                richTextBox1.SelectionFont = new Font(currFont.FontFamily, currFont.Size, newStyle);
                CheckStyle();
            }
            catch { }
        }
        #endregion

            #region Галочки стилей

        private void CheckStyle()
        {
			if (richTextBox1.SelectionFont.Bold == true)
			{
				жToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
				жToolStripMenuItem.Checked = true;
			}
			else
			{
				жToolStripMenuItem.Checked = false;
				жToolStripMenuItem.BackColor = SystemColors.Control;
			}
			//курсив
			if (richTextBox1.SelectionFont.Italic == true)
			{
				iToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
				iToolStripMenuItem.Checked = true;
			}
			else
			{
				iToolStripMenuItem.Checked = false;
				iToolStripMenuItem.BackColor = SystemColors.Control;
			}
			//подчёркнутый
			if (richTextBox1.SelectionFont.Underline == true)
			{
				lToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
				lToolStripMenuItem.Checked = true;
			}
			else
			{
				lToolStripMenuItem.Checked = false;
				lToolStripMenuItem.BackColor = SystemColors.Control;
			}
			//зачёркнутый
			if (richTextBox1.SelectionFont.Strikeout == true)
			{
				sToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
				sToolStripMenuItem.Checked = true;
			}
			else
			{
				sToolStripMenuItem.Checked = false;
				sToolStripMenuItem.BackColor = SystemColors.Control;
			}
        }

        //Делаем так, чтобы при клике на текст, нам отмечался стиль этого текста
        //Ставим на MouseCaptureChanged в RichBox
        private void CheckActive(object sender, EventArgs e)
        {
            CheckStyle();
            CheckAlignment();
            toolStripComboBox2.SelectedItem = richTextBox1.SelectionFont.Size.ToString();
        }


        #endregion

            #region Очистка стилей
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font currFont = richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new Font(currFont.FontFamily, currFont.Size, FontStyle.Regular);
            CheckStyle();
        }
        #endregion

        #endregion

        #region Выравнивание текста
        private void LToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        //И любимые галочки
        private void CheckAlignment()
        {
            //слева
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Left)
            {
                lToolStripMenuItem1.BackColor = SystemColors.GradientActiveCaption;
            }
            else
            {
                lToolStripMenuItem1.BackColor = SystemColors.Control;
            }
            //по центру
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Center)
            {
                cToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
            }
            else
            {
                cToolStripMenuItem.BackColor = SystemColors.Control;
            }
            //справа
            if(richTextBox1.SelectionAlignment == HorizontalAlignment.Right)
            {
                rToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
            }
            else
            {
                rToolStripMenuItem.BackColor = SystemColors.Control;
            }
        }

        #endregion

    }
}
