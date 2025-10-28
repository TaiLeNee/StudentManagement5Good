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
        /// Dictionary mapping c�c pattern th�?ng g?p
        /// </summary>
        private static readonly Dictionary<string, string> SimpleFixMap = new()
        {
            // Basic fixes - using only safe characters
            { "Ch? duy?t", "Ch? duy?t" },
            { "?? duy?t", "�? duy?t" },
            { "T? ch?i", "T? ch?i" },
            { "C?n b? sung", "C?n b? sung" },
            { "Ho?t ??ng", "Ho?t �?ng" },
            { "V? hi?u h?a", "V� hi?u h�a" },
            { "T?t c?", "T?t c?" },
            { "Qu?n tr? vi?n", "Qu?n tr? vi�n" },
            { "Gi?o v?", "Gi�o v?" },
            { "C? v?n", "C? v?n" },
            { "H?c t?p", "H?c t?p" },
            { "?o?n", "�o�n" },
            { "Tr??ng", "Tr�?ng" },
            { "Th?nh ph?", "Th�nh ph?" },
            { "Trung ??ng", "Trung ��ng" },
            { "Sinh vi?n", "Sinh vi�n" },
            { "Ng??i d?ng", "Ng�?i d�ng" },
            { "??n v?", "��n v?" },
            { "Ti?u ch?", "Ti�u ch�" },
            { "B?o c?o", "B�o c�o" },
            { "Th?ng k?", "Th?ng k�" },
            { "Ti?n ??", "Ti?n �?" },
            { "X?t duy?t", "X�t duy?t" },
            { "L?i", "L?i" },
            { "Th?nh c?ng", "Th�nh c�ng" },
            { "C?nh b?o", "C?nh b�o" },
            { "X?c nh?n", "X�c nh?n" },
            { "??ng xu?t", "��ng xu?t" },
            { "L?m m?i", "L�m m?i" },
            { "Ch?n", "Ch?n" },
            { "Nh?p", "Nh?p" },
            { "L? do", "L? do" },
            { "Chi ti?t", "Chi ti?t" },
            { "M? t?", "M� t?" },
            { "K?ch th??c", "K�ch th�?c" }
        };

        /// <summary>
        /// S?a text ti?ng Vi?t b? encode sai b?ng c�ch thay th? pattern ��n gi?n
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
        /// �p d?ng fix cho t?t c? controls trong form
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
        /// �? quy fix t?t c? controls con
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

                    // �? quy cho controls con
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
        /// Setup encoding cho to�n b? application
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

                // Register Encoding provider �? h? tr? code pages kh�c
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
        /// Force refresh UI v?i ti?ng Vi?t ��ng cho m?t form c? th?
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