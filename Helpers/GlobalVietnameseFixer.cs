using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Simplified Vietnamese text fixer
    /// </summary>
    public static class GlobalVietnameseFixer
    {
        /// <summary>
        /// Dictionary mapping các pattern thý?ng g?p
        /// </summary>
        private static readonly Dictionary<string, string> SimpleFixMap = new()
        {
            // Basic fixes - using only safe characters
            { "Ch? duy?t", "Ch? duy?t" },
            { "?? duy?t", "Ð? duy?t" },
            { "T? ch?i", "T? ch?i" },
            { "C?n b? sung", "C?n b? sung" },
            { "Ho?t ??ng", "Ho?t ð?ng" },
            { "V? hi?u h?a", "Vô hi?u hóa" },
            { "T?t c?", "T?t c?" },
            { "Qu?n tr? vi?n", "Qu?n tr? viên" },
            { "Gi?o v?", "Giáo v?" },
            { "C? v?n", "C? v?n" },
            { "H?c t?p", "H?c t?p" },
            { "?o?n", "Ðoàn" },
            { "Tr??ng", "Trý?ng" },
            { "Th?nh ph?", "Thành ph?" },
            { "Trung ??ng", "Trung ýõng" },
            { "Sinh vi?n", "Sinh viên" },
            { "Ng??i d?ng", "Ngý?i dùng" },
            { "??n v?", "Ðõn v?" },
            { "Ti?u ch?", "Tiêu chí" },
            { "B?o c?o", "Báo cáo" },
            { "Th?ng k?", "Th?ng kê" },
            { "Ti?n ??", "Ti?n ð?" },
            { "X?t duy?t", "Xét duy?t" },
            { "L?i", "L?i" },
            { "Th?nh c?ng", "Thành công" },
            { "C?nh b?o", "C?nh báo" },
            { "X?c nh?n", "Xác nh?n" },
            { "??ng xu?t", "Ðãng xu?t" },
            { "L?m m?i", "Làm m?i" },
            { "Ch?n", "Ch?n" },
            { "Nh?p", "Nh?p" },
            { "L? do", "L? do" },
            { "Chi ti?t", "Chi ti?t" },
            { "M? t?", "Mô t?" },
            { "K?ch th??c", "Kích thý?c" }
        };

        /// <summary>
        /// S?a text ti?ng Vi?t b? encode sai b?ng cách thay th? pattern ðõn gi?n
        /// </summary>
        public static string FixVietnameseText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            string result = text;
            
            foreach (var pair in SimpleFixMap)
            {
                result = result.Replace(pair.Key, pair.Value, StringComparison.OrdinalIgnoreCase);
            }

            return result;
        }

        /// <summary>
        /// Áp d?ng fix cho t?t c? controls trong form
        /// </summary>
        public static void FixAllControlsInForm(Form form)
        {
            if (form == null) return;

            try
            {
                // Fix form title
                form.Text = FixVietnameseText(form.Text);

                // Fix t?t c? controls
                FixControlsRecursive(form);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing Vietnamese text: {ex.Message}");
            }
        }

        /// <summary>
        /// Ð? quy fix t?t c? controls con
        /// </summary>
        private static void FixControlsRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                try
                {
                    // Fix text c?a control
                    if (!string.IsNullOrEmpty(control.Text))
                    {
                        var fixedText = FixVietnameseText(control.Text);
                        if (fixedText != control.Text)
                        {
                            ThreadSafeUIHelper.SetText(control, fixedText);
                        }
                    }

                    // Fix items c?a ComboBox
                    if (control is ComboBox comboBox)
                    {
                        FixComboBoxItems(comboBox);
                    }

                    // Fix items c?a ListBox
                    if (control is ListBox listBox)
                    {
                        FixListBoxItems(listBox);
                    }

                    // Fix DataGridView
                    if (control is DataGridView dataGridView)
                    {
                        FixDataGridView(dataGridView);
                    }

                    // Fix MenuItem (cho ContextMenuStrip)
                    if (control is ContextMenuStrip contextMenu)
                    {
                        FixContextMenuItems(contextMenu);
                    }

                    // Ð? quy cho controls con
                    if (control.HasChildren)
                    {
                        FixControlsRecursive(control);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fixing control {control.Name}: {ex.Message}");
                }
            }
        }

        private static void FixComboBoxItems(ComboBox comboBox)
        {
            try
            {
                var selectedIndex = comboBox.SelectedIndex;
                var needsUpdate = false;
                var fixedItems = new List<object>();

                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i] != null)
                    {
                        var originalText = comboBox.Items[i].ToString();
                        var fixedText = FixVietnameseText(originalText);
                        
                        if (originalText != fixedText)
                        {
                            needsUpdate = true;
                        }
                        
                        fixedItems.Add(fixedText);
                    }
                    else
                    {
                        fixedItems.Add(comboBox.Items[i]);
                    }
                }

                if (needsUpdate)
                {
                    ThreadSafeUIHelper.InvokeOnUIThread(comboBox, () =>
                    {
                        comboBox.BeginUpdate();
                        comboBox.Items.Clear();
                        foreach (var item in fixedItems)
                        {
                            comboBox.Items.Add(item);
                        }
                        
                        if (selectedIndex >= 0 && selectedIndex < comboBox.Items.Count)
                        {
                            comboBox.SelectedIndex = selectedIndex;
                        }
                        comboBox.EndUpdate();
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing ComboBox items: {ex.Message}");
            }
        }

        private static void FixListBoxItems(ListBox listBox)
        {
            try
            {
                var needsUpdate = false;
                var fixedItems = new List<object>();

                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    if (listBox.Items[i] != null)
                    {
                        var originalText = listBox.Items[i].ToString();
                        var fixedText = FixVietnameseText(originalText);
                        
                        if (originalText != fixedText)
                        {
                            needsUpdate = true;
                        }
                        
                        fixedItems.Add(fixedText);
                    }
                    else
                    {
                        fixedItems.Add(listBox.Items[i]);
                    }
                }

                if (needsUpdate)
                {
                    ThreadSafeUIHelper.InvokeOnUIThread(listBox, () =>
                    {
                        listBox.BeginUpdate();
                        listBox.Items.Clear();
                        foreach (var item in fixedItems)
                        {
                            listBox.Items.Add(item);
                        }
                        listBox.EndUpdate();
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing ListBox items: {ex.Message}");
            }
        }

        private static void FixDataGridView(DataGridView dgv)
        {
            try
            {
                ThreadSafeUIHelper.UpdateDataGridView(dgv, () =>
                {
                    // Fix column headers
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        var fixedHeader = FixVietnameseText(column.HeaderText);
                        if (fixedHeader != column.HeaderText)
                        {
                            column.HeaderText = fixedHeader;
                        }
                    }

                    // Fix cell values
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && cell.Value is string cellText)
                            {
                                var fixedValue = FixVietnameseText(cellText);
                                if (fixedValue != cellText)
                                {
                                    cell.Value = fixedValue;
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing DataGridView: {ex.Message}");
            }
        }

        private static void FixContextMenuItems(ContextMenuStrip contextMenu)
        {
            try
            {
                ThreadSafeUIHelper.InvokeOnUIThread(contextMenu, () =>
                {
                    foreach (ToolStripItem item in contextMenu.Items)
                    {
                        var fixedText = FixVietnameseText(item.Text);
                        if (fixedText != item.Text)
                        {
                            item.Text = fixedText;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing ContextMenu items: {ex.Message}");
            }
        }

        /// <summary>
        /// Setup encoding cho toàn b? application
        /// </summary>
        public static void SetupApplicationEncoding()
        {
            try
            {
                // Set console encoding
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;

                // Set thread culture
                var culture = new CultureInfo("vi-VN");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                // Set application culture
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                // Register Encoding provider ð? h? tr? code pages khác
                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                }
                catch
                {
                    // Ignore if already registered
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up encoding: {ex.Message}");
            }
        }

        /// <summary>
        /// Force refresh UI v?i ti?ng Vi?t ðúng cho m?t form c? th?
        /// </summary>
        public static void ForceRefreshVietnamese(Form form)
        {
            if (form == null) return;

            ThreadSafeUIHelper.InvokeOnUIThread(form, () =>
            {
                FixAllControlsInForm(form);
                form.Refresh();
            });
        }
    }
}