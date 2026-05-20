using System;
using System.Windows.Forms;

namespace laba8
{
    public partial class InputDialog : Form
    {
        // Поля для хранения введенных данных
        public double Number1 { get; private set; }
        public double Number2 { get; private set; }
        public double Number3 { get; private set; }
        public bool CalculateSumm { get; private set; }
        public bool CalculateLeastMultiple { get; private set; }

        // Элементы управления (их создаст конструктор)
        private TextBox txtNumber1;
        private TextBox txtNumber2;
        private TextBox txtNumber3;
        private CheckBox chkSumm;
        private CheckBox chkLeastMultiple;
        private Button btnOK;
        private Label lblNumber1;
        private Label lblNumber2;
        private Label lblNumber3;

        public InputDialog()
        {
            // Создаем элементы управления вручную (чтобы избежать ошибок конструктора)
            InitializeComponentsManually();
        }

        private void InitializeComponentsManually()
        {
            // Создаем форму
            this.Text = "Ввод данных";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(300, 250);

            // Label Number 1
            lblNumber1 = new Label();
            lblNumber1.Text = "Number 1:";
            lblNumber1.Location = new System.Drawing.Point(20, 20);
            lblNumber1.Size = new System.Drawing.Size(70, 25);

            // TextBox Number 1
            txtNumber1 = new TextBox();
            txtNumber1.Location = new System.Drawing.Point(100, 20);
            txtNumber1.Size = new System.Drawing.Size(150, 20);

            // Label Number 2
            lblNumber2 = new Label();
            lblNumber2.Text = "Number 2:";
            lblNumber2.Location = new System.Drawing.Point(20, 55);
            lblNumber2.Size = new System.Drawing.Size(70, 25);

            // TextBox Number 2
            txtNumber2 = new TextBox();
            txtNumber2.Location = new System.Drawing.Point(100, 55);
            txtNumber2.Size = new System.Drawing.Size(150, 20);

            // Label Number 3
            lblNumber3 = new Label();
            lblNumber3.Text = "Number 3:";
            lblNumber3.Location = new System.Drawing.Point(20, 90);
            lblNumber3.Size = new System.Drawing.Size(70, 25);

            // TextBox Number 3
            txtNumber3 = new TextBox();
            txtNumber3.Location = new System.Drawing.Point(100, 90);
            txtNumber3.Size = new System.Drawing.Size(150, 20);

            // CheckBox Summ
            chkSumm = new CheckBox();
            chkSumm.Text = "Summ";
            chkSumm.Location = new System.Drawing.Point(20, 130);
            chkSumm.Size = new System.Drawing.Size(80, 25);

            // CheckBox Least multiple
            chkLeastMultiple = new CheckBox();
            chkLeastMultiple.Text = "Least multiple";
            chkLeastMultiple.Location = new System.Drawing.Point(20, 160);
            chkLeastMultiple.Size = new System.Drawing.Size(120, 25);

            // Button OK
            btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Location = new System.Drawing.Point(100, 200);
            btnOK.Size = new System.Drawing.Size(80, 30);
            btnOK.Click += BtnOK_Click;

            // Добавляем все элементы на форму
            this.Controls.Add(lblNumber1);
            this.Controls.Add(txtNumber1);
            this.Controls.Add(lblNumber2);
            this.Controls.Add(txtNumber2);
            this.Controls.Add(lblNumber3);
            this.Controls.Add(txtNumber3);
            this.Controls.Add(chkSumm);
            this.Controls.Add(chkLeastMultiple);
            this.Controls.Add(btnOK);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Проверяем, что поля не пустые и содержат числа
            if (double.TryParse(txtNumber1.Text, out double n1) &&
                double.TryParse(txtNumber2.Text, out double n2) &&
                double.TryParse(txtNumber3.Text, out double n3))
            {
                Number1 = n1;
                Number2 = n2;
                Number3 = n3;
                CalculateSumm = chkSumm.Checked;
                CalculateLeastMultiple = chkLeastMultiple.Checked;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные числа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}