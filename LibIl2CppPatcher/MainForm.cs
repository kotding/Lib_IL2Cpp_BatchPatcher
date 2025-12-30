using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibIl2CppPatcher
{
    public partial class MainForm : Form
    {
        private byte[]? fileData;
        private string? currentFilePath;
        private DataGridView patchGrid;
        private Button openButton;
        private Button saveButton;
        private Button patchAllButton;
        private Button addRowButton;
        private Button deleteRowButton;
        private ComboBox architectureComboBox;
        private Label statusLabel;
        private Label fileInfoLabel;
        private Dictionary<string, Dictionary<string, string>> presetCodesByArch;

        public MainForm()
        {
            InitializePresetCodes();
            InitializeComponent();
        }

        private void InitializePresetCodes()
        {
            presetCodesByArch = new Dictionary<string, Dictionary<string, string>>
            {
                ["ARMv7"] = new Dictionary<string, string>
                {
                    ["Value 1 (True)"] = "01 00 A0 E3 1E FF 2F E1",
                    ["Value 0 (False)"] = "00 00 A0 E3 1E FF 2F E1",
                    ["1M"] = "01 04 A0 E3 1E FF 2F E1",
                    ["10M"] = "01 02 A0 E3 1E FF 2F E1",
                    ["Number 2"] = "02 00 A0 E3 1E FF 2F E1",
                    ["Float 40"] = "20 02 44 E3 1E FF 2F E1",
                    ["NOP"] = "00 F0 20 E3 1E FF 2F E1",
                    ["Number 7"] = "07 00 A0 E3 1E FF 2F E1",
                    ["Number 10"] = "0A 00 A0 E3 1E FF 2F E1",
                    ["Number 15"] = "0F 00 A0 E3 1E FF 2F E1",
                    ["Number 16"] = "10 00 A0 E3 1E FF 2F E1",
                    ["Number 17"] = "11 00 A0 E3 1E FF 2F E1",
                    ["Number 50"] = "37 00 A0 E3 1E FF 2F E1",
                    ["Number 255"] = "FF 00 A0 E3 1E FF 2F E1",
                    ["12 Million"] = "12 07 80 E3 1E FF 2F E1",
                    ["4060"] = "DC 0F 00 E3 1E FF 2F E1",
                    ["12000"] = "DC OF OF E3 1E FF 2F E1",
                    ["10000"] = "01 0A A0 E3 1E FF 2F E1",
                    ["1000"] = "01 08 A0 E3 1E FF 2F E1",
                    ["Speed Hack"] = "C2 0A 64 60 00 00 00 02",
                    ["Max Int"] = "02 01 E0 E3 1E FF 2F E1",
                    ["2,130,706,432"] = "7F 04 E0 E3 1E FF 2F E1",
                    ["999,999,999"] = "FF 09 0C E3 00 10 A0 E3 9A 0B 43 E3 1E FF 2F E1",
                    ["Fire Rate"] = "0E 00 A0 E3 1E FF 2F E1",
                    ["Float 0"] = "00 00 40 E3 1E FF 2F E1",
                    ["Float 1"] = "80 0F 43 E3 1E FF 2F E1",
                    ["Float 2"] = "00 00 44 E3 1E FF 2F E1",
                    ["Float 3"] = "40 00 44 E3 1E FF 2E E1",
                    ["Float 4"] = "80 00 44 E3 1E FF 2E E1",
                    ["Float 5"] = "A0 00 44 E3 1E FF 2E E1",
                    ["Float 10"] = "20 01 04 E3 1E FF 2F E1",
                    ["Float 69"] = "51 00 44 E3 1E FF 2F E1",
                    ["Float 99"] = "C6 02 44 E3 1E FF 2F E1",
                    ["Float 99.9"] = "C7 02 44 E3 1F FF 2F E1",
                    ["Float 100"] = "C8 02 44 E3 1E FF 2F E1",
                    ["Float 150"] = "16 03 44 E3 1E FF 2F E1",
                    ["Float 200"] = "48 03 44 E3 1E FF 2F E1",
                    ["Float 300"] = "96 03 44 E3 1E FF 2F E1",
                    ["Float 400"] = "C8 03 44 E3 1E FF 2F E1",
                    ["Float 500"] = "FA 03 04 E3 1E FF 2F E1",
                    ["Float 999"] = "7A 04 04 E3 1E FF 2F E1",
                    ["Float 999.9"] = "79 04 44 E3 1E FF 2F E1",
                    ["Float 1000"] = "7A 04 44 E3 1E FF 2F E1",
                    ["Float 2000"] = "FA 04 04 E3 1E FF 2F E1",
                    ["9,999"] = "0F 07 02 E3 1E FF 2F E1",
                    ["Strings"] = "74 70 80 E5 1E FF 2F E1",
                    ["Freeze Int"] = "E7 03 00 E3 1E FF 2F E1",
                    ["Freeze Float"] = "7A 04 04 E3 1E FF 2F E1",
                    ["Custom"] = ""
                },
                ["ARM64"] = new Dictionary<string, string>
                {
                    ["Value 1 (True)"] = "20 00 80 D2 C0 03 5F D6",
                    ["Value 0 (False)"] = "00 00 80 D2 C0 03 5F D6",
                    ["Number 2"] = "40 00 80 D2 C0 03 5F D6",
                    ["Number 7"] = "E0 00 80 D2 C0 03 5F D6",
                    ["Number 10"] = "40 01 80 D2 C0 03 5F D6",
                    ["Number 15"] = "E0 01 80 D2 C0 03 5F D6",
                    ["Number 16"] = "00 02 80 D2 C0 03 5F D6",
                    ["Number 17"] = "20 02 80 D2 C0 03 5F D6",
                    ["Number 50"] = "40 06 80 D2 C0 03 5F D6",
                    ["Number 100"] = "80 0C 80 D2 C0 03 5F D6",
                    ["Number 255"] = "E0 1F 80 D2 C0 03 5F D6",
                    ["1000"] = "80 7D 80 D2 C0 03 5F D6",
                    ["10000"] = "00 71 82 D2 C0 03 5F D6",
                    ["100000"] = "00 0D 86 D2 C0 03 5F D6",
                    ["1M"] = "00 40 89 D2 C0 03 5F D6",
                    ["10M"] = "00 80 95 D2 C0 03 5F D6",
                    ["999,999"] = "E0 3F 8F D2 C0 03 5F D6",
                    ["9,999,999"] = "E0 FF 98 D2 C0 03 5F D6",
                    ["Max Int32"] = "E0 FF BF D2 C0 03 5F D6",
                    ["High Value"] = "00 E0 AF D2 C0 03 5F D6",
                    ["Float 0.0"] = "00 00 80 D2 C0 03 5F D6",
                    ["Float 1.0"] = "00 00 3F D2 C0 03 5F D6",
                    ["Float 2.0"] = "00 00 40 D2 C0 03 5F D6",
                    ["Float 5.0"] = "00 00 40 D2 A0 00 80 D2 C0 03 5F D6",
                    ["Float 10.0"] = "00 00 41 D2 C0 03 5F D6",
                    ["Float 50.0"] = "00 00 42 D2 40 06 80 D2 C0 03 5F D6",
                    ["Float 100.0"] = "00 00 42 D2 80 0C 80 D2 C0 03 5F D6",
                    ["Float 500.0"] = "00 00 43 D2 A0 0F 80 D2 C0 03 5F D6",
                    ["Float 999.0"] = "E0 1F 44 D2 C0 03 5F D6",
                    ["Float 1000.0"] = "80 7D 44 D2 C0 03 5F D6",
                    ["Double 1.0"] = "00 00 F0 3F C0 03 5F D6",
                    ["Double 100.0"] = "00 00 59 40 C0 03 5F D6",
                    ["Double 1000.0"] = "00 00 8F 40 C0 03 5F D6",
                    ["NOP"] = "1F 20 03 D5",
                    ["Return"] = "C0 03 5F D6",
                    ["Branch"] = "00 00 00 14",
                    ["Custom"] = ""
                }
            };
        }

        private void InitializeComponent()
        {
            this.Text = "LibIl2Cpp Batch Patcher";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // File info
            fileInfoLabel = new Label
            {
                Text = "No file loaded",
                Location = new Point(20, 20),
                Size = new Size(450, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            // Architecture selection
            var archLabel = new Label
            {
                Text = "Architecture:",
                Location = new Point(20, 70),
                Size = new Size(80, 20)
            };

            architectureComboBox = new ComboBox
            {
                Location = new Point(110, 68),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            architectureComboBox.Items.AddRange(new[] { "ARMv7", "ARM64" });
            architectureComboBox.SelectedIndex = 0; // Default to ARMv7

            // Open file button
            openButton = new Button
            {
                Text = "Open libil2cpp.so",
                Location = new Point(230, 65),
                Size = new Size(120, 30),
                UseVisualStyleBackColor = true
            };

            // Add/Delete row buttons
            addRowButton = new Button
            {
                Text = "Add Row",
                Location = new Point(370, 65),
                Size = new Size(80, 30),
                UseVisualStyleBackColor = true
            };

            deleteRowButton = new Button
            {
                Text = "Delete Row",
                Location = new Point(460, 65),
                Size = new Size(80, 30),
                UseVisualStyleBackColor = true
            };

            // DataGridView for patches
            patchGrid = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(740, 350),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Setup columns (removed Enable column)
            var offsetColumn = new DataGridViewTextBoxColumn
            {
                Name = "Offset",
                HeaderText = "Offset (Hex)",
                Width = 120
            };

            var presetColumn = new DataGridViewComboBoxColumn
            {
                Name = "Preset",
                HeaderText = "Preset Code",
                Width = 200,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
            };

            var customCodeColumn = new DataGridViewTextBoxColumn
            {
                Name = "CustomCode",
                HeaderText = "Custom Hex Code",
                Width = 300
            };

            var statusColumn = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 120,
                ReadOnly = true
            };

            patchGrid.Columns.AddRange(new DataGridViewColumn[] {
                offsetColumn, presetColumn, customCodeColumn, statusColumn
            });

            // Patch all button
            patchAllButton = new Button
            {
                Text = "Patch All",
                Location = new Point(20, 480),
                Size = new Size(100, 35),
                Enabled = false,
                BackColor = Color.Orange,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Save button
            saveButton = new Button
            {
                Text = "Save File",
                Location = new Point(140, 480),
                Size = new Size(80, 35),
                Enabled = false,
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            // Status bar
            statusLabel = new Label
            {
                Text = "Ready - Select architecture and load a file to start patching",
                Location = new Point(20, 530),
                Size = new Size(740, 30),
                BackColor = SystemColors.Control,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            this.Controls.AddRange(new Control[] {
                fileInfoLabel, archLabel, architectureComboBox, openButton, 
                addRowButton, deleteRowButton, patchGrid,
                patchAllButton, saveButton, statusLabel
            });
            
            // Event handlers
            architectureComboBox.SelectedIndexChanged += Architecture_Changed;
            openButton.Click += OpenFile_Click;
            addRowButton.Click += AddRow_Click;
            deleteRowButton.Click += DeleteRow_Click;
            saveButton.Click += SaveFile_Click;
            patchAllButton.Click += PatchAll_Click;
            patchGrid.CellValueChanged += PatchGrid_CellValueChanged;
            
            // Initialize with default architecture
            UpdatePresetOptions();
            AddDefaultRows();
        }

        private void Architecture_Changed(object? sender, EventArgs e)
        {
            UpdatePresetOptions();
            // Clear existing rows and add new defaults
            patchGrid.Rows.Clear();
            AddDefaultRows();
        }

        private void UpdatePresetOptions()
        {
            var selectedArch = architectureComboBox.SelectedItem?.ToString() ?? "ARMv7";
            var presetColumn = patchGrid.Columns["Preset"] as DataGridViewComboBoxColumn;
            
            if (presetColumn != null && presetCodesByArch.ContainsKey(selectedArch))
            {
                presetColumn.Items.Clear();
                presetColumn.Items.AddRange(presetCodesByArch[selectedArch].Keys.ToArray());
            }
        }

        private void AddDefaultRows()
        {
            var selectedArch = architectureComboBox.SelectedItem?.ToString() ?? "ARMv7";
            var presets = presetCodesByArch[selectedArch];
            
            // Add a few example rows based on architecture
            if (selectedArch == "ARMv7")
            {
                patchGrid.Rows.Add("", "Value 1 (True)", "", "Ready");
                patchGrid.Rows.Add("", "Float 100", "", "Ready");
                patchGrid.Rows.Add("", "Custom", "", "Ready");
            }
            else // ARM64
            {
                patchGrid.Rows.Add("", "Value 1 (True)", "", "Ready");
                patchGrid.Rows.Add("", "High Value", "", "Ready");
                patchGrid.Rows.Add("", "Custom", "", "Ready");
            }
        }

        private void AddRow_Click(object? sender, EventArgs e)
        {
            patchGrid.Rows.Add("", "Custom", "", "Ready");
        }

        private void DeleteRow_Click(object? sender, EventArgs e)
        {
            if (patchGrid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in patchGrid.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        patchGrid.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PatchGrid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = patchGrid.Rows[e.RowIndex];
            
            // If preset changed, update custom code
            if (e.ColumnIndex == patchGrid.Columns["Preset"].Index)
            {
                var presetValue = row.Cells["Preset"].Value?.ToString();
                var selectedArch = architectureComboBox.SelectedItem?.ToString() ?? "ARMv7";
                
                if (!string.IsNullOrEmpty(presetValue) && 
                    presetCodesByArch.ContainsKey(selectedArch) &&
                    presetCodesByArch[selectedArch].ContainsKey(presetValue))
                {
                    row.Cells["CustomCode"].Value = presetCodesByArch[selectedArch][presetValue];
                }
            }
        }

        private void OpenFile_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "SO Files (*.so)|*.so|All Files (*.*)|*.*",
                Title = "Select libil2cpp.so file"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileInfo = new FileInfo(openFileDialog.FileName);
                    
                    // Cảnh báo với file lớn
                    if (fileInfo.Length > 100 * 1024 * 1024) // 100MB
                    {
                        var result = MessageBox.Show(
                            $"File size is {fileInfo.Length / (1024 * 1024):F1}MB. This may use significant memory.\n\nContinue?",
                            "Large File Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        
                        if (result == DialogResult.No) return;
                    }

                    currentFilePath = openFileDialog.FileName;
                    fileData = File.ReadAllBytes(currentFilePath);
                    
                    fileInfoLabel.Text = $"File: {Path.GetFileName(currentFilePath)}\nSize: {fileData.Length:N0} bytes";
                    saveButton.Enabled = true;
                    patchAllButton.Enabled = true;
                    statusLabel.Text = $"Loaded: {Path.GetFileName(currentFilePath)} - Ready to patch ({architectureComboBox.SelectedItem} mode)";
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("File is too large to load into memory", "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PatchAll_Click(object? sender, EventArgs e)
        {
            if (fileData == null)
            {
                MessageBox.Show("Please load a file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int successCount = 0;
            int errorCount = 0;

            foreach (DataGridViewRow row in patchGrid.Rows)
            {
                if (row.IsNewRow) continue;

                var offsetStr = row.Cells["Offset"].Value?.ToString();
                var customCode = row.Cells["CustomCode"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(offsetStr) || string.IsNullOrWhiteSpace(customCode))
                {
                    row.Cells["Status"].Value = "Missing data";
                    errorCount++;
                    continue;
                }

                try
                {
                    var offset = Convert.ToInt32(offsetStr, 16);
                    var replaceBytes = HexStringToBytes(customCode);

                    if (offset + replaceBytes.Length > fileData.Length)
                    {
                        row.Cells["Status"].Value = "Exceeds file";
                        errorCount++;
                        continue;
                    }

                    Array.Copy(replaceBytes, 0, fileData, offset, replaceBytes.Length);
                    row.Cells["Status"].Value = "Patched ✓";
                    successCount++;
                }
                catch (Exception ex)
                {
                    row.Cells["Status"].Value = $"Error: {ex.Message}";
                    errorCount++;
                }
            }

            statusLabel.Text = $"Patch completed: {successCount} success, {errorCount} errors";
            
            if (successCount > 0)
            {
                MessageBox.Show($"Patching completed!\n\nSuccess: {successCount}\nErrors: {errorCount}", 
                    "Patch Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveFile_Click(object? sender, EventArgs e)
        {
            if (fileData == null || currentFilePath == null) return;

            try
            {
                // Create backup
                var backupPath = currentFilePath + ".backup";
                if (!File.Exists(backupPath))
                {
                    File.Copy(currentFilePath, backupPath);
                }

                File.WriteAllBytes(currentFilePath, fileData);
                statusLabel.Text = "File saved successfully with backup";
                MessageBox.Show("File saved successfully!\nBackup created: " + Path.GetFileName(backupPath), 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string must have even length");

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
    }
}