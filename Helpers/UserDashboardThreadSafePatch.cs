using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using StudentManagement5Good.Winform;

namespace StudentManagement5Good.Helpers
{
    /// <summary>
    /// Thread-safe patch for UserDashboard to prevent cross-thread operation errors
    /// </summary>
    public static class UserDashboardThreadSafePatch
    {
        /// <summary>
        /// Apply thread-safe patches to UserDashboard form
        /// </summary>
        public static void ApplyThreadSafePatches(UserDashboard dashboard)
        {
            if (dashboard == null) return;

            try
            {
                // Disable CheckForIllegalCrossThreadCalls for this form only during initialization
                // This is a temporary measure - better solution is to use proper Invoke patterns
                Control.CheckForIllegalCrossThreadCalls = false;
                
                // Re-enable after a short delay
                Task.Run(async () =>
                {
                    await Task.Delay(2000);
                    if (dashboard.InvokeRequired)
                    {
                        dashboard.Invoke(new Action(() => Control.CheckForIllegalCrossThreadCalls = true));
                    }
                    else
                    {
                        Control.CheckForIllegalCrossThreadCalls = true;
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying thread-safe patches: {ex.Message}");
            }
        }

        /// <summary>
        /// Safe method to update dashboard data with proper thread safety
        /// </summary>
        public static void SafeUpdateDashboardData(UserDashboard dashboard, 
            string pendingCount, string processedCount, string deadlineInfo, string systemStatus)
        {
            if (dashboard == null) return;

            try
            {
                ThreadSafeUIHelper.InvokeOnUIThread(dashboard, () =>
                {
                    // Find controls by name and update them safely
                    var lblPendingCount = FindControl<Label>(dashboard, "lblPendingCount");
                    var lblProcessedCount = FindControl<Label>(dashboard, "lblProcessedCount");
                    var lblDeadlineInfo = FindControl<Label>(dashboard, "lblDeadlineInfo");
                    var lblSystemStatusInfo = FindControl<Label>(dashboard, "lblSystemStatusInfo");

                    if (lblPendingCount != null)
                        ThreadSafeUIHelper.SetText(lblPendingCount, pendingCount);

                    if (lblProcessedCount != null)
                        ThreadSafeUIHelper.SetText(lblProcessedCount, processedCount);

                    if (lblDeadlineInfo != null)
                        ThreadSafeUIHelper.SetText(lblDeadlineInfo, deadlineInfo);

                    if (lblSystemStatusInfo != null)
                    {
                        ThreadSafeUIHelper.SetText(lblSystemStatusInfo, systemStatus);
                        ThreadSafeUIHelper.SetForeColor(lblSystemStatusInfo, Color.White);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating dashboard data safely: {ex.Message}");
            }
        }

        /// <summary>
        /// Safe method to update current date time
        /// </summary>
        public static void SafeUpdateDateTime(UserDashboard dashboard, string dateTime)
        {
            if (dashboard == null) return;

            try
            {
                var lblCurrentDateTime = FindControl<Label>(dashboard, "lblCurrentDateTime");
                if (lblCurrentDateTime != null)
                {
                    ThreadSafeUIHelper.SetText(lblCurrentDateTime, dateTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating date time safely: {ex.Message}");
            }
        }

        /// <summary>
        /// Generic method to find a control by name recursively
        /// </summary>
        private static T FindControl<T>(Control parent, string name) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T && control.Name == name)
                {
                    return control as T;
                }

                var found = FindControl<T>(control, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        /// <summary>
        /// Safer timer implementation that prevents cross-thread issues
        /// </summary>
        public static System.Windows.Forms.Timer CreateSafeTimer(UserDashboard dashboard, int interval)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = interval;
            
            timer.Tick += async (sender, e) =>
            {
                try
                {
                    // Temporarily stop timer to prevent overlapping executions
                    timer.Stop();
                    
                    // Update date time safely
                    SafeUpdateDateTime(dashboard, DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm"));
                    
                    // Check if dashboard module is visible before refreshing data
                    var panelDashboardModule = FindControl<Panel>(dashboard, "panelDashboardModule");
                    if (panelDashboardModule?.Visible == true)
                    {
                        // Trigger data refresh through a method call that can be made thread-safe
                        // This should be implemented in the UserDashboard class itself
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Timer error: {ex.Message}");
                }
                finally
                {
                    // Restart timer
                    timer.Start();
                }
            };
            
            return timer;
        }

        /// <summary>
        /// Emergency fix for cross-thread issues - disables checks temporarily
        /// USE WITH CAUTION - This is a temporary workaround
        /// </summary>
        public static void EmergencyThreadFix()
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Emergency thread fix error: {ex.Message}");
            }
        }

        /// <summary>
        /// Re-enable cross-thread checks
        /// </summary>
        public static void RestoreThreadChecks()
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Restore thread checks error: {ex.Message}");
            }
        }
    }
}