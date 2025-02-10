using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UserManagment.RefToUserService;

namespace UserManagment
{
    public partial class Form1 : Form
    {
        private UserServiceClient _client;
        private BindingSource _bindingSource;

        public Form1()
        {
            InitializeComponent();
            _client = new UserServiceClient();
            _bindingSource = new BindingSource();
            InitializeDataGridView();
            LoadUsers();
        }

        private void InitializeDataGridView()
        {
            dataGridViewUsers.AutoGenerateColumns = false;
            dataGridViewUsers.DataSource = _bindingSource;
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID"});
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "ПІБ" });
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email" });
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PhoneNumber", HeaderText = "Телефон" });
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TaxNumber", HeaderText = "ДРФО" });
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedDate", HeaderText = "Дата створення" });
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LastModifiedDate", HeaderText = "Дата зміни" });
        }

        private void LoadUsers()
        {
            try
            {
                var users = _client.GetAllUsers();
                label1.Text = $"Всього користувачів: {users.Count()}";
                label2.Text = $"Створених сьогодні: {users.Where(x=> x.CreatedDate == DateTime.Today).Count()}";

                _bindingSource.DataSource = new List<User>(users);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            using (var form = new UserForm(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _client.CreateUser(form.GetUser());
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EditUserButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow != null)
            {
                var user = (User)_bindingSource.Current;

                using (var form = new UserForm(user))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            _client.UpdateUser(form.GetUser());
                            LoadUsers();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

    }
}
