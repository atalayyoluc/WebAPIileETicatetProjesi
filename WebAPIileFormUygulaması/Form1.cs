using Entities.DTOS.UserDTOs;
using Entities.Enum;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace WebAPIileFormUygulaması
{
    public partial class Form1 : Form
    {
        #region Defines
        private string Url = "http://localhost:5263/api/";
        private int selectedID = 0;
        #endregion
        #region Form1
        public Form1()
        {
            InitializeComponent();
        }
         



        private async void Form1_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            await DataGirdViewFill();
            CmbGenderFill();
        }
        #endregion
        #region Methods

        private void CmbGenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender()
            {
                Id = 1,
                Name="Erkek"
            });genders.Add(new Gender()
            {
                Id = 2,
                Name="Kadın"
            });
            comboBox1.DataSource= genders;
            comboBox1.DisplayMember= "Name";
            comboBox1.ValueMember = "Id";
        }
        class Gender
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
        private async Task DataGirdViewFill()
        {
            using (HttpClient client = new HttpClient())
            {
                var users = await client.GetFromJsonAsync<List<UserDetailDto>>(new Uri(Url + "User/GetList"));
                dataGridView1.DataSource = users;
            }
        }

        void ClearForm()
        {
            txtUserName.Text = String.Empty;
            txtUserFirstName.Text = String.Empty;
            txtUserLastName.Text = String.Empty;
            txtUserAdress.Text = String.Empty;
            txtUserPassword.Text = String.Empty;
            txtUserEmail.Text = String.Empty;
            dtpDateOfBirth.Value = DateTime.Now;
            comboBox1.SelectedValue = 0;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        #endregion

        #region Crud
        private async void btnAdd_Click(object sender, EventArgs e)
        {

            using (HttpClient client = new HttpClient())
            {
              
                
                UserAddDTO userAddDTO = new UserAddDTO()
                {
                    FirstName = txtUserFirstName.Text,
                    LastName = txtUserLastName.Text,
                    UserName = txtUserName.Text,
                    Password = txtUserPassword.Text,
                    Email = txtUserEmail.Text,
                    Address = txtUserAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDateOfBirth.Text),
                    Gender = comboBox1.Text == "Erkek" ? true : false,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(Url + "User/Add", userAddDTO);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    await DataGirdViewFill();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Ekleme İşlemi Başarısız");
                    ClearForm();
                }
            }
        }

   

        private async void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            using (HttpClient client = new HttpClient())
            {
                var user = await client.GetFromJsonAsync<UserDto>(Url + "User/GetById/" + selectedID);

                txtUserName.Text = user.UserName;
                txtUserFirstName.Text = user.FirstName;
                txtUserLastName.Text = user.LastName;
                txtUserAdress.Text = user.Address;
                txtUserPassword.Text = null;
                txtUserEmail.Text = user.Email;
                dtpDateOfBirth.Value = user.DateOfBirth;
                comboBox1.SelectedValue = user.Gender == true ? 1 : 2;   
            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {


                UserUpdateDto userUpdateDto = new UserUpdateDto()
                {
                    Id = selectedID,
                    FirstName = txtUserFirstName.Text,
                    LastName = txtUserLastName.Text,
                    UserName = txtUserName.Text,
                    Email = txtUserEmail.Text,
                    Address = txtUserAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDateOfBirth.Text),
                    Gender = comboBox1.Text == "Erkek" ? true : false,
                    Password= txtUserPassword.Text,
                };
                HttpResponseMessage response = await client.PutAsJsonAsync(Url + "User/Update", userUpdateDto);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Düzenleme İşlemi Başarılı");
                    await DataGirdViewFill();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Düzenleme İşlemi Başarısız");
                    ClearForm();
                }
            }


        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(Url + "User/Delete/" + selectedID);
                    if (response.IsSuccessStatusCode)
                     {
                    MessageBox.Show("Silme İşlemi Başarılı");
                    await DataGirdViewFill();
                    ClearForm();
                     }
                else
                {
                    MessageBox.Show("Silme İşlemi Başarısız");
                }
            }

            }
        #endregion
    }
}