using System.Text.Json;
using System.Xml.Serialization;

namespace FileManager
{
    public partial class AdmissionForm : Form
    {
        private AuthCredits _authorizationFields;
        private string? _login, _password;
        public AdmissionForm()
        {
            KeyPreview = true;
            InitializeComponent();
            textBoxPassword.PasswordChar = '*';
            using var fs = new FileStream("authorization.json", FileMode.OpenOrCreate);
            _authorizationFields = JsonSerializer.Deserialize<AuthCredits>(fs);
        }

        private void ButtonSubmit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // var newGui = new GUI();
                // var serializer = new XmlSerializer(typeof(GUI));
                // using var tw = new StreamWriter(@"E:\fm\gui.xml");
                // serializer.Serialize(tw, newGui);
                // tw.Close();
                if (textBoxLogin.Text == _authorizationFields.Login &&
                    textBoxPassword.Text == _authorizationFields.Password)
                {
                    Hide();
                    new MainForm().Show();
                }
            }
        }
    }
}