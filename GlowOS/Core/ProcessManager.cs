using Cosmos.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowOS.Core
{
    public static class ProcessManager
    {
        public static List<Process> runningProcesses = new()
        {
            new BaseSystemProcesses.GlowOSSystem(), // the pid must be 0 as its the base process.
            new BaseSystemProcesses.GlowOSMouseHandler(),
            new BaseSystemProcesses.GlowOSDesktopUI(),
            new BaseSystemProcesses.GGLRenderer()
        };

        public static int GetCurrentPID
        {
            get
            {
                return runningProcesses.Count + 1;
            }
            set {  }
        }

        public static void AddProcess(Process process) { process.Initialize(); runningProcesses.Add(process); }
        public static void RemoveProcess(Process process) { runningProcesses.Remove(process); }
        public static void StopAllProcesses() { runningProcesses.Clear(); }

        public static void Update()
        {
            foreach (Process process in runningProcesses)
            {
                process.Running();
            }
        }
    }

    public class Process {
        public int pid; // process id
        public string name; // running name
        public string description; // short description

        public Process(string name, string description)
        {
            pid = ProcessManager.GetCurrentPID;
            this.name = name;
            this.description = description;
        }

        public Process() { }

        public virtual void Initialize()
        {
            
        }

        public virtual void Running()
        {

        }
    }

    public static class BaseSystemProcesses
    {
        public class GlowOSSystem : Process
        {
            public GlowOSSystem()
            {
                name = "GlowOS_System";
                description = "GlowOS's system core process.";
                pid = 0;
            }

            public override void Initialize()
            {
                Console.WriteLine("System core initialized!");
            }

            public override void Running()
            {
                Kernel.framesCount++;

                if (Kernel.framesCount >= 20)
                {
                    Heap.Collect();
                    Kernel.framesCount = 0;
                }
            }
        }

        public class GlowOSMouseHandler : Process
        {
            public GlowOSMouseHandler()
            {
                name = "GlowOS_MouseHandler";
                description = "GlowOS's mouse handler that manages and updates cursor.";
                pid = 1;
            }

            public override void Initialize()
            {
                Console.WriteLine("System mouse handler initialized!");
            }

            public override void Running()
            {
                MouseManager.Update();
            }
        }

        public class GlowOSDesktopUI : Process
        {
            public GlowOSDesktopUI()
            {
                name = "GlowOS_DesktopUI";
                description = "GlowOS_DesktopUI includes handling the top and bottom menu.";
                pid = 2; // the pid's needed to be hard-coded due to the process list being generated before the the variable
            }

            public override void Initialize()
            {
                base.Initialize();
            }

            public override void Running()
            {
                base.Running();
            }
        }

        public class GGLRenderer : Process
        {
            public GGLRenderer()
            {
                name = "GGLRenderer";
                description = "Loads all registered GlowCanvas instances.";
                pid = 3;
            }

            public override void Initialize() { }

            public override void Running()
            {
                // GGL.Render();
            }
        }
    }
}
