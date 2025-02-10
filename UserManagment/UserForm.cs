using System;

using System.Drawing;
using System.Windows.Forms;
using UserManagment.RefToUserService;

namespace UserManagment
{
    public partial class UserForm : Form
    {
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtTaxNumber;
        private Button btnSave;
        private Button btnCancel;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblTaxNumber;

        private User _user;
        private User _validatedUser;

        private BindingSource _bindingSource = new BindingSource();

        public UserForm()
        {
            InitializeComponents();

            _user = new User 
            {
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            Text = "Додати нового користувача";
        }

        public UserForm(User user)
        {
            _user = user;

            InitializeComponents();
            LoadUserData();

            Text = "Редагувати користувача";
        }

        public User GetUser()
        {
            if (_validatedUser == null)
            {
                throw new InvalidOperationException("Попытка получить данные пользователя без валидации");
            }
            return _validatedUser;
        }

        private void InitializeComponents()
        {
            // [Код InitializeComponents залишається без змін]
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            lblFullName = new Label { Text = "ПІБ:", Location = new Point(20, 20) };
            lblEmail = new Label { Text = "Email:", Location = new Point(20, 60) };
            lblPhone = new Label { Text = "Телефон:", Location = new Point(20, 100) };
            lblTaxNumber = new Label { Text = "ДРФО:", Location = new Point(20, 140) };

            txtFullName = new TextBox { Location = new Point(120, 20), Width = 200 };
            txtEmail = new TextBox { Location = new Point(120, 60), Width = 200 };
            txtPhone = new TextBox { Location = new Point(120, 100), Width = 200 };
            txtTaxNumber = new TextBox { Location = new Point(120, 140), Width = 200 };

            btnSave = new Button
            {
                Text = "Зберегти",
                DialogResult = DialogResult.OK,
                Location = new Point(120, 200)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Скасувати",
                DialogResult = DialogResult.Cancel,
                Location = new Point(220, 200)
            };

            Controls.AddRange(new Control[] {
                lblFullName, lblEmail, lblPhone, lblTaxNumber,
                txtFullName, txtEmail, txtPhone, txtTaxNumber,
                btnSave, btnCancel
            });
        }

        private void LoadUserData()
        {
            if (_user != null)
            {
                txtFullName.Text = _user.FullName;
                txtEmail.Text = _user.Email;
                txtPhone.Text = _user.PhoneNumber;
                txtTaxNumber.Text = _user.TaxNumber;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                _validatedUser = new User
                {
                    FullName = txtFullName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    TaxNumber = txtTaxNumber.Text,
                    LastModifiedDate = DateTime.Now,
                    CreatedDate = _user?.CreatedDate ?? DateTime.Now
                };

                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("ПІБ є обов'язковим полем", "Помилка валідації",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Введіть коректний email", "Помилка валідації",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTaxNumber.Text))
            {
                MessageBox.Show("ДРФО є обов'язковим полем", "Помилка валідації",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
