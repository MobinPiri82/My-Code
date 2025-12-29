using System;
using System.Windows.Forms;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp
{
    public partial class Form1 : Form
    {
        BookRepository repo = new BookRepository();
        int selectedBookId = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // 🔄 لود / رفرش داده‌ها
        private void LoadData()
        {
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = repo.GetAll();
        }

        // 🧹 پاک کردن فیلدها
        private void ClearInputs()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
            txtSearch.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("سال انتشار نامعتبر است ❌");
                return;
            }

            Book book = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                PublishYear = year
            };

            repo.Add(book);
            LoadData();
            ClearInputs();

            MessageBox.Show("کتاب اضافه شد ✅");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
            {
                MessageBox.Show("لطفاً یک کتاب انتخاب کنید ❌");
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("سال انتشار نامعتبر است ❌");
                return;
            }

            Book book = new Book
            {
                Id = selectedBookId,
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                PublishYear = year
            };

            repo.Update(book);
            LoadData();
            ClearInputs();
            selectedBookId = 0;

            MessageBox.Show("ویرایش انجام شد ✏️");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
            {
                MessageBox.Show("لطفاً یک کتاب انتخاب کنید ❌");
                return;
            }

            repo.Delete(selectedBookId);
            LoadData();
            ClearInputs();
            selectedBookId = 0;

            MessageBox.Show("کتاب حذف شد 🗑️");
        }
        // 🔍 جستجو با نام کتاب
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = repo.SearchByTitle(txtSearch.Text);
            dgvBooks.DataSource = result;

            MessageBox.Show(
                result.Count > 0
                ? "نتایج جستجو نمایش داده شد 🔍"
                : "کتابی پیدا نشد ❌"
            );
        }

        // 🖱️ کلیک روی Grid → پر شدن TextBox ها
        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvBooks.Rows[e.RowIndex];

                selectedBookId = Convert.ToInt32(row.Cells["Id"].Value);

                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtAuthor.Text = row.Cells["Author"].Value.ToString();
                txtYear.Text = row.Cells["PublishYear"].Value.ToString();
            }
        }

    }
}
