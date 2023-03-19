namespace iCalGenerator
{
    public partial class Form1 : Form
    {
        private string _selectedScreenshotPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerateICal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedScreenshotPath))
            {
                var imagePath = _selectedScreenshotPath;
                var startDate = dateTimePickerStart.Value;
                var endDate = dateTimePickerEnd.Value;

                var weeklySchedule = new WeeklySchedule();
                var scheduleText = weeklySchedule.ExtractTextFromImage(imagePath);
                var scheduleItems = weeklySchedule.ParseScheduleText(scheduleText);

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "iCal files (*.ics)|*.ics",
                    DefaultExt = "ics",
                    FileName = "schedule.ics"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var outputFilePath = saveFileDialog.FileName;
                    weeklySchedule.CreateWeeklyICalFile(outputFilePath, scheduleItems, startDate, endDate);
                    MessageBox.Show("iCal file generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a screenshot first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectScreenshot_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
                Title = "Select a screenshot"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _selectedScreenshotPath = openFileDialog.FileName;
            }
        }
    }
}