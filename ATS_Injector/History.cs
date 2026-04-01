using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace ATS_Injector
{
    public class History
    {
        private BindingList<LogEntry> _logEntries;
        private readonly string _filePath = Path.Combine(PopOutApp.GetATSFolder(), "log_data.json"); 

        internal static void InitGrid(DataGridView dataGridView1)
        {
            // Configure the grid for a clean look
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "Date",
                Name = "colDate",
                Width = 120
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Url",
                HeaderText = "URL",
                Name = "colUrl"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Results",
                HeaderText = "Results",
                Name = "colResults",
                Width = 100
            });
        }

        private void SaveData()
        {
            try
            {
                var json = JsonSerializer.Serialize(_logEntries);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}");
            }
        }

        private void LoadData(DataGridView dataGridView1)
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    var list = JsonSerializer.Deserialize<List<LogEntry>>(json);
                    _logEntries = new BindingList<LogEntry>(list ?? new List<LogEntry>());
                }
                catch
                {
                    _logEntries = new BindingList<LogEntry>();
                }
            }
            else
            {
                _logEntries = new BindingList<LogEntry>();
            }

            // Bind the list to the grid
            dataGridView1.DataSource = _logEntries;
        }
    }

    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Results { get; set; }
    }
}
