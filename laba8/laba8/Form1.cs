using System;
using System.Windows.Forms;

namespace laba8
{
    public partial class Form1 : Form
    {
        private double num1, num2, num3;
        private bool needSumm, needLCM;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem inputMenuItem;
        private ToolStripMenuItem calcMenuItem;
        private ToolStripMenuItem aboutMenuItem;

        public Form1()
        {
            // Не вызываем InitializeComponent(), если его нет
            // Создаем форму и меню вручную
            this.Text = "Главное окно";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateMenu();
        }

        private void CreateMenu()
        {
            // Создаем MenuStrip
            menuStrip1 = new MenuStrip();

            // Создаем пункты меню
            inputMenuItem = new ToolStripMenuItem("input");
            calcMenuItem = new ToolStripMenuItem("Calc");
            aboutMenuItem = new ToolStripMenuItem("About");

            // Добавляем пункты в меню
            menuStrip1.Items.Add(inputMenuItem);
            menuStrip1.Items.Add(calcMenuItem);
            menuStrip1.Items.Add(aboutMenuItem);

            // Добавляем MenuStrip на форму
            this.Controls.Add(menuStrip1);

            // Привязываем обработчики
            inputMenuItem.Click += InputMenuItem_Click;
            calcMenuItem.Click += CalcMenuItem_Click;
            aboutMenuItem.Click += AboutMenuItem_Click;
        }

        private void InputMenuItem_Click(object sender, EventArgs e)
        {
            using (InputDialog dialog = new InputDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    num1 = dialog.Number1;
                    num2 = dialog.Number2;
                    num3 = dialog.Number3;
                    needSumm = dialog.CalculateSumm;
                    needLCM = dialog.CalculateLeastMultiple;

                    MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CalcMenuItem_Click(object sender, EventArgs e)
        {
            string result = "";

            if (needSumm)
            {
                double sum = num1 + num2 + num3;
                result += $"Сумма: {sum}\n";
            }

            if (needLCM)
            {
                int lcm = CalculateLCM((int)num1, (int)num2);
                result += $"НОК ({num1}, {num2}): {lcm}\n";
            }

            if (string.IsNullOrEmpty(result))
            {
                result = "Ни один режим не выбран! Сначала выберите режим в меню input.";
            }

            MessageBox.Show(result, "Результаты вычислений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабораторная работа №8\nВариант 5\n\nПрограмма для вычисления:\n- Суммы трех чисел\n- НОК первых двух чисел",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int CalculateLCM(int a, int b)
        {
            return Math.Abs(a * b) / CalculateGCD(a, b);
        }

        private int CalculateGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }
    }
}