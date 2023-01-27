using System;
using TaskScheduler;

namespace DLMSoft.MiniPAC {
    static class TaskSchedulerManager {
        const string TASK_NAME = "MiniPAC";

        static TaskSchedulerManager()
        {
            tsc_ = new TaskSchedulerClass();
            tsc_.Connect(null, null, null, null);

            var asm = typeof(TaskSchedulerManager).Assembly;
            executingPath_ = asm.Location;
        }

        static IRegisteredTaskCollection GetAllTasks()
        {
            var folder = tsc_.GetFolder("\\");
            return folder.GetTasks(1);
        }

        public static bool IsTaskExists()
        {
            var tasks = GetAllTasks();

            foreach (IRegisteredTask task in tasks) {
                if (task.Name == TASK_NAME) return true;
            }

            return false;
        }

        public static void CreateTask()
        {
            if (IsTaskExists()) return;

            var folder = tsc_.GetFolder("\\");

            var task = tsc_.NewTask(0);

            task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);

            var action = task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC) as IExecAction;
            action.Path = executingPath_;

            task.Settings.AllowDemandStart = false;
            task.Settings.StartWhenAvailable = false;
            task.Settings.RestartCount = 0;
            task.Settings.ExecutionTimeLimit = "PT0S";
            task.Settings.DisallowStartIfOnBatteries = false;
            task.Settings.StopIfGoingOnBatteries = false;
            task.Settings.StopIfGoingOnBatteries = false;
            task.Settings.MultipleInstances = _TASK_INSTANCES_POLICY.TASK_INSTANCES_IGNORE_NEW;
            task.Settings.IdleSettings.StopOnIdleEnd = false;
            task.Settings.IdleSettings.RestartOnIdle = false;
            task.Settings.Enabled = true;
            task.Settings.AllowHardTerminate = false;

            folder.RegisterTaskDefinition(TASK_NAME, task, (int)_TASK_CREATION.TASK_CREATE,
                null, null, _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN, "");
        }

        public static void DeleteTask()
        {
            if (!IsTaskExists()) return;

            var folder = tsc_.GetFolder("\\");

            folder.DeleteTask(TASK_NAME, 0);
        }

        static TaskSchedulerClass tsc_;
        static string executingPath_;
    }
}
