using System.Diagnostics;
using System.IO.Compression;
using System.Text;


namespace FileManager
{
    public class DirectoryView
    {
        private ListBox _listBox;
        private Label _label;

        public string CurrentPath { get; set; } = string.Empty;

        public DirectoryView(ListBox listbox,  Label label)
        {
            _label = label;
            _label.Text = CurrentPath;
            _listBox = listbox;
        }

        public void View()
        {
            var newPath = Path.Combine(CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty);
            if (File.Exists(newPath))
            {
                Process.Start(new ProcessStartInfo(newPath) {UseShellExecute = true});
            }
            if (Directory.Exists(newPath))
            {
                CurrentPath = newPath;
                Update();
            }
        }

        public void Delete()
        {
            var newPath = Path.Combine(CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            if (Directory.Exists(newPath))
            {
                Directory.Delete(newPath, true);
            }
            Update();
        }

        public void MoveTo(DirectoryView directoryView)
        {
            var sourcePath = Path.Combine(CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty);
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, Path.Combine(directoryView.CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty));
            }

            if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, Path.Combine(directoryView.CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty));
            }
            Update();
            directoryView.Update();
        }

        public void Archive()
        {
            var fileName = _listBox.SelectedItem.ToString();
            var sourcePath = Path.Combine(CurrentPath, fileName ?? string.Empty);
            if (File.Exists(sourcePath))
            {
                using var fileStream = new FileStream(@sourcePath, FileMode.Create, FileAccess.ReadWrite);
                using var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true, Encoding.UTF8);
                //var s
                archive.CreateEntryFromFile(sourcePath, fileName);
            }

            if (Directory.Exists(sourcePath))
            {
                ZipFile.CreateFromDirectory(sourcePath, Path.Combine(CurrentPath, $"{fileName}.zip"));
            }
            Update();
        }

        public void Copy(DirectoryView targetDirectoryView)
        {
            var sourcePath = Path.Combine(CurrentPath, _listBox.SelectedItem.ToString() ?? string.Empty);
            var targetPath = Path.Combine(targetDirectoryView.CurrentPath);
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, Path.Combine(targetPath, _listBox.SelectedItem.ToString() ?? string.Empty), true);
            }

            if (Directory.Exists(sourcePath))
            {
                CopyDirectory(sourcePath, targetPath, _listBox.SelectedItem.ToString());
            }
            targetDirectoryView.Update();
        }

        public void Update()
        {
            _listBox.Items.Clear();
            _label.Text = CurrentPath;
            if (CurrentPath == string.Empty)
            {
                var drives = DriveInfo.GetDrives();
                foreach (var drive in drives)
                    _listBox.Items.Add(drive.Name);
                return;
            }
            var directoryInfo = new DirectoryInfo(@CurrentPath);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                _listBox.Items.Add(directory.Name);
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                _listBox.Items.Add(file.Name);
            }
        }

        private void CopyDirectory(string sourcePath, string targetPath, string name)
        {
            var path = Path.Combine(targetPath, name);
            Directory.CreateDirectory(path);
            var directoryInfo = new DirectoryInfo(sourcePath);
            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                file.CopyTo(Path.Combine(path, file.Name));
            }
            var directories = directoryInfo.GetDirectories();
            foreach (var directory in directories)
            {
                CopyDirectory(Path.Combine(sourcePath, directory.Name), path, directory.Name);
            }
        }
    }
}
