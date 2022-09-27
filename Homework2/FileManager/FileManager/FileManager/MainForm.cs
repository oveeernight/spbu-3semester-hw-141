using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace FileManager;

public partial class MainForm : Form
{
    private DirectoryView _leftDirectoryView;
    private DirectoryView _rightDirectoryView;
    private UI _gui;

    public MainForm()
    {
        KeyPreview = true;
        InitializeComponent();
        var deserializer = new XmlSerializer(typeof(UI));
        using var reader = new StreamReader(@"gui.xml");
        _gui = (UI)deserializer.Deserialize(reader);
        reader.Close();
        SetFonts(_gui.Font);
        ChangeTheme(_gui.Theme);
        comboBoxFont.Text = _gui.Font;
        comboBoxColor.Text = _gui.Theme;
        comboBoxFont.Items.AddRange(_gui.Fonts);
        comboBoxColor.Items.AddRange(_gui.Themes);
        var drives = DriveInfo.GetDrives();
        foreach (var drive in drives)
        {
            listBoxLeft.Items.Add(drive.Name);
            listBoxRight.Items.Add(drive.Name);
        }

        _leftDirectoryView = new DirectoryView(listBoxLeft, labelLeft);
        _rightDirectoryView = new DirectoryView(listBoxRight, labelRight);
    }

    private void SetFonts(string font)
    {
        ResetGui(_gui.Theme, font);
        buttonSearch.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonCopy.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonDownload.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonDelete.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonEdit.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonLeftBack.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonMove.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonView.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonRightBack.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonLeftBack.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        buttonArchiving.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        labelLeft.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        labelRight.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        label1.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        label2.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        listBoxRight.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        listBoxLeft.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        comboBoxFont.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
        comboBoxColor.Font = new Font(font, 9F, FontStyle.Regular, GraphicsUnit.Point);
    }

    private void ChangeForeColor(int red, int green, int blue)
    {
        var color = red + green + blue < 500 ? Color.White : Color.Black;
        buttonSearch.ForeColor = color;
        buttonCopy.ForeColor = color;
        buttonDelete.ForeColor = color;
        buttonEdit.ForeColor = color;
        buttonLeftBack.ForeColor = color;
        buttonDownload.ForeColor = color;
        buttonMove.ForeColor = color;
        buttonView.ForeColor = color;
        buttonRightBack.ForeColor = color;
        buttonLeftBack.ForeColor = color;
        buttonArchiving.ForeColor = color;
        labelLeft.ForeColor = color;
        labelRight.ForeColor = color;
        listBoxRight.ForeColor = color;
        listBoxLeft.ForeColor = color;
        label1.ForeColor = color;
        label2.ForeColor = color;
        comboBoxColor.ForeColor = color;
        comboBoxFont.ForeColor = color;
    }

    private void ChangeTheme(string theme)
    {
        int red, green, blue;
        (red, green, blue) = theme switch
        {
            "White" => (255, 255, 255),
            "Material Ocean" => (60, 105, 120),
            "Night Owl" => (95, 75, 120)
        };
        comboBoxColor.BackColor = Color.FromArgb(red - 10, green, blue);
        comboBoxFont.BackColor = Color.FromArgb(red - 10, green, blue);
        BackColor = Color.FromArgb(red, green, blue);
        buttonSearch.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonDownload.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonCopy.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonDelete.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonView.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonMove.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonLeftBack.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonRightBack.BackColor = Color.FromArgb(red - 10, green, blue);
        buttonArchiving.BackColor = Color.FromArgb(red -10, green, blue);
        buttonEdit.BackColor = Color.FromArgb(red - 10, green, blue);
        listBoxRight.BackColor = Color.FromArgb(red- 30, green -10, blue);
        listBoxLeft.BackColor = Color.FromArgb(red - 30, green -10, blue);
        label1.BackColor = Color.FromArgb(red, green, blue);
        label2.BackColor = Color.FromArgb(red, green, blue);
        comboBoxColor.BackColor = Color.FromArgb(red, green, blue);
        comboBoxFont.BackColor = Color.FromArgb(red, green, blue);
        ChangeForeColor(red, green, blue);
    }



private void MainForm_KeyDown_View(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F4)
        {
            if (listBoxLeft.SelectedItem != null)
            {
                _leftDirectoryView.View();
            }
            else if (listBoxRight.SelectedItem != null)
            {
                _rightDirectoryView.View();
            }
        }
    }

    private void MainForm_KeyDown_Delete(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F8)
        {
            if (listBoxLeft.SelectedItem != null)
            {
                _leftDirectoryView.Delete();
            }
            else if (listBoxRight.SelectedItem != null)
            {
                _rightDirectoryView.Delete();
            }
        }
    }

    private void MainForm_KeyDown_Move(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F6)
        {
            if (listBoxLeft.SelectedItem != null)
            {
                _leftDirectoryView.MoveTo(_rightDirectoryView);
            }
            else if (listBoxRight.SelectedItem != null)
            {
                _rightDirectoryView.MoveTo(_leftDirectoryView);
            }
        }
    }

    private void MainForm_KeyDown_Archiving(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F10)
        {
            if (listBoxLeft.SelectedItem != null)
            {
                _leftDirectoryView.Archive();
            }
            else if (listBoxRight.SelectedItem != null)
            {
                _rightDirectoryView.Archive();
            }
        }
    }

    private void MainForm_KeyDown_Copy(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F7)
        {
            if (listBoxLeft.SelectedItem != null)
            {
                _leftDirectoryView.Copy(_rightDirectoryView);
            }
            else if (listBoxRight.SelectedItem != null)
            {
                _rightDirectoryView.Copy(_leftDirectoryView);
            }
        }
    }

    private void buttonLeftBack_Click(object sender, EventArgs e)
    {
        if (_leftDirectoryView.CurrentPath.Length == 3)
        {
            _leftDirectoryView.CurrentPath = string.Empty;
        }
        else if (_leftDirectoryView.CurrentPath.LastIndexOf('\\') == 2)
        {
            _leftDirectoryView.CurrentPath =
                _leftDirectoryView.CurrentPath[..(_leftDirectoryView.CurrentPath.LastIndexOf('\\') + 1)];
        }
        else
        {
            if (labelLeft.Text.Length == 0)
            {
                _leftDirectoryView.Update();
                return;
            }
            _leftDirectoryView.CurrentPath =
                _leftDirectoryView.CurrentPath[.._leftDirectoryView.CurrentPath.LastIndexOf('\\')];
        }
        _leftDirectoryView.Update();
    }

    private void buttonRightBack_Click(object sender, EventArgs e)
    {
        if (_rightDirectoryView.CurrentPath.Length == 3)
        {
            _rightDirectoryView.CurrentPath = string.Empty;
        }
        else if (_rightDirectoryView.CurrentPath.LastIndexOf('\\') == 2)
        {
            _rightDirectoryView.CurrentPath =
                _rightDirectoryView.CurrentPath[..(_rightDirectoryView.CurrentPath.LastIndexOf('\\')+1)];
        }
        else
        {
            if (labelRight.Text.Length == 0)
            {
                _rightDirectoryView.Update();
                return;
            }
            _rightDirectoryView.CurrentPath =
                _rightDirectoryView.CurrentPath[.._rightDirectoryView.CurrentPath.LastIndexOf('\\')];
        }
        _rightDirectoryView.Update();
    }

    private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxColor.SelectedItem.ToString() == "Night Owl")
        {
            ChangeTheme("Night Owl");
            ResetGui("Night Owl", _gui.Font);
        }
        else if (comboBoxColor.SelectedItem.ToString() == "Material Ocean")
        {
            ChangeTheme("Material Ocean");
            ResetGui("Material Ocean", _gui.Font);
        }
        else if (comboBoxColor.SelectedItem.ToString() == "White")
        {
            ChangeTheme("White");
            ResetGui("White", _gui.Font);
        }
    }

    private void ResetGui(string theme, string font)
    {
        _gui.Theme = theme;
        _gui.Font = font;
        var newGui = new UI() { Theme = theme, Font = font };
        var serializer = new XmlSerializer(typeof(UI));
        using var tw = new StreamWriter(@"gui.xml");
        serializer.Serialize(tw, newGui);
        tw.Close();
    }

    private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFonts(comboBoxFont.SelectedItem.ToString());
    }

    private void buttonAnalyze_Click(object sender, EventArgs e)
    {
        var path = textBoxAnalyze.Text;
        var words = File.ReadAllText(path).Split(',', '-', ' ', '.', ':')
            .AsParallel()
            .Where(word => word.Length > 5)
            .Select(word => word.ToLower())
            .AsParallel()
            .GroupBy(w => w)
            .OrderByDescending(word => word.Count())
            .ToArray()
            .Select(w => w.Key)
            .ToArray();

        var mostFrequent = words.Take(10).ToArray();
        var message = new StringBuilder();
        message.Append($"количество слов: {File.ReadAllText(path).Length}\n");
        message.Append($"количество строк: {File.ReadAllLines(path).Length}\n");
        message.Append($"10 слов, которые встречаются чаще всего \n");
        foreach (var word in mostFrequent)
        {
            message.Append($"{word} \n");
        }
        MessageBox.Show(message.ToString());

    }
}