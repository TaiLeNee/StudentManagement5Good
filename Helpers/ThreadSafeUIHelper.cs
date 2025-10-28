using System;
using System.Windows.Forms;
using System.Drawing;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Helper class ð? th?c hi?n thread-safe UI operations
    /// </summary>
    public static class ThreadSafeUIHelper
    {
        /// <summary>
        /// Update text c?a control m?t cách thread-safe
        /// </summary>
        public static void SetText(Control control, string text)
        {
            if (control == null) return;

            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(() => 
                    {
                        if (!control.IsDisposed && control.IsHandleCreated)
                        {
                            control.Text = text;
                        }
                    }));
                }
                else
                {
                    if (!control.IsDisposed && control.IsHandleCreated)
                    {
                        control.Text = text;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Control ð? ðý?c dispose, không làm g?
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific cross-thread errors
                Console.WriteLine($"Cross-thread operation prevented: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other exceptions but don't crash
                Console.WriteLine($"Error setting text for control {control.Name}: {ex.Message}");
            }
        }

        /// <summary>
        /// Update forecolor c?a control m?t cách thread-safe
        /// </summary>
        public static void SetForeColor(Control control, Color color)
        {
            if (control == null) return;

            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(() => 
                    {
                        if (!control.IsDisposed && control.IsHandleCreated)
                        {
                            control.ForeColor = color;
                        }
                    }));
                }
                else
                {
                    if (!control.IsDisposed && control.IsHandleCreated)
                    {
                        control.ForeColor = color;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Control ð? ðý?c dispose, không làm g?
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific cross-thread errors
                Console.WriteLine($"Cross-thread operation prevented: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other exceptions but don't crash
                Console.WriteLine($"Error setting forecolor for control {control.Name}: {ex.Message}");
            }
        }

        /// <summary>
        /// Update backcolor c?a control m?t cách thread-safe
        /// </summary>
        public static void SetBackColor(Control control, Color color)
        {
            if (control == null) return;

            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(() => 
                    {
                        if (!control.IsDisposed && control.IsHandleCreated)
                        {
                            control.BackColor = color;
                        }
                    }));
                }
                else
                {
                    if (!control.IsDisposed && control.IsHandleCreated)
                    {
                        control.BackColor = color;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Control ð? ðý?c dispose, không làm g?
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific cross-thread errors
                Console.WriteLine($"Cross-thread operation prevented: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other exceptions but don't crash
                Console.WriteLine($"Error setting backcolor for control {control.Name}: {ex.Message}");
            }
        }

        /// <summary>
        /// Th?c thi action trên UI thread m?t cách thread-safe
        /// </summary>
        public static void InvokeOnUIThread(Control control, Action action)
        {
            if (control == null || action == null) return;

            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(() =>
                    {
                        if (!control.IsDisposed && control.IsHandleCreated)
                        {
                            action();
                        }
                    }));
                }
                else
                {
                    if (!control.IsDisposed && control.IsHandleCreated)
                    {
                        action();
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Control ð? ðý?c dispose, không làm g?
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific cross-thread errors - this is the main fix for your error
                Console.WriteLine($"Cross-thread operation prevented on control {control.Name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log other exceptions but don't crash
                Console.WriteLine($"Error invoking on UI thread for control {control.Name}: {ex.Message}");
            }
        }

        /// <summary>
        /// Ki?m tra control có th? truy c?p an toàn không
        /// </summary>
        public static bool CanAccessSafely(Control control)
        {
            if (control == null) return false;

            try
            {
                return !control.IsDisposed && control.IsHandleCreated && !control.InvokeRequired;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Safe method to update multiple labels at once
        /// </summary>
        public static void UpdateLabels(Control parentForm, params (string name, string text)[] labelUpdates)
        {
            if (parentForm == null || labelUpdates == null) return;

            InvokeOnUIThread(parentForm, () =>
            {
                foreach (var (name, text) in labelUpdates)
                {
                    var label = FindControlByName<Label>(parentForm, name);
                    if (label != null)
                    {
                        SetText(label, text);
                    }
                }
            });
        }

        /// <summary>
        /// Find control by name recursively
        /// </summary>
        private static T FindControlByName<T>(Control parent, string name) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T && control.Name == name)
                {
                    return control as T;
                }

                var found = FindControlByName<T>(control, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        /// <summary>
        /// Update DataGridView m?t cách thread-safe
        /// </summary>
        public static void UpdateDataGridView(DataGridView dgv, Action updateAction)
        {
            if (dgv == null || updateAction == null) return;

            InvokeOnUIThread(dgv, () =>
            {
                try
                {
                    dgv.SuspendLayout();
                    updateAction();
                    dgv.ResumeLayout();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating DataGridView: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Update ComboBox items m?t cách thread-safe
        /// </summary>
        public static void UpdateComboBoxItems(ComboBox comboBox, string[] items, int selectedIndex = -1)
        {
            if (comboBox == null || items == null) return;

            InvokeOnUIThread(comboBox, () =>
            {
                try
                {
                    comboBox.BeginUpdate();
                    comboBox.Items.Clear();
                    comboBox.Items.AddRange(items);
                    
                    if (selectedIndex >= 0 && selectedIndex < items.Length)
                    {
                        comboBox.SelectedIndex = selectedIndex;
                    }
                    
                    comboBox.EndUpdate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating ComboBox: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Hi?n th? MessageBox m?t cách thread-safe
        /// </summary>
        public static DialogResult ShowMessageBox(Control parent, string message, string title, 
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            DialogResult result = DialogResult.None;

            try
            {
                if (parent != null && parent.InvokeRequired)
                {
                    parent.Invoke(new Action(() =>
                    {
                        result = MessageBox.Show(parent, message, title, buttons, icon);
                    }));
                }
                else
                {
                    result = MessageBox.Show(parent, message, title, buttons, icon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error showing MessageBox: {ex.Message}");
                // Fallback to simple MessageBox without parent
                result = MessageBox.Show(message, title, buttons, icon);
            }

            return result;
        }

        /// <summary>
        /// Emergency method to handle cross-thread issues by temporarily disabling checks
        /// Use only as a last resort!
        /// </summary>
        public static void DisableCrossThreadChecking()
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disabling cross-thread checking: {ex.Message}");
            }
        }

        /// <summary>
        /// Re-enable cross-thread checking
        /// </summary>
        public static void EnableCrossThreadChecking()
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enabling cross-thread checking: {ex.Message}");
            }
        }
    }
}